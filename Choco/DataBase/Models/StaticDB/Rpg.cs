using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Choco.Database.Models.StaticDB
{
    [Table("rpg")]
    public class Rpg
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("resource")]
        public string? resource { get; set; }
        [Column("name")]
        public string? name { get; set; }
        [Column("developer")]
        public string? developer { get; set; }
        [Column("year")]
        public string? year { get; set; }
        [Column("platform")]
        public string? platform { get; set; }
        [Column("genres")]
        public string? genres { get; set; }
        [Column("engine")]
        public string? engine { get; set; }
        [Column("author")]
        public string? author { get; set; }
        [Column("tag")]
        public string? tag { get; set; }
        [Column("description")]
        public string? description { get; set; }
        [Column("cover")]
        public string? cover { get; set; }
        [Column("pic1")]
        public string? pic1 { get; set; }
        [Column("pic2")]
        public string? pic2 { get; set; }
        [Column("pic3")]
        public string? pic3 { get; set; }
        [Column("eng")]
        public string? eng { get; set; }
        [Column("rus")]
        public string? rus { get; set; }
        [Column("jpn")]
        public string? jpn { get; set; }
    }
}
