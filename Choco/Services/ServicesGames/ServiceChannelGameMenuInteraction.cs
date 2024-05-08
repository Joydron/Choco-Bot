using Choco.Services.ServicesLogsDiscord;
using ChocoLogging;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Choco.Services.ServicesGames
{
    public class ServiceChannelGameMenuInteraction
    {
        internal static async Task ServiceGameMenuInteraction(ComponentInteractionCreateEventArgs args)
        {
            LogMessage.LogService();
            await args.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

            var member = args.Interaction.User as DiscordMember;
            await ServicesLogsGameDiscord.ServicesLogsDiscordNovella(args, member);

            var send = await member.CreateDmChannelAsync();

            // Создаем кнопки и эмбенды для ВН
            DiscordMessage msg = await send.SendMessageAsync($"Правила чтения новеллы: Просто тыкай кнопочки, чтобы было хорошо :)" +
                                                             $"\n ------> Чтобы листать вперед" +
                                                             $"\n <------ Чтобы листать назад" +
                                                             $"\n ✷ Чтобы получить изображение новеллы" +
                                                             $"\n ⚇ Чтобы получить изображение аватарки героев");

            // Создаем кнопку
            string startButt = "game-novella-right/left,0";
            var buttRight = new DiscordButtonComponent(ButtonStyle.Secondary, startButt, "------>");

            DiscordMessage msgReplay = await msg.RespondAsync(m => m.WithContent("Предупреждение: кнопки дискорда не всегда работают корректно.\n" +
                "В случае возникновения ошибки следует подождать 3-5 секунд и нажать кнопку заново.")
                  .AddComponents(buttRight));
        }
    }
}
