namespace ShoppingListApp.Models;

public class ShoppingListViewModel
{
    public List<ShoppingItem> ItemsNotBought { get; set; }
    public List<ShoppingItem> ItemsBought { get; set; }
}