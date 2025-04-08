using System.Diagnostics;
using Exc1.Models;
using Microsoft.EntityFrameworkCore;

namespace Exc1.Persistence;

public class TransactionRepository : ITransactionRepository
{
    private readonly FinanceDbContext _context;

    public TransactionRepository(FinanceDbContext context)
    {
        _context = context;
        _context.InitializeDatabase();
    }

    public async Task<List<TransactionModel>> GetTransactionsAsync(string userId = null, bool isAdmin = false)
    {
        var query = _context.Transactions.AsQueryable();

        if (isAdmin) return await query.OrderByDescending(t => t.Date).ToListAsync();

        if (string.IsNullOrEmpty(userId))
            return [];

        return await query.Where(t => t.UserId == userId).OrderByDescending(t => t.Date).ToListAsync();
    }

    public async Task<TransactionModel> GetTransactionByIdAsync(int id)
    {
        return await _context.Transactions.FindAsync(id);
    }

    public async Task AddTransactionAsync(TransactionModel transaction)
    {
        _context.Transactions.Add(transaction);
        await Task.CompletedTask;
    }

    public async Task UpdateTransactionAsync(TransactionModel transaction)
    {
        var existingTransaction = await _context.Transactions.FindAsync(transaction.Id);
        if (existingTransaction != null)
            _context.Entry(existingTransaction).CurrentValues.SetValues(transaction);
        else
            throw new KeyNotFoundException($"Transaction with ID {transaction.Id} not found.");

        await Task.CompletedTask;
    }

    public async Task DeleteTransactionAsync(int transactionId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId);
        if (transaction != null)
            _context.Transactions.Remove(transaction);
        else
            Debug.WriteLine($"Warning: Transaction with ID {transactionId} not found for deletion.");
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}