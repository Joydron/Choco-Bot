using Choco.Services.ServicesGames;
using Choco.Services.ServicesGames.ServiceGameNovella;
using ChocoLogging;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Choco.EventHandlers
{
    public static class EventHandlerInteraction
    {
        private static readonly Dictionary<string, Func<ComponentInteractionCreateEventArgs, Task>> _handlers = new()
        {
            { "menu", ServiceChannelGameMenuInteraction.ServiceGameMenuInteraction },
            { "game-novella-right/left", ServiceGameNovellaButtons.ServiceGameNovellaButtonsRightLeftInteraction },
            { "game-novella-getImage", ServiceGameNovellaButtons.ServiceGetImageInteraction },
            { "game-novella-getAvatar", ServiceGameNovellaButtons.ServiceGetAvatarInteraction }
        };

        public static async Task HandlerComponentInteractionCreated(DiscordClient client, ComponentInteractionCreateEventArgs args)
        {
            LogMessage.LogHandler();

            if (_handlers.TryGetValue(args.Id.Split(",")[0], out var handler))
            {
                await handler(args);
            }
        }

    }
}