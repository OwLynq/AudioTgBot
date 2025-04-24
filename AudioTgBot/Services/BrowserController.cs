using System.Diagnostics;
using WindowsInput;

public static class BrowserController // Не используется, оставлен для теста на будущее/временный
{
    private static readonly InputSimulator _simulator = new InputSimulator();

    private static bool _windowOpened = false;

    public static void OpenPageInChrome(string url)
    {
        if (_windowOpened)
        {
            Console.WriteLine("Окно Chrome уже открыто. Закрываем его с помощью Alt + F4.");
            CloseWindow();
        }

        Process.Start(new ProcessStartInfo
        {
            FileName = "chrome",
            Arguments = $"--new-window --start-maximized \"{url}\"", 
            UseShellExecute = true
        });

        _windowOpened = true;
        Console.WriteLine("Окно Chrome открыто с URL: " + url);
        Thread.Sleep(5000); 
        _simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
    }

    public static void CloseWindow()
    {
        Console.WriteLine("Попытка закрыть окно с помощью Alt + F4...");
        _simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.F4);
        Console.WriteLine("Команда Alt + F4 отправлена.");
    }
}