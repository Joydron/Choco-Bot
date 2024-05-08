using System;
using DSharpPlus.CommandsNext;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Choco.Database.Models.StaticDB;
using Choco.Core.Database.Logger;

namespace Choco.Core.Database
{
    public class StaticDBContext : DbContext
    {
        public virtual DbSet<AnswerMe> AnswersMe { get; set; }
        public virtual DbSet<Rpg> Rpg { get; set; }
        public virtual DbSet<Novella> Novella { get; set; }

        public StaticDBContext() : base(GetOptions("Host=postgres;Port=5432;Database=choco;Username=choco;Password=kalmari"))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseLoggerFactory(new DbLoggerFactory());
#endif
            base.OnConfiguring(optionsBuilder);
        }
    }
}