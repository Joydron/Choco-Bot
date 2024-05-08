using DSharpPlus.EventArgs;
using DSharpPlus;
using ChocoLogging;

namespace Choco.EventHandlers
{
    public static class EventHandlerClientReadyHandler
    {
        public static async Task HandlerReady (DiscordClient sender, ReadyEventArgs e)
        {
            LogMessage.LogHandler();
        }
    }
}
