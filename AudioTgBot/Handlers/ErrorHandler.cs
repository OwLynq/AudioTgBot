using Telegram.Bot;

namespace AudioTgBot.Handlers
{
    public static class ErrorHandler
    {
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"The bot encountered an exception: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
