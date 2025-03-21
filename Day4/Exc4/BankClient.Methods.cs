namespace Exc4;

public partial class BankClient
{
    public bool IsLowBalance(decimal minBalance)
    {
        return AccountBalance < minBalance;
    }
}