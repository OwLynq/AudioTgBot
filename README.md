# AudioTgBot — A Simple Audio Controller Bot for Telegram

## Description
AudioTgBot is a Telegram bot for managing music playback. It allows the user to play music on a PC, switch tracks, and launch audio files from a folder.

## 🎵 Features

- **Track Switching** – Switch between tracks using inline buttons.
- **File Playback** – Play local audio files stored on your PC.
- **Simple Interface** – Control everything with Telegram buttons.

## 🛠️ Technologies Used

- **C# / .NET** – Core logic and bot implementation.
- **Telegram Bot API** – For creating and managing the bot.

## 🚀 How to Run

1. Clone the repository.
2. Open the project in Visual Studio.
3. Install dependencies:
   - Open **NuGet Package Manager**.
   - Install `Telegram.Bot`.
4. Create a bot using [@BotFather](https://t.me/BotFather) and copy the token.
5. Configure the `config.json` file with your bot token, music folder path, and allowed user IDs.
6. Run the project and start chatting with your bot in Telegram.

## 📁 Example `config.json`

```json
{
  "Token": "your-bot-token",  
  "SoundFolderPath": "C:\\Music\\Folder",  
  "AllowedUsers": [123456789, 987654321]
}



