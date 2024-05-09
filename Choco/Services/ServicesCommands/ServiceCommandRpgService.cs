using DSharpPlus.Entities;
using System.IO;
using System.Threading.Tasks;
using Choco.Database.Models.StaticDB;
using ChocoLogging;
using System.Reflection;

namespace Choco.Services.ServicesCommands
{
    public static class ServiceCommandRpgService
    {

        public static DiscordEmbedBuilder ServiceCreateEmbedWithCover(Rpg rpgEntry)
        {
            return new DiscordEmbedBuilder
            {
                Title = rpgEntry.name,
                Description = $"**Разработчик:** {rpgEntry.developer}\n" +
                              $"**Год релиза:** {rpgEntry.year}\n" +
                              $"**Платформа:** {rpgEntry.platform}\n" +
                              $"**Жанр:** {rpgEntry.genres}\n" +
                              $"**Движок:** {rpgEntry.engine}\n" +
                              $"**Автор:** {rpgEntry.author}\n" +
                              $"=============================================\n" +
                              $"**Описание**: {rpgEntry.description}\n",
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"Подробнее: {rpgEntry.resource}",
                },
                ImageUrl = "attachment://image.png",
                Color = DiscordColor.Teal
            };
        }
        public static Dictionary<string, DiscordEmbedBuilder> ServiceCreateEmbeds()
        {
            LogMessage.LogService();

            return new Dictionary<string, DiscordEmbedBuilder>
            {
                { "pic1", new DiscordEmbedBuilder { ImageUrl = "attachment://pic1.png",
                                                    Color = DiscordColor.Teal } },
                { "pic2", new DiscordEmbedBuilder { ImageUrl = "attachment://pic2.png",
                                                    Color = DiscordColor.Teal } },
                { "pic3", new DiscordEmbedBuilder { ImageUrl = "attachment://pic3.png",
                                                    Color = DiscordColor.Teal } }
            };
        }

        public static Stream ServiceGetStreamFromFile(string filePath)
        {
            LogMessage.LogService();

            return File.Open(filePath, FileMode.Open);
        }

    }

    public static class LoadingService
    {
        public static async Task ServiceUpdateLoadingMessage(DiscordMessage loadingMessage, int step)
        {
            LogMessage.LogService();

            const int maxLoadingChars = 16;
            string loadingBar = "[";
            for (int i = 0; i < step && i < maxLoadingChars; i++)
            {
                loadingBar += "⏭";
            }
            for (int i = step; i < maxLoadingChars; i++)
            {
                loadingBar += "-";
            }
            loadingBar += "]";

            await loadingMessage.ModifyAsync($"{loadingBar}");
        }
    }
}
