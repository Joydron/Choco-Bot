using Choco.Commands.Prefix;
using Choco.Commands.Slash;
using ChocoLogging;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;

namespace Choco.Commands
{
    public static class CommandManager
    {
        public static void SetupCommands(DiscordClient client, string prefix, IServiceProvider serviceProvider)
        {
            LogMessage.LogOther();
            // Создание конфигурации команд
            var commandsConfig = new CommandsNextConfiguration
            {
                Services = serviceProvider,
                StringPrefixes = new string[] { prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            // CommandsPrefix
            var prefixCommands = client.UseCommandsNext(commandsConfig);
            prefixCommands.RegisterCommands<PrefixCommandChat>();

            // CommandsSlash
            var slashCommandsConfig = client.UseSlashCommands();
            slashCommandsConfig.RegisterCommands<SlashCommandChat>();
            slashCommandsConfig.RegisterCommands<SlashCommandAdmins>();

        }
    }

}