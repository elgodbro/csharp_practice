namespace Exc2;

public class Supplier(string name)
{
    public string Name { get; set; } = name;

    public void SupplyProducts()
    {
        Console.WriteLine($"Поставщик {Name} поставляет продукты");
    }
}