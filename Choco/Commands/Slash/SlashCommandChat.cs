using Choco.Core.Database;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.EntityFrameworkCore;
using System.IO;
using static DSharpPlus.Entities.DiscordEmbedBuilder;
using System.Xml;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using DSharpPlus.Exceptions;
using Microsoft.Extensions.Logging.Abstractions;
using Choco.Database.Models.StaticDB;
using static Choco.Services.ServicesCommands.ServiceCommandRpgService;
using DSharpPlus.CommandsNext.Attributes;
using Choco.Services.ServicesCommands;
using ChocoLogging;
using System.Reflection;

namespace Choco.Commands.Slash
{
    public class SlashCommandChat : ApplicationCommandModule
    {
        private static readonly Random random = new Random();

        [SlashCommand("answer_me", "ми?")]
        [SlashCooldown(1, 5, SlashCooldownBucketType.User)]
        public async Task CommandAnswerMe (InteractionContext ctx)
        {
            LogMessage.LogsCommand(ctx: ctx);
            string response = string.Empty;

            using (var databaseContext = new StaticDBContext())
            {
                var maxId = await databaseContext.AnswersMe.MaxAsync(x => x.Id);

                var randomId = random.Next(1, maxId + 1);

                response = await databaseContext.AnswersMe
                    .Where(answer => answer.Id == randomId)
                    .Select(answer => answer.goodwords)
                    .FirstOrDefaultAsync() ?? "default";

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(response));
            }
        }

        [SlashCommand("ping", "ping?")]
        [SlashCooldown(1, 5, SlashCooldownBucketType.User)]
        public async Task CommandPing(InteractionContext ctx)
        {
            LogMessage.LogsCommand(ctx: ctx);

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("pong?"));
        }


        [SlashCommand("rpg", "Выбирает рандомную игру на rpg maker")]
        [SlashCooldown(1, 5, SlashCooldownBucketType.User)]
        public async Task CommandRpg(InteractionContext ctx)
        {
            LogMessage.LogsCommand(ctx: ctx);

            try
            {
                await ctx.DeferAsync();

                using (var databaseContext = new StaticDBContext())
                {
                    var maxId = await databaseContext.Rpg.MaxAsync(x => x.Id);
                    var randomId = random.Next(1, maxId + 1);
                    var rpgEntry = await databaseContext.Rpg
                        .Where(rpg => rpg.Id == randomId)
                        .FirstOrDefaultAsync();

                    if (rpgEntry == null) {
                        Program.Client?.Logger.LogError($"[RpgCommand.Error] - Can't get rpg entry.");
                        return;
                    }

                    if (rpgEntry.name == null) {
                        Program.Client?.Logger.LogError($"[RpgCommand.Error] - Rpg entry has no name.");
                        return;
                    }

                    var loadingMessage = await ctx.Channel.SendMessageAsync("[--------------]");
                    await LoadingService.ServiceUpdateLoadingMessage(loadingMessage, 2);

                    var rpgEmbedCover = ServiceCommandRpgService.ServiceCreateEmbedWithCover(rpgEntry);
                    var rpgEmbedPics = ServiceCommandRpgService.ServiceCreateEmbeds();
                    await LoadingService.ServiceUpdateLoadingMessage(loadingMessage, 4);

                    var embedsToAdd = rpgEmbedPics.Values.Select(builder => builder.Build());
                    await LoadingService.ServiceUpdateLoadingMessage(loadingMessage, 6);

                    string pattern = @"[:?]";
                    Regex regex = new Regex(pattern);
                    string cleanedName = regex.Replace(rpgEntry.name, "_");
                    await LoadingService.ServiceUpdateLoadingMessage(loadingMessage, 6);

                    string pathCover = $"./Pictures/rpgPictures/{randomId}_" + $"{cleanedName.Replace(" ", "_")}_cover_resized.png";
                    string pathPic1 = $"./Pictures/rpgPictures/{randomId}_" + $"{cleanedName.Replace(" ", "_")}_pic1_resized.png";
                    string pathPic2 = $"./Pictures/rpgPictures/{randomId}_" + $"{cleanedName.Replace(" ", "_")}_pic2_resized.png";
                    string pathPic3 = $"./Pictures/rpgPictures/{randomId}_" + $"{cleanedName.Replace(" ", "_")}_pic3_resized.png";

                    var streamCover = ServiceCommandRpgService.ServiceGetStreamFromFile(pathCover);
                    var streamPic1 = ServiceCommandRpgService.ServiceGetStreamFromFile(pathPic1);
                    var streamPic2 = ServiceCommandRpgService.ServiceGetStreamFromFile(pathPic2);
                    var streamPic3 = ServiceCommandRpgService.ServiceGetStreamFromFile(pathPic3);

                    await LoadingService.ServiceUpdateLoadingMessage(loadingMessage, 14);

                    var streams = new Dictionary<string, Stream>
                        {
                            {"image.png", streamCover},
                            {"pic1.png", streamPic1},
                            {"pic2.png", streamPic2},
                            {"pic3.png", streamPic3}
                        };

                    await LoadingService.ServiceUpdateLoadingMessage(loadingMessage, 16);
                    await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(rpgEmbedCover)
                                                                               .AddEmbeds(embedsToAdd)
                                                                               .AddFiles(streams));
                    await loadingMessage.DeleteAsync();
                }
            }
            catch (BadRequestException e)
            {
                ctx.Client.Logger.LogInformation($"error: {e.Errors}");
                ctx.Client.Logger.LogInformation($"error: {e.JsonMessage}");
            }
        }

        [SlashCommand("ava", "Получить аватар пользователя")]
        [SlashCooldown(1, 5, SlashCooldownBucketType.User)]
        public async Task Avatar(InteractionContext ctx)
        {
            LogMessage.LogsCommand(ctx: ctx);
            var mbr = ctx.Member;  

            var urlUser = mbr.GetAvatarUrl(ImageFormat.Png);
            string ava = urlUser.ToString();

            DiscordEmbedBuilder discordEmbed = new()
            {
                Title = "А кто это у нас такой красивый?",
                Description = "Да-да, это ты",
                ImageUrl = mbr.GetAvatarUrl(ImageFormat.Png),
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    IconUrl = ava,
                    Text = mbr.Username
                }
            };

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(discordEmbed));
        }
    } 
}
