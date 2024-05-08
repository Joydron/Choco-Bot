using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ChocoLogging;
using ConfigChannels;
using DSharpPlus;
using DSharpPlus.Entities;
using Serilog;

namespace Choco.Services.ServicesMessage
{
    public class ServiceSendEmojiToChat
    {
        private readonly DiscordClient _client;
        private readonly Random _random = new Random();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public ServiceSendEmojiToChat(DiscordClient client)
        {
            _client = client;
        }

        public async Task ServiceStartEmojiSenderAsync()
        {
            LogMessage.LogService();

            await Task.Delay(5000); // waiting 5 sec while boy is ready

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    ulong getChannelIdSendEmoji = ConfigChannelId.GetChannelId("IdChannelSendEmoji");
                    var channel = await _client.GetChannelAsync(getChannelIdSendEmoji);

                    if (channel != null)
                    {

                        List<string> emojiesNames = new List<string>
                        {
                            ":pepe_popcorn:",
                            ":pepecute:",
                            ":PeepeDisgust:",
                            ":beer:",
                            ":nya:",
                            ":Yey:",
                            ":a_:",
                            ":eh:",
                            ":embarrassed:",
                            ":bbbaka:",
                            ":slublu:",
                            ":nasuversgovnina:",
                            ":puzirik:",
                            ":choco_love:",
                            ":oAo:",
                            ":troll_cat:",
                            ":AnzuFutaba:"
                        };

                        var emojies = emojiesNames.Select(name => DiscordEmoji.FromName(_client, name)).ToList();
                        var randomEmoji = emojies[_random.Next(emojies.Count)];
                        await channel.SendMessageAsync(randomEmoji);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Ошибка при обработке данных: {ErrorMessage}", ex.Message);
                }

                await Task.Delay(TimeSpan.FromMinutes(_random.Next(25, 70)), _cancellationTokenSource.Token);
            }
        }

        public void StopEmojiSender()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}