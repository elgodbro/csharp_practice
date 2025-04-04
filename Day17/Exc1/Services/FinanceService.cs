using Exc1.Models;

namespace Exc1.Services;

public class FinanceService
{
    private readonly DataStorage _dataStorage = new DataStorage();

    public async Task<List<TransactionModel>> LoadTransactionsAsync()
    {
        await Task.Delay(300);
        return _dataStorage.LoadTransactions();
    }

    public void SaveTransactions(List<TransactionModel> transactions)
    {
        _dataStorage.SaveTransactions(transactions);
    }
}