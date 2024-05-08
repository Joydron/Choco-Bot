using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;
using Serilog;
using System.Runtime.CompilerServices;

namespace ChocoLogging;

public class LogMessage
{
    protected static readonly EventId BotEventId = new(0110, "Choco");

    //CommandsLogging
    public static void LogsCommand(object ctx, [CallerMemberName] string methodLogs = "")
    {
        if (ctx is CommandContext commandContext)
        {
            Log.Information("[{SourceContext}] - Command executed: [{MethodName}] by {Username} ({UserId}) in {Channel} ({ChannelId})", typeof(LogMessage).FullName, methodLogs, commandContext.User.Username, commandContext.User.Id, commandContext.Channel.Name, commandContext.Channel.Id);
        }
        else if (ctx is InteractionContext interactionContext)
        {
            Log.Information("[{SourceContext}] - Command executed: [{MethodName}] by {Username} ({UserId}) in {Channel} ({ChannelId})", typeof(LogMessage).FullName, methodLogs, interactionContext.User.Username, interactionContext.User.Id, interactionContext.Channel.Name, interactionContext.Channel.Id);
        }
    }

    //HandlersLogging
    public static void LogHandler([CallerMemberName] string methodLogs = "")
    {
        Log.Information("[{SourceContext}] - Handler executed: [{MethodName}]", typeof(LogMessage).FullName, methodLogs);
    }

    //ServicesLogging
    public static void LogService([CallerMemberName] string methodLogs = "")
    {
        Log.Information("[{SourceContext}] - Service executed: [{MethodName}]", typeof(LogMessage).FullName, methodLogs);
    }

    //OtherLogging
    public static void LogOther([CallerMemberName] string methodLogs = "")
    {
        Log.Information("[{SourceContext}] - Other executed: [{MethodName}]", typeof(LogMessage).FullName, methodLogs);
    }
}
    
