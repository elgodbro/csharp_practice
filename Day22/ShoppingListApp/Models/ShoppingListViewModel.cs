namespace ShoppingListApp.Models;

public class ShoppingListViewModel
{
    public List<ShoppingItem> ItemsNotBought { get; set; } = new List<ShoppingItem>();
    public List<ShoppingItem> ItemsBought { get; set; } = new List<ShoppingItem>();
    public ShoppingItemViewModel NewItem { get; set; }
}