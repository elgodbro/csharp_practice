namespace Exc2;

public class City
{
    public string Name = Faker.Address.State();
    public int Population = Faker.Number.RandomNumber(10000, 10000000);
}