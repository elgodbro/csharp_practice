using Exc1;

var session1 = SessionManager.Instance;
session1.Login("Копатыч");

var session2 = SessionManager.Instance;
Console.WriteLine($"Текущий пользователь: {session2.GetCurrentUser()}");

session1.Login("Бараш");

session1.Logout();

Console.WriteLine($"Текущий пользователь: {session2.GetCurrentUser()}");