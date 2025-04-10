using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Models;

public class ShoppingItemViewModel
{
    [Required(ErrorMessage = "Название обязательно")]
    public string Name { get; set; }

    [Range(1, 100, ErrorMessage = "Количество должно быть от 1 до 100")]
    public int Quantity { get; set; }
}