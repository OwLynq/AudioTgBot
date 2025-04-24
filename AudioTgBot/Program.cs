using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using AudioTgBot.Handlers;

namespace AudioTgBot
{
    class Program
    {

        private static readonly TelegramBotClient BotClient = new(Config.Token ?? throw new InvalidOperationException("Токен не был найден в конфигурации.")); 

        static async Task Main()
        {
            Console.WriteLine("Бот запускается...");

            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions // (Polling)
            {
                AllowedUpdates = Array.Empty<UpdateType>(), 
                DropPendingUpdates = true 
            };

            BotClient.StartReceiving(UpdateHandler.HandleUpdateAsync, ErrorHandler.HandleErrorAsync, receiverOptions, cts.Token);

            Console.WriteLine("Бот запущен...");
            await Task.Delay(-1);
        }
    }
}
