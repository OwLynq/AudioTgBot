using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class Config
{
    public static string? Token { get; private set; }
    public static List<long>? AllowedUsers { get; private set; }
    public static string? SoundFolderPath { get; private set; }

    private const string ConfigFilePath = "config.json"; 

    static Config()
    {
        LoadConfig();
    }

    // Метод для загрузки данных из config.json
    private static void LoadConfig()
    {
        try
        {
            if (File.Exists(ConfigFilePath))
            {
                var json = File.ReadAllText(ConfigFilePath);
                var configData = JsonSerializer.Deserialize<AppConfig>(json);

                if (configData != null)
                {
                    Token = configData?.Token ?? throw new InvalidOperationException("Токен не найден в конфиге!");
                    SoundFolderPath = configData?.SoundFolderPath ?? throw new InvalidOperationException("Путь к папке звуков не найден в конфиге!");
                    AllowedUsers = configData?.AllowedUsers ?? new List<long>();

                    Console.WriteLine("[Config] Конфигурация успешно загружена.");
                }
                else
                {
                    throw new InvalidOperationException("Ошибка при десериализации конфигурации.");
                }
            }
            else
            {
                throw new FileNotFoundException("Конфигурационный файл config.json не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Config] Ошибка при загрузке конфигурации: {ex.Message}");
            throw;
        }
    }
}

// Класс для десериализации конфигурации
public class AppConfig
{
    public string? Token { get; set; }
    public string? SoundFolderPath { get; set; }
    public List<long>? AllowedUsers { get; set; }
}
