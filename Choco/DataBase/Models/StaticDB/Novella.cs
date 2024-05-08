using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Choco.Database.Models.StaticDB
{
    [Table("novella")]
    public class Novella
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("withtitle")]
        public string? WithTitle { get; set; }

        [Column("withdescription")]
        public string? WithDescription { get; set; }

        [Column("withthumbnail")]
        public string? WithThumbnail { get; set; }

        [Column("withimageurlmain")]
        public string? WithImageUrlMain { get; set; }
    }
}