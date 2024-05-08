using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Choco.Database.Models.StaticDB
{
    [Table("me")]
    public class AnswerMe 
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("goodwords")]
        public string? goodwords { get; set; }
    }
}
