using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using AudioTgBot.Handlers;

namespace AudioTgBot
{
    class Program
    {

        private static readonly TelegramBotClient BotClient = new(Config.Token ?? throw new InvalidOperationException("The token was not found in the configuration.")); 

        static async Task Main()
        {
            Console.WriteLine("The bot is starting...");

            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions // (Polling)
            {
                AllowedUpdates = Array.Empty<UpdateType>(), 
                DropPendingUpdates = true 
            };

            BotClient.StartReceiving(UpdateHandler.HandleUpdateAsync, ErrorHandler.HandleErrorAsync, receiverOptions, cts.Token);

            Console.WriteLine("The bot is running...");
            await Task.Delay(-1);
        }
    }
}
