namespace Exc1;

public class SessionManager
{
    private static readonly Lazy<SessionManager> _instance = new Lazy<SessionManager>(() => new SessionManager());
    private string? _currentUser;
    
    private SessionManager() 
    {
        _currentUser = null;
    }
    
    public static SessionManager Instance => _instance.Value;
    
    public void Login(string? user)
    {
        if (string.IsNullOrWhiteSpace(user))
            throw new ArgumentException("Имя пользователя не может быть пустым");

        if (_currentUser != null)
            Console.WriteLine("Пользователь уже авторизован");
        else
        {
            _currentUser = user;
            Console.WriteLine($"Пользователь {user} вошел в систему");
        }
    }
    
    public void Logout()
    {
        if (_currentUser == null) return;
        
        Console.WriteLine($"Пользователь {_currentUser} вышел из системы");
        _currentUser = null;
    }
    
    public string? GetCurrentUser() => string.IsNullOrEmpty(_currentUser) ? "Нет активного пользвоателя" : _currentUser;
}