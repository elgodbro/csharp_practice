namespace Exc4;

public partial class BankClient
{
    public string Name { get; set; } = Faker.Name.FullName();
    public decimal AccountBalance { get; set; } = Faker.Number.RandomNumber(100, 1000);

    public override string ToString()
    {
        return $"{Name}: {AccountBalance} BYN";
    }
}