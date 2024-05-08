using Choco.Core.Database;
using ChocoLogging;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace Choco.Commands.Prefix
{
    public class PrefixCommandChat : BaseCommandModule
    {
        private static readonly Random random = new Random();

        [Command("me?")]
        public async Task CommandMe(CommandContext ctx)
        {
            LogMessage.LogsCommand(ctx: ctx);
            string response = string.Empty;

            using (var databaseContext = new StaticDBContext())
            {
                var maxId = await databaseContext.AnswersMe.MaxAsync(x => x.Id);

                var randomId = random.Next(1, maxId + 1);

                response = await databaseContext.AnswersMe
                    .Where(answer => answer.Id == randomId)
                    .Select(answer => answer.goodwords)
                    .FirstOrDefaultAsync() ?? "default";

                await ctx.Channel.SendMessageAsync(ctx.User.Mention + " " + response);

            }
        }
    }
}
