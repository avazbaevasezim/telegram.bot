using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

class Program
{
    static async Task Main(string[] args)
    {
        // Замените <Your_Token_Here> на токен вашего бота
        string token = "7233772468:AAFbHFMZGiuUABUPxb8dutj6SzQmruE4FK0";
        var botClient = new TelegramBotClient(token);

        // Вывод информации о боте
        var botInfo = await botClient.GetMeAsync();
        Console.WriteLine($"Запущен бот @{botInfo.Username}");

        // Настройка обновлений
        using var cts = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<U
        // Запуск бота
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token
        );

        Console.WriteLine("Нажмите любую клавишу для остановки бота...");
        Console.ReadKey();

        // Остановка бота
        cts.Cancel();
    }

    // Обработка сообщений
    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message || message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;
        Console.WriteLine($"Получено сообщение: '{messageText}' в чате {chatId}");

        // Ответ на сообщение
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Вы написали: {messageText}",
            cancellationToken: cancellationToken
        );
    }

    // Обработка ошибок
    static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Произошла ошибка: {exception.Message}");
        return Task.CompletedTask;
    }
}