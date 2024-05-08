using Choco.EventHandlers;
using Choco.Services.ServicesMessage;
using ChocoLogging;
using DSharpPlus;

namespace Choco.EventHandlerRegisterClient
{
    public class EventHandlers
    {
        private readonly DiscordClient _client;

        public EventHandlers(DiscordClient client)
        {
            _client = client;
            RegisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            LogMessage.LogHandler();

            _client.Ready += EventHandlerClientReadyHandler.HandlerReady;
            //_client.GuildMemberAdded += Client_GuildMemberAdded;
            //_client.ModalSubmitted += Client_ModalSubmitted;
            _client.MessageCreated += HandlerMessageCreated.ServiceMessageServiceReaction;
            _client.ComponentInteractionCreated += EventHandlerInteraction.HandlerComponentInteractionCreated;
            _client.ClientErrored += EventHandlerClientError.HandlerClientError;

        }
    }
}
