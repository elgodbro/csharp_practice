namespace Exc2;

public class Restaurant(string name, Chef[] chefs, Supplier supplier)
{
    public string Name { get; set; } = name;
    public Chef[] Chefs { get; set; } = chefs;
    public Menu Menu { get; set; } = new();
    public Supplier Supplier { get; set; } = supplier;

    public void PrepareDishes()
    {
        Console.WriteLine($"Ресторан {Name} готовит блюда:");
        foreach (var chef in Chefs)
        {
            chef.Cook();
        }
    }
}