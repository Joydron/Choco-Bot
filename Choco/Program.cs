using Choco.Commands.Prefix;
using Choco.Commands.Slash;
using Choco.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Choco.Core.Database.Logger;
using Choco.Core.Database;
using Npgsql;
using Choco.Services;
using Microsoft.Extensions.DependencyInjection;
using Choco.Commands;
using Choco.Services.ServicesMessage;
using Serilog;
using Serilog.Debugging;
using Serilog.Filters;

namespace Choco
{
    public sealed class Program
    {
        public static DiscordClient Client { get; set; }
        public static CommandsNextExtension Commands { get; set; }
        public ComponentInteractionCreateEventArgs ComponentInteractionCreated { get; private set; }

        static async Task Main(string[] args)
        {
            // Create Logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] - {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            var logFactory = new LoggerFactory().AddSerilog();

            // Create Configuration
            var configProperties = new ConfigJsonReader();
            await configProperties.ReadJSON();

            // Set Configuration
            var discordConfig = new DiscordConfiguration
            {
                Intents = DiscordIntents.All,
                Token = configProperties.discordToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LoggerFactory = logFactory

            };
            Client = new DiscordClient(discordConfig);

            // Commands
            var serviceProvider = ServicesManagement.SetupServices(Client);
            // Services
            CommandManager.SetupCommands(Client, configProperties.discordPrefix, serviceProvider);


            // Discord Gateway
            await Client.ConnectAsync();

            await Task.Delay(-1);

        }

    }
}