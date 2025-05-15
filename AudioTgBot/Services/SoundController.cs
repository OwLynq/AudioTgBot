using System;
using NAudio.Wave;
using System.Runtime.InteropServices;

namespace AudioTgBot.Services
{
    public static class SoundController
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        private const byte VK_MEDIA_PLAY_PAUSE = 0xB3;
        private const byte VK_MEDIA_NEXT_TRACK = 0xB0;
        private const byte VK_MEDIA_PREV_TRACK = 0xB1;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        private static void PressKey(byte key)
        {
            keybd_event(key, 0, 0, 0); 
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0); 
        }

        public static void HandleMusicAction(string action)
        {
            switch (action)
            {
                case "playpause":
                    PressKey(VK_MEDIA_PLAY_PAUSE);
                    break;
                case "next":
                    PressKey(VK_MEDIA_NEXT_TRACK);
                    break;
                case "previous":
                    PressKey(VK_MEDIA_PREV_TRACK);
                    break;
            }
        }
        public static async Task HandleSoundAction(string soundName)
        {
            string? fileName = soundName switch
            {
                "siren" => "siren.mp3",
                "scream" => "scream.mp3",
                "load" => "load.mp3",
                "boom" => "boom.mp3",
                "dog" => "dog.mp3",
                "сat" => "сat.mp3",
                _ => null
            };

            if (fileName == null)
            {
                Console.WriteLine($"Unknown sound: {soundName}");
                return;
            }

            string soundPath = Path.Combine(Config.SoundFolderPath ?? throw new InvalidOperationException("Папка не была установлена."), fileName);

            if (!File.Exists(soundPath))
            {
                Console.WriteLine($"File not found: {soundPath}");
                return;
            }
            await PlaySoundAsync(soundPath);
        }
        private static async Task PlaySoundAsync(string soundPath)
        {
            try
            {
                await Task.Run(async () =>
                {
                    using var audioFile = new AudioFileReader(soundPath);
                    using var outputDevice = new WaveOutEvent();
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        await Task.Delay(100);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound: {ex.Message}");
            }
        }
    }
}
