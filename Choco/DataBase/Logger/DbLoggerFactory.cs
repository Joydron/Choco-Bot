using System;
using System.Collections.Generic;
using System.Text;
using Choco.Core.Database.Logger;
using DSharpPlus;
using Microsoft.Extensions.Logging;

namespace Choco.Core.Database.Logger
{
    public class DbLoggerFactory : ILoggerFactory
    {
        public void Dispose()
        {

        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger();
        }

        public void AddProvider(ILoggerProvider provider)
        {

        }
    }
}