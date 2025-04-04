using System.Collections.Generic;
using System.IO;
using Exc1.Models;
using Newtonsoft.Json;

namespace Exc1.Services;

public class DataStorage
{
    private const string UsersFile = "users.json";
    private const string TransactionsFile = "finance.json";

    public List<User> LoadUsers()
    {
        if (File.Exists(UsersFile))
        {
            var json = File.ReadAllText(UsersFile);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }
        var users = new List<User> { new User { Login = "admin", PasswordHash = HashPassword("admin"), Role = "Admin" } };
        SaveUsers(users);
        return users;
    }

    public void SaveUsers(List<User> users)
    {
        var json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(UsersFile, json);
    }

    public List<TransactionModel> LoadTransactions()
    {
        if (File.Exists(TransactionsFile))
        {
            var json = File.ReadAllText(TransactionsFile);
            return JsonConvert.DeserializeObject<List<TransactionModel>>(json);
        }
        return new List<TransactionModel>();
    }

    public void SaveTransactions(List<TransactionModel> transactions)
    {
        var json = JsonConvert.SerializeObject(transactions, Formatting.Indented);
        File.WriteAllText(TransactionsFile, json);
    }

    public static string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}