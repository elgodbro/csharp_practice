namespace Exc2;

public class Menu
{
    private string[] Dishes { get; set; } = ["Суп", "Салат", "Стейк"];

    public void ShowMenu()
    {
        Console.WriteLine("Меню ресторана:");
        foreach (var dish in Dishes)
        {
            Console.WriteLine("- " + dish);
        }
    }
}