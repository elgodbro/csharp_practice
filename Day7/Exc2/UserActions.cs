namespace Exc2;

public delegate void UserAction(int userId);

public static class UserActions
{
    public static void PerformUserAction(int userId, UserAction action)
    {
        action(userId);
    }
    public static void BlockUser(int userId)
    {
        Console.WriteLine($"Пользователь {userId} заблокирован");
    }
    
    public static void UnblockUser(int userId)
    {
        Console.WriteLine($"Пользователь {userId} разблокирован");
    }
}