using System;
using System.Collections.Generic;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using ChocoLogging;
using Microsoft.Extensions.Configuration;

namespace ConfigChannels
{
    public class ConfigChannelId
    {
        private static readonly Dictionary<string, ulong> ChannelIds = new Dictionary<string, ulong>();

        static ConfigChannelId()
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            ChannelIds["IdChannelGameLogs"] = configuration.GetValue<ulong>("ID_CHANNEL_GAME_LOGS");
            ChannelIds["IdChannelGameUpdateMenu"] = configuration.GetValue<ulong>("ID_CHANNEL_GAME_UPDATE_MENU");
            ChannelIds["IdChannelSendEmoji"] = configuration.GetValue<ulong>("ID_CHANNEL_SEND_EMOJI");
            ChannelIds["IdChannelSendReaction1"] = configuration.GetValue<ulong>("ID_CHANNEL_SEND_REACTION1");
            ChannelIds["IdChannelSendReaction2"] = configuration.GetValue<ulong>("ID_CHANNEL_SEND_REACTION2");
        }
        public static ulong GetChannelId(string key)
        {
            if (ChannelIds.TryGetValue(key, out ulong id))
                return id;
            else
                throw new KeyNotFoundException($"Key {key} not found in Channel IDs.");
        }
    }
}