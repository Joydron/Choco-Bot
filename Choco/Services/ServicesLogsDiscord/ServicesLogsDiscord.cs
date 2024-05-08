using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using ChocoLogging;
using ConfigChannels;

namespace Choco.Services.ServicesLogsDiscord
{
    public class ServicesLogsGameDiscord
    {
        public static async Task ServicesLogsDiscordNovella(ComponentInteractionCreateEventArgs args, DiscordMember member)
        {
            LogMessage.LogService();

            ulong getChannelId = ConfigChannelId.GetChannelId("IdChannelGameLogs");
            var logsChannel = member.Guild.GetChannel(getChannelId);

            var embLogs = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Teal,
                Title = "Info on the last interaction " + member.Username,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = member.AvatarUrl }
            };

            embLogs.AddField("Username", args.Interaction.User.Mention, true)
                   .AddField("Open/Closed DM", args.Interaction.Guild.DefaultMessageNotifications.ToString(), true)
                   .AddField("Time", DateTime.Now.ToString(), true)
                   .AddField("Id", member.Id.ToString(), true)
                   .AddField("Name of the game", "Приключение принцессы Сахарок", true);

            await logsChannel.SendMessageAsync(embLogs);
        }
    }
}