namespace Exc2;

public class Chef(string name)
{
    public string Name { get; set; } = name;

    public void Cook()
    {
        Console.WriteLine($"Повар {Name} готовит");
    }
}