using Choco.Services.ServicesMessage;
using ChocoLogging;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Choco.EventHandlers
{
    public static class EventHandlerMessageCreated
    {
        public static async Task HandleMessageCreated(DiscordClient sender, MessageCreateEventArgs args)
        {
            LogMessage.LogHandler();

            await HandlerMessageCreated.ServiceMessageServiceReaction(sender, args);
        }
    }
}
