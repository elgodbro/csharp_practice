using Microsoft.AspNetCore.Mvc;
using ShoppingListApp.Models;

namespace ShoppingListApp.Controllers;

public class ShoppingController : Controller
{
    private static List<ShoppingItem> items = new();
    private static int nextId = 1;
    
    public IActionResult Index()
    {
        var model = new ShoppingListViewModel
        {
            ItemsNotBought = items.Where(i => !i.Bought).ToList(),
            ItemsBought = items.Where(i => i.Bought).ToList()
        };
        return View(model);
    }
    
    [Route("Shopping/Mark/{id}")]
    public IActionResult Mark(int id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item != null)
            item.Bought = !item.Bought;
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Add(string name, int quantity)
    {
        items.Add(new ShoppingItem
        {
            Id = nextId++,
            Name = name,
            Quantity = quantity,
            Bought = false
        });
        return RedirectToAction("Index");
    }
}