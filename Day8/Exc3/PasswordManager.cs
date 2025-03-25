namespace Exc3;

public class PasswordManager
{
    public void ValidatePassword(string password)
    {
        if (password.Length < 8)
            throw new WeakPasswordException("Пароль должен содержать минимум 8 символов");
        
        if (!password.Any(char.IsDigit))
            throw new WeakPasswordException("Пароль должен содержать хотя бы одну цифру");
    }
}