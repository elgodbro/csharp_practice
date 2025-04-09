using Microsoft.AspNetCore.Mvc;
using ShoppingListApp.Models;
using ShoppingListApp.Services;

namespace ShoppingListApp.Controllers;

public class ShoppingController : Controller
{
    private readonly IShoppingService _shoppingService;

    public ShoppingController(IShoppingService shoppingService)
    {
        _shoppingService = shoppingService;
    }

    public IActionResult Index()
    {
        var items = _shoppingService.GetItems();
        var model = new ShoppingListViewModel
        {
            ItemsNotBought = items.Where(i => !i.Bought).ToList(),
            ItemsBought = items.Where(i => i.Bought).ToList(),
            NewItem = new ShoppingItemViewModel()
        };
        return View(model);
    }

    [Route("Shopping/Mark/{id}")]
    public IActionResult Mark(int id)
    {
        _shoppingService.ToggleBought(id);
        TempData["Message"] = $"Состояние товара изменено";
        TempData["MessageType"] = "info";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Add([FromForm] ShoppingListViewModel model)
    {
        if (ModelState.IsValid)
        {
            var item = new ShoppingItem
            {
                Name = model.NewItem.Name,
                Quantity = model.NewItem.Quantity,
                Bought = false
            };
            _shoppingService.AddItem(item);
            TempData["Message"] = $"Товар {item.Name} ({item.Quantity} шт.) добавлен";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index");
        }
        var items = _shoppingService.GetItems();
        var viewModel = new ShoppingListViewModel
        {
            ItemsNotBought = items.Where(i => !i.Bought).ToList(),
            ItemsBought = items.Where(i => i.Bought).ToList(),
            NewItem = model.NewItem
        };
        return View("Index", viewModel);
    }
        
    [Route("Shopping/Remove/{id}")]
    public IActionResult Remove(int id)
    {
        _shoppingService.RemoveItem(id);
        TempData["Message"] = "Товар удалён";
        TempData["MessageType"] = "danger";
        return RedirectToAction("Index");
    }
}