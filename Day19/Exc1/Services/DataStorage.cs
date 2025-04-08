using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Exc1.Models;
using Newtonsoft.Json;

namespace Exc1.Services;

public class DataStorage
{
    private const string UsersFileName = "users.json";
    private static string UsersFilePath => Path.Combine(AppContext.BaseDirectory, UsersFileName);

    public List<User> LoadUsers()
    {
        try
        {
            if (File.Exists(UsersFilePath))
            {
                var json = File.ReadAllText(UsersFilePath);
                var users = JsonConvert.DeserializeObject<List<User>>(json);
                return users ?? new List<User>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading users file '{UsersFilePath}': {ex.Message}");
        }

        var defaultUsers = new List<User>
        {
            new() { Login = "admin", PasswordHash = HashPassword("admin"), Role = "Admin" }
        };
        SaveUsers(defaultUsers);
        return defaultUsers;
    }

    public void SaveUsers(List<User> users)
    {
        try
        {
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(UsersFilePath, json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving users file '{UsersFilePath}': {ex.Message}");
        }
    }


    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return string.Empty;

        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

        return Convert.ToBase64String(bytes);
    }


    public static bool VerifyPassword(string password, string storedHash)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
            return false;

        var hashOfInput = HashPassword(password);

        return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, storedHash) == 0;
    }
}