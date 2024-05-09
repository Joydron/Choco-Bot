using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus;
using ChocoLogging;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choco.Services.ServicesMessage
{
    public class HandlerMessageCreated
    {
        private static readonly Dictionary<ulong, DiscordEmoji> channelEmojis;

        static HandlerMessageCreated()
        {
            var emojiChannelIds = new Dictionary<string, string>
            {
                { "IdChannelSendReaction1", ":lublu:" },
                { "IdChannelSendReaction2", ":nya:" }
            };

            channelEmojis = new Dictionary<ulong, DiscordEmoji>();
            foreach (var pair in emojiChannelIds)
            {
                ulong channelId = ConfigChannels.ConfigChannelId.GetChannelId(pair.Key);
                channelEmojis[channelId] = DiscordEmoji.FromName(Program.Client, pair.Value);
            }
        }

        public static async Task ServiceMessageServiceReaction(DiscordClient sender, MessageCreateEventArgs args)
        {
            LogMessage.LogService();

            if (channelEmojis.TryGetValue(args.Channel.Id, out var emoji))
            {
                await args.Message.CreateReactionAsync(emoji);
            }
        }
    }
}
