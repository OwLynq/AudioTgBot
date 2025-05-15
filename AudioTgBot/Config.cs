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
                    Token = configData?.Token ?? throw new InvalidOperationException("Token not found in the config!");
                    SoundFolderPath = configData?.SoundFolderPath ?? throw new InvalidOperationException("Sound folder path not found in the config!");
                    AllowedUsers = configData?.AllowedUsers ?? new List<long>();

                    Console.WriteLine("[Config] Configuration successfully loaded.");
                }
                else
                {
                    throw new InvalidOperationException("Error during configuration deserialization.");
                }
            }
            else
            {
                throw new FileNotFoundException("Configuration file config.json not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Config] Error loading configuration: {ex.Message}");
            throw;
        }
    }
}

public class AppConfig
{
    public string? Token { get; set; }
    public string? SoundFolderPath { get; set; }
    public List<long>? AllowedUsers { get; set; }
}
