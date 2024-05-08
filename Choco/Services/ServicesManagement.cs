using DSharpPlus;
using Choco.Services;
using Choco.Services.ServicesMessage;
using Microsoft.Extensions.DependencyInjection;
using ChocoLogging;
using System.Reflection;
using ConfigChannels;

namespace Choco.Services
{
    public static class ServicesManagement
    {
        public static IServiceProvider SetupServices(DiscordClient client)
        {
            LogMessage.LogService();

            var services = new ServiceCollection();
            services.AddSingleton(client);
            services.AddSingleton<EventHandlerRegisterClient.EventHandlers>(); 

            var serviceProvider = services.BuildServiceProvider();

            var eventHandlers = serviceProvider.GetRequiredService<EventHandlerRegisterClient.EventHandlers>();

            SetupCustomServices(client, serviceProvider);

            return serviceProvider;
        }

        private static void SetupCustomServices(DiscordClient client, IServiceProvider serviceProvider)
        {
            ulong getChannelIdGameUpdateMenu = ConfigChannelId.GetChannelId("IdChannelGameUpdateMenu");

            var emojiToChat = new ServiceSendEmojiToChat(client);
            client.Ready += async (sender, args) =>
            {
                await emojiToChat.ServiceStartEmojiSenderAsync();
            };

            var channelMessageManager = new ChannelMessageManager(client, getChannelIdGameUpdateMenu);
            client.Ready += async (sender, args) =>
            {
                await channelMessageManager.ServiceUpdateMessagesAsync();
            };
        }
    }
}