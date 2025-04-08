using Exc1.Models;

namespace Exc1.Persistence;

public interface ITransactionRepository
{
    Task<List<TransactionModel>> GetTransactionsAsync(string userId = null, bool isAdmin = false);
    Task<List<Category>> GetCategoriesAsync();
    Task<TransactionModel> GetTransactionByIdAsync(int id);
    Task AddTransactionAsync(TransactionModel transaction);
    Task UpdateTransactionAsync(TransactionModel transaction);
    Task DeleteTransactionAsync(int transactionId);
    Task<int> SaveChangesAsync();
}