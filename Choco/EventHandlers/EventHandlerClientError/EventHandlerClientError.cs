using ChocoLogging;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Choco.EventHandlers
{
    public static class EventHandlerClientError
    {
        public static Task HandlerClientError(DiscordClient sender, ClientErrorEventArgs e)
        {
            LogMessage.LogHandler();

            sender.Logger.LogError(e.Exception, "Exception occured");

            return Task.CompletedTask;
        }
    }
}