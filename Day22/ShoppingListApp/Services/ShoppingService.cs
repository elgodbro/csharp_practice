using ShoppingListApp.Data;
using ShoppingListApp.Models;

namespace ShoppingListApp.Services;

public class ShoppingService : IShoppingService
{
    private readonly AppDbContext _context;

    public ShoppingService(AppDbContext context)
    {
        _context = context;
    }

    public void AddItem(ShoppingItem item)
    {
        _context.ShoppingItems.Add(item);
        _context.SaveChanges();
    }

    public void RemoveItem(int id)
    {
        var item = _context.ShoppingItems.Find(id);
        if (item != null)
        {
            _context.ShoppingItems.Remove(item);
            _context.SaveChanges();
        }
    }

    public void ToggleBought(int id)
    {
        var item = _context.ShoppingItems.Find(id);
        if (item == null) return;
        item.IsBought = !item.IsBought;
        _context.SaveChanges();
    }

    public List<ShoppingItem> GetItems() => 
        _context.ShoppingItems.OrderBy(i => i.IsBought).ToList();
}