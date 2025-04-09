using ShoppingListApp.Models;

namespace ShoppingListApp.Services;

public class ShoppingService : IShoppingService
{
    private List<ShoppingItem> items = new();
    private int nextId = 1;

    public void AddItem(ShoppingItem item)
    {
        item.Id = nextId++;
        items.Add(item);
    }

    public void RemoveItem(int id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item != null)
            items.Remove(item);
    }

    public void ToggleBought(int id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item != null)
            item.Bought = !item.Bought;
    }

    public List<ShoppingItem> GetItems() => items;
}