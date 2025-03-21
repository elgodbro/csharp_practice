namespace Exc4;

public class Bank(BankClient[] clients)
{
    public BankClient[] GetClientsWithLowBalance(decimal minBalance)
    {
        return clients.Where(c => c.IsLowBalance(minBalance)).ToArray();
    }
    
    public BankClient GetRichestClient()
    {
        return clients.OrderByDescending(c => c.AccountBalance).FirstOrDefault();
    }
}