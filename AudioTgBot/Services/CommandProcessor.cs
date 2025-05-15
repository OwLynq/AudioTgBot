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
                return (string.IsNullOrEmpty(echoText) ? "Buddy, add your text after /echo!" : echoText, null);
            }

            if (messageText == "/music")
            {
                if (Config.AllowedUsers.Contains(userId))
                {
                    var buttons = GetMusicControlButtons();
                    return ("Sending you music control buttons.", buttons);
                }
                else
                {
                    return ("You do not have access to control the music.", null);
                }
            }

            if (messageText == "/sound")
            {
                if (Config.AllowedUsers.Contains(userId))
                {
                    var buttons = GetSoundButtons();
                    return ("Sending you the soundboard.", buttons);
                }
                else
                {
                    return ("You do not have access to control the soundboard!", null);
                }
            }

            return messageText switch
            {
                "/start" => ("Waiting for a command.", null),
                "/help" => ("Currently, I can respond to the following commands:\n" +
                            "/start – initial activation\n" +
                            "/echo [text] – the bot repeats your text\n" +
                            "/help – well, you’re already here... congratulations.\n" +
                            "/music – music control\n" +
                            "/sound – soundboard control", null),
                _ => ("I don’t understand this command. Try entering /help to check available commands.", null)
            };
        }

        public static InlineKeyboardMarkup GetMusicControlButtons()
        {
            return new InlineKeyboardMarkup(new[]
            {
                // First row with three buttons
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("⏮", "previous"),
                    InlineKeyboardButton.WithCallbackData("⏯", "playpause"),
                    InlineKeyboardButton.WithCallbackData("⏭", "next")
                },
                // Each following button on a new line (currently not working)
                [InlineKeyboardButton.WithCallbackData("Playlist 1 (in development)", "playlist1")],
                [InlineKeyboardButton.WithCallbackData("Playlist 2 (in development)", "playlist2")],
                [InlineKeyboardButton.WithCallbackData("Playlist 3 (in development)", "playlist3")],
                [InlineKeyboardButton.WithCallbackData("Playlist 4 (in development)", "playlist4")],
                [InlineKeyboardButton.WithCallbackData("Playlist 5 (in development)", "playlist5")]
            });
        }

        public static InlineKeyboardMarkup GetSoundButtons()
        {
            return new InlineKeyboardMarkup(
            [
                [
                   InlineKeyboardButton.WithCallbackData("Siren", "sound_siren"),
                   InlineKeyboardButton.WithCallbackData("Scream", "sound_scream")
                ],
                [
                   InlineKeyboardButton.WithCallbackData("Loading", "sound_load"),
                   InlineKeyboardButton.WithCallbackData("Explosion", "sound_boom")
                ],
                [
                   InlineKeyboardButton.WithCallbackData("Dog", "sound_dog"),
                   InlineKeyboardButton.WithCallbackData("Cat", "sound_cat")
                ]
            ]);
        }
    }
}
