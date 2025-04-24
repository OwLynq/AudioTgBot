using Telegram.Bot.Types.ReplyMarkups;

namespace AudioTgBot.Services
{
    public static class CommandProcessor
    {
        public static (string, InlineKeyboardMarkup?) ProcessCommand(string messageText, long userId)
        {
            if (messageText.StartsWith("/echo")) 
            {
                string echoText = messageText.Substring(5).Trim(); 
                return (string.IsNullOrEmpty(echoText) ? "Пользователь добавь текст после /echo!" : echoText, null);
            }

            if (messageText == "/music")
            {
                if (Config.AllowedUsers.Contains(userId))
                {
                    var buttons = GetMusicControlButtons();
                    return ("Отправляю тебе кнопки Пользователь.", buttons);
                }
                else
                {
                    return ("Пользователь, у тебя нет доступа к управлению Музыкой.", null);
                }
            }

            if (messageText == "/sound")
            {
                if (Config.AllowedUsers.Contains(userId))
                {
                    var buttons = GetSoundButtons();
                    return ("Отправляю саундбар.", buttons);
                }
                else
                {
                    return ("Пользователь, у тебя нет доступа к управлению Саундбаром!", null);
                }
            }

            return messageText switch
            {
                "/start" => ("Жду команды.", null),
                "/help" => ("В данный момент могу отвечать на команды:\n" +
                            "/start – первичная активация\n" +
                            "/echo [текст] – Бот повторяет за тобой\n" +
                            "/help – ну, ты уже тут… поздравляю.\n" +
                            "/music – управление Музыкой\n" +
                            "/sound – управление Саунд-баром", null),
                _ => ("Пользователь, я не понимаю этой команды. Попробуй ввести /help, чтобы проверить доступные команды.", null)
            };
        }
        public static InlineKeyboardMarkup GetMusicControlButtons()
        {
            return new InlineKeyboardMarkup(new[]
            {
                // Первая строка с тремя кнопками
                new InlineKeyboardButton[]
                {
                InlineKeyboardButton.WithCallbackData("⏮", "previous"),
                InlineKeyboardButton.WithCallbackData("⏯", "playpause"),
                InlineKeyboardButton.WithCallbackData("⏭", "next")
                },
                // Каждая из следующих кнопок на новой строке (cейчас не работает)
                [InlineKeyboardButton.WithCallbackData("Плейлист 1 (в разработке)", "playlist1")],
                [InlineKeyboardButton.WithCallbackData("Плейлист 2 (в разработке)", "playlist2")],
                [InlineKeyboardButton.WithCallbackData("Плейлист 3 (в разработке)", "playlist3")],
                [InlineKeyboardButton.WithCallbackData("Плейлист 4 (в разработке)", "playlist4")],
                [InlineKeyboardButton.WithCallbackData("Плейлист 5 (в разработке)", "playlist5")]
            });
        }
        public static InlineKeyboardMarkup GetSoundButtons()
        {
            return new InlineKeyboardMarkup(
            [
                    [
                       InlineKeyboardButton.WithCallbackData("Сирена", "sound_siren"),
                       InlineKeyboardButton.WithCallbackData("Крик", "sound_scream")
                    ],
                    [
                       InlineKeyboardButton.WithCallbackData("Загрузка", "sound_load"),
                       InlineKeyboardButton.WithCallbackData("Взрыв", "sound_boom")
                    ],
                    [
                       InlineKeyboardButton.WithCallbackData("Собака", "sound_dog"),
                       InlineKeyboardButton.WithCallbackData("Кот", "sound_сat")
                    ]
            ]);
        }
    }
}
