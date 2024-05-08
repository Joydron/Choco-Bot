using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using ChocoLogging;
using Serilog.Events;
using DSharpPlus;
using Microsoft.Extensions.Logging;

namespace Choco.Core.Database.Logger
{
    public class DbLogger : ILogger
    {
        protected static readonly EventId BotEventId = new(0110, "Choco");

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Program.Client.Logger.LogError($"[Postgress.Error] - {state}");
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}