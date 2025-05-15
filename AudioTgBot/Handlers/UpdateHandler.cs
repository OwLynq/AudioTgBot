using Telegram.Bot.Types;
using Telegram.Bot;
using AudioTgBot.Services;


namespace AudioTgBot.Handlers
{
    public static class UpdateHandler
    {
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                long chatId = (update.Message?.Chat?.Id ?? update.CallbackQuery?.Message?.Chat?.Id) ?? 0;
                long userId = (update.Message?.From?.Id ?? update.CallbackQuery?.From?.Id) ?? 0;

                if (chatId == 0 || userId == 0) return;

                if (update.Message is { } message && message.Text is { } messageText) 
                {
                    Console.WriteLine($"The bot received a message in the chat: {chatId}: {messageText}");

                    var (responseText, buttons) = CommandProcessor.ProcessCommand(messageText, userId); 

                    if (buttons != null)
                    {
                        await MessageSender.SendMessageWithButtons(botClient, chatId, responseText, buttons, cancellationToken);
                    }
                    else
                    {
                        await MessageSender.SendMessage(botClient, chatId, responseText, cancellationToken);
                    }
                }
                else if (update.CallbackQuery is { } callbackQuery)
                {
                    string action = callbackQuery.Data ?? string.Empty;
                    Console.WriteLine($"User {userId} pressed the button: {action}");
                    if (!Config.AllowedUsers.Contains(userId))
                    {
                        await botClient.AnswerCallbackQuery(callbackQuery.Id, "Buddy, how did you get here!?");
                        return;
                    }

                    
                    if (action.StartsWith("sound_"))
                    {
                        _ = SoundController.HandleSoundAction(action.Substring(6)); 
                    }
                    else
                    {
                        SoundController.HandleMusicAction(action); 
                    }

                    await botClient.AnswerCallbackQuery(callbackQuery.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update processing error: {ex.Message}");
            }
        }
    }
}
