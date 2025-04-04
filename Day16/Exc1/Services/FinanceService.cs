using System.Collections.ObjectModel;
using Exc1.Models;

namespace Exc1.Services;

public class FinanceService
{
    public async Task<ObservableCollection<TransactionModel>> LoadTransactionsAsync()
    {
        await Task.Delay(3000);

        var transactions = new ObservableCollection<TransactionModel>
        {
            new() { Date = DateTime.Now.AddDays(-2), Category = "Зарплата", Amount = 500, Type = "Доход" },
            new() { Date = DateTime.Now.AddDays(-1), Category = "Продукты", Amount = 30, Type = "Расход" },
            new() { Date = DateTime.Now, Category = "Развлечения", Amount = 15, Type = "Расход" }
        };

        return transactions;
    }
}