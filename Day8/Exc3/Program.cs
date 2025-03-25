using Exc3;
using Spectre.Console;

var manager = new PasswordManager();
        
string[] testPasswords = {
    "short",
    "goodpass123",
    "nolessdigits",
    "anotherGood1"
};

foreach (var pwd in testPasswords)
{
    try
    {
        Console.WriteLine($"Проверка пароля: {pwd}");
        manager.ValidatePassword(pwd);
        AnsiConsole.Write(new Panel($"[green bold]Пароль надежный[/]").BorderColor(Color.Green));
    }
    catch (WeakPasswordException ex)
    {
        AnsiConsole.Write(new Panel($"[red bold]Ошибка:[/] {ex.Message}").BorderColor(Color.Red));
    }
    Console.WriteLine();
}