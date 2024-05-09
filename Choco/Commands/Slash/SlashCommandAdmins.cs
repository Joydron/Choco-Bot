using ChocoLogging;
using ConfigChannels;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using ImageMagick;
using System.Net;

namespace Choco.Commands.Slash
{
    public class SlashCommandAdmins : ApplicationCommandModule
    {

        [RequireRoles(RoleCheckMode.Any, new string[] { "teet" })]
        [SlashCommand("ach", "Выдача достижения какому-либо юзеру")]
        public async Task Achievement(InteractionContext ctx,

                              [Option("member", "The member to manage their role")]
                              DiscordUser user,
#region "achievement tools"
                              [Choice("0 - Шахматы", 0)]
                              [Choice("1 - Роскомнадзор тян", 1)]
                              [Choice("2 - Пепе с вином и накаченный Пепе", 2)]
                              [Choice("3 - Лягушонок и принцесса Пузырик", 3)]
                              [Choice("4 - Кинотеатр", 4)]
                              [Choice("5 - Майнрафт", 5)]
                              [Choice("6 - Ведра с красками, художнеческое", 6)]
                              [Choice("7 - Спиртное, почти художнеческое", 7)]
                              [Choice("8 - Книги", 8)]
                              [Choice("9 - Музыка", 9)]
                              [Choice("10 - Тюрьма, СИЗО, колония строгого режима", 10)]
                              [Choice("11 - Наташа, мы все уронили, котики", 11)]
                              [Choice("12 - Клубничка, для горячестей", 12)]
                              [Choice("13 - Айтишное с синим фоном", 13)]
                              [Choice("14 - Большой торт, для кулинаров", 14)]
                              [Choice("15 - Фантазийный rpg мир", 15)]
                              [Choice("16 - Розовые сердечки", 16)]
                              [Choice("17 - Врачебный халат, для тех кому пора в дурку", 17)]
                              [Choice("18 - Деньги, доллары, для донатеров", 18)]
                              [Choice("19 - Голубой фон Zzzz для самых сонных", 19)]
                              [Choice("20 - Аниме девушка со снайперкой, вид сбоку", 20)]
                              [Choice("21 - Рисунок пингвина с грозным лицом. Автор - Теданор, рисунок с конкурса", 21)]
                              [Choice("22 - Рисунок пингвина из пикселей с сигарой с шапочкой Staff. Рисунок с конкурса", 22)]
                              //[Choice("23-Рисунок пингвина с завода на белом фоне. Рисунок с конкурса", 23)]
                              //[Choice("24-Рисунки пингвинов, один похож на репера, другой с сигарой. Рисунки с конкурса", 24)]
                              //[Choice("25-Рисунок пингвина панды. Я не буду его полностью описывать. Рисунок с конкурса", 25)]
                              //[Choice("26-Рисунок пингвина? С крыльями. Видны в основном крылья. Артхаусный какой-то стиль. Рисунок с конкурса", 26)]
                              //[Choice("27-Рисунок пингвина от Реда. Он явно устал от жизни. Рисунок с конкурса", 27)]
                              //[Choice("28-Рисунки пингвинов. У одного из них кажется запрещенный по РФ знак на повязке. Рисунок с конкурса", 28)]
                              //[Choice("29-Рисунок явно не пингвина. Темнокожий человек в черно-желтом и с вантузом в руке. Рисунок с конкурса", 29)]
                              [Choice("30-Рисунок православного Чоко. Победитель конкурса. Рисунок с конкурса", 30)]
                              [Option("background", "Значение для фона ачивки")] long bg,

                              [Choice("Красная ленточка", 0)]
                              [Choice("Розовая ленточка", 1)]
                              [Choice("Темно-Синяя ленточка", 2)]
                              [Choice("Салатовая ленточка", 3)]
                              [Choice("Голубая ленточка", 4)]
                              [Choice("Оранжевая ленточка", 5)]
                              [Choice("Темно-голубая ленточка", 6)]
                              [Choice("Сиреневая ленточка", 7)]
                              [Choice("Серая ленточка", 8)]
                              [Choice("Желтая ленточка", 9)]
                              [Choice("Зеленая ленточка", 10)]
                              [Option("ribbon", "Ленточки для достижений")] long ribbon,
# endregion
                              [Option("title", "Title of the achievement")] string? title = null,
                              [Option("description", "Description of the achievement")] string? description = null)
        {
            LogMessage.LogsCommand(ctx: ctx);

            string pathAvatar = $"./Pictures/achPictures/ava-original-{user.Id}.png";
            string pathAvatarResized = $"./Pictures/achPictures/ava-resized-{user.Id}.png";
            string pathComposed = $"./Pictures/achPictures/composed-{user.Id}.png";

            string pathBg = $"./Pictures/achPictures/bg-{bg}.png";
            string pathRibbon = $"./Pictures/achPictures/ribbon-{ribbon}.png";
            string pathIcon = $"./Pictures/achPictures/icon-{ribbon}.png";

            ulong getChannelIdSendAchievement = ConfigChannelId.GetChannelId("IdChannelSendAchievement");
            var channel = await ctx.Client.GetChannelAsync(getChannelIdSendAchievement);

            Program.Client?.Logger.LogInformation($"[Achievement.Info] - started");
            using (HttpClient client = new HttpClient())
            {
                try {
                    // load and save avatar
                    byte[] imageDataAvatar = await client.GetByteArrayAsync(user.AvatarUrl);
                    Program.Client?.Logger.LogInformation($"[Achievement.Info] - avatar loaded");
                    File.WriteAllBytes(pathAvatar, imageDataAvatar);
                    Program.Client?.Logger.LogInformation($"[Achievement.Info] - avatar saved");
                } catch (Exception e) {
                    Program.Client?.Logger.LogError($"[Achievement.Error] - can't load avatar: {e}");
                }

                // do small avatar
                using (MagickImage image = new MagickImage(pathAvatar))
                {
                    image.Resize(new MagickGeometry(300, 300));
                    image.Write(pathAvatarResized);
                }
                Program.Client?.Logger.LogInformation($"[Achievement.Info] - avatar resized");

                using (var backgroundLoad = new MagickImage(pathBg))

                // avatar and bg => compose
                using (var avatarLoad = new MagickImage(pathAvatarResized))
                {
                    backgroundLoad.Composite(avatarLoad, Gravity.Center, CompositeOperator.Over);
                    backgroundLoad.Write(pathComposed);
                }
                Program.Client?.Logger.LogInformation($"[Achievement.Info] - background image composed");

                using (var composedImageLoad = new MagickImage(pathComposed))

                // ribbon => compose
                using (var ribbonImageLoad = new MagickImage(pathRibbon))
                {
                    composedImageLoad.Composite(ribbonImageLoad, Gravity.Center, CompositeOperator.Over);
                    composedImageLoad.Write(pathComposed);
                }
                Program.Client?.Logger.LogInformation($"[Achievement.Info] - ribbon image composed");

                // send to chat achievement
                using (var fileStream = File.OpenRead(pathComposed))
                using (var iconFileStream = File.OpenRead(pathIcon))
                {
                    Program.Client?.Logger.LogInformation($"[Achievement.Info] - result opened");
                    var embed = new DiscordEmbedBuilder
                    {
                        Title = $"Открыто достижение для {user.Username}: {title}",
                        Description = description + " " + user.Mention,
                        Color = DiscordColor.Teal,
                        ImageUrl = $"attachment://achievement-cover.png",
                        Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                        {
                            Url = $"attachment://achievement-icon.png"
                        },
                        Footer = new DiscordEmbedBuilder.EmbedFooter
                        {
                            Text = $"Достижение выдал(а): {ctx.User.Username}"
                        }
                    };
                    var streams = new Dictionary<string, Stream>
                    {
                        {"achievement-cover.png", fileStream},
                        {"achievement-icon.png", iconFileStream}
                    };
                    Program.Client?.Logger.LogInformation($"[Achievement.Info] - embed created");
                    await channel.SendMessageAsync(new DiscordMessageBuilder()
                        .AddEmbed(embed)
                        .AddFiles(streams));
                    Program.Client?.Logger.LogInformation($"[Achievement.Info] - achievement sent");
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent("Done")
                        .AsEphemeral(true));
                    Program.Client?.Logger.LogInformation($"[Achievement.Info] - response sent");
                }
            }
        }
    }
}
