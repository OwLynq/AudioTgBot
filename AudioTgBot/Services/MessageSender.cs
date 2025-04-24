using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace AudioTgBot.Services
{
    public static class MessageSender
    {
        public static async Task SendMessage(ITelegramBotClient botClient, long chatId, string responseText, CancellationToken cancellationToken)
        {
            await botClient.SendMessage(chatId, responseText, cancellationToken: cancellationToken);
        }
        public static async Task SendMessageWithButtons(ITelegramBotClient botClient, long chatId, string responseText, InlineKeyboardMarkup buttons, CancellationToken cancellationToken)
        {
            await botClient.SendMessage(chatId, responseText, replyMarkup: buttons, cancellationToken: cancellationToken);
        }
    }
}
