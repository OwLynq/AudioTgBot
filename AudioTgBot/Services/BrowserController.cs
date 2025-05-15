using System.Diagnostics;
using WindowsInput;

public static class BrowserController // Not in use, left for future testing/temporary
{
    private static readonly InputSimulator _simulator = new InputSimulator();

    private static bool _windowOpened = false;

    public static void OpenPageInChrome(string url)
    {
        if (_windowOpened)
        {
            Console.WriteLine("The Chrome window is already open. Close it with Alt + F4.");
            CloseWindow();
        }

        Process.Start(new ProcessStartInfo
        {
            FileName = "chrome",
            Arguments = $"--new-window --start-maximized \"{url}\"", 
            UseShellExecute = true
        });

        _windowOpened = true;
        Console.WriteLine("The Chrome window is open with the URL: " + url);
        Thread.Sleep(5000); 
        _simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
    }

    public static void CloseWindow()
    {
        Console.WriteLine("Attempt to close the window using Alt + F4...");
        _simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.F4);
        Console.WriteLine("Alt + F4 command has been sent.");
    }
}