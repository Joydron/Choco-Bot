using ChocoLogging;
using ConfigChannels;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;

public class ChannelMessageManager
{
    private readonly DiscordClient _client;
    private readonly ulong _channelId;
    private readonly Dictionary<ulong, DiscordMessageBuilder> _messageBuilders = new();

    public ChannelMessageManager(DiscordClient client, ulong channelId)
    {
        _client = client;
        _channelId = channelId;
    }
    private async Task UpdateMessagesInChannel(DiscordChannel channel)
    {
        try
        {
            foreach (var messageBuilder in _messageBuilders)
            {
                var message = await channel.GetMessageAsync(messageBuilder.Key);
                await message.ModifyAsync(messageBuilder.Value);
            }
        }
        catch (BadRequestException e)
        {
            Console.WriteLine($"error: {e.Errors}");
            Console.WriteLine($"error: {e.Source}");
            Console.WriteLine($"error: {e.JsonMessage}");
        }

    }

    public async Task ServiceUpdateMessagesAsync()
    {
        LogMessage.LogService();

        // Получаем все сообщения и канал
        ulong getChanneldGameUpdateMenu = ConfigChannelId.GetChannelId("IdChannelGameUpdateMenu");
        var channel = await _client.GetChannelAsync(getChanneldGameUpdateMenu);
        var messages = await channel.GetMessagesAsync();

        // Создаем DiscordMessageBuilder для каждого сообщения и сохраняем их в словаре
        foreach (var message in messages)
        {
            _messageBuilders[message.Id] = new DiscordMessageBuilder();
        }

        // Получаем ключи словаря в обратном порядке
        var reversedKeys = _messageBuilders.Keys.Reverse();

        // Обновляем содержимое 1-3 сообщений
        UpdateFirstMessage(reversedKeys.First());
        UpdateSecondMessage(reversedKeys.Skip(1).First());
        UpdateThirdMessage(reversedKeys.Skip(2).First());

        // Обновляем сообщения в канале
        await UpdateMessagesInChannel(channel);
    }

    private void UpdateFirstMessage(ulong messageId)
    {
        _messageBuilders[messageId].WithContent("Приветствую тебя дорогой житель крепости-ОРЕХ! \r" +
            "\n\r\nДанный канал создан для игр. " + 
            "\r\nНиже ты можешь попробовать пройти визуальную новеллу. " +
            "\r\nДля ее прочтения личные сообщения должны быть открыты." +
            "\r\nНажми на кнопку, чтобы начать читать!");
    }

    private void UpdateSecondMessage(ulong messageId)
    {
        var embSaharokTitle = new DiscordEmbedBuilder
        {
            ImageUrl = "https://cdn.discordapp.com/attachments/1096410743395586069/1133098257866825848/cover.png",
            Color = DiscordColor.Goldenrod
        };

        var embSaharok = new DiscordEmbedBuilder
        {
            Title = "Приключение принцессы Сахарок",
            ImageUrl = "https://cdn.discordapp.com/attachments/1074359263536885783/1076717642062237716/04_4.png",
            Description = $"Небольшая сказка, в которой принцесса Сахарок отправилась в поиски лучшего дворца",
            Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
            {
                Url = "https://cdn.discordapp.com/attachments/1096410743395586069/1134858614834733096/ico.png"
            },
            Footer = new DiscordEmbedBuilder.EmbedFooter
            {
                Text = $"Автор: Houron"
            },
            Color = DiscordColor.Goldenrod
    };

        _messageBuilders[messageId]
            .AddEmbed(embSaharokTitle)
            .AddEmbed(embSaharok)
            .AddComponents(new DiscordButtonComponent(ButtonStyle.Secondary, "menu", "Начать читать сказку!"));
    }

    private void UpdateThirdMessage(ulong messageId)
    {
        TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
        DateTime moscowDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, moscowTimeZone);
        string time = moscowDateTime.ToString();

        _messageBuilders[messageId].WithContent(
            $"``` Last update Choco: {time}```");

    }

}