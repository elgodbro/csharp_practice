using Exc2;

var userId = 12345;

Console.WriteLine("Блокировка пользователя:");
UserActions.PerformUserAction(userId, UserActions.BlockUser);
        
Console.WriteLine("\nРазблокировка пользователя:");
UserActions.PerformUserAction(userId, UserActions.UnblockUser);