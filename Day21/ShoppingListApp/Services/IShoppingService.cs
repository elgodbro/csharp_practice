using ShoppingListApp.Models;

namespace ShoppingListApp.Services;

public interface IShoppingService
{
    void AddItem(ShoppingItem item);
    void RemoveItem(int id);
    void ToggleBought(int id);
    List<ShoppingItem> GetItems();
}
