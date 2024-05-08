using Choco.Core.Database;
using ChocoLogging;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.EntityFrameworkCore;


namespace Choco.Services.ServicesGames.ServiceGameNovella
{
    public class ServiceGameNovellaButtons
    {
        internal static async Task ServiceGameNovellaButtonsRightLeftInteraction (ComponentInteractionCreateEventArgs args)
        {
            LogMessage.LogService();

            // Получаю из айдишника номер 
            // Получаю айдишних текущего слайда
            int num = int.Parse(args.Id.Split(",")[1]);
            var buttonIdRight = $"game-novella-right/left,{num + 1}";
            var buttonIdLeft = $"game-novella-right/left,{num - 1}";

            await args.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

            DiscordEmbed embedPicture = new DiscordEmbedBuilder { };
            DiscordEmbed embedText = new DiscordEmbedBuilder { };

            using (var context = new StaticDBContext())
            {
                var novella = await context.Novella
                    .Skip(num)
                    .Take(1)
                    .FirstOrDefaultAsync();

                if (novella != null)
                {
                    embedText = new DiscordEmbedBuilder()
                        .WithTitle(novella.WithTitle)
                        .WithDescription(novella.WithDescription)
                        .WithImageUrl("https://cdn.discordapp.com/attachments/1096410743395586069/1127278573607202836/empty_line.png")
                        .WithThumbnail(novella.WithThumbnail)
                        .WithFooter($"Слайд: {num}")
                        .WithColor(DiscordColor.Goldenrod);
                    embedPicture = new DiscordEmbedBuilder()
                        .WithImageUrl(novella.WithImageUrlMain)
                        .WithColor(DiscordColor.Goldenrod);

                    var buttonRight = new DiscordButtonComponent(ButtonStyle.Secondary, buttonIdRight, "------>");
                    var buttonLeft = new DiscordButtonComponent(ButtonStyle.Secondary, buttonIdLeft, "<------");
                    var buttImg = new DiscordButtonComponent(ButtonStyle.Secondary, "game-novella-getImage", "✷");
                    var buttAva = new DiscordButtonComponent(ButtonStyle.Secondary, "game-novella-getAvatar", "⚇");

                    var builder = new DiscordMessageBuilder()
                        .AddEmbed(embedPicture)
                        .AddEmbed(embedText)
                        .AddComponents(buttonLeft, buttAva, buttImg, buttonRight);

                    await args.Message.ModifyAsync(builder).ConfigureAwait(false);

                }
            }
        }
        internal static async Task ServiceGetImageInteraction(ComponentInteractionCreateEventArgs args)
        {
            LogMessage.LogService();
            await args.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
            string url = args.Message.Embeds[0].Image.Url.ToString();
            await args.Interaction.Channel.SendMessageAsync(url).ConfigureAwait(false);

        }

        internal static async Task ServiceGetAvatarInteraction(ComponentInteractionCreateEventArgs args)
        {
            LogMessage.LogService();
            await args.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
            string url = args.Message.Embeds[1].Thumbnail.Url.ToString();
            await args.Interaction.Channel.SendMessageAsync(url).ConfigureAwait(false);
        }
    }
}
