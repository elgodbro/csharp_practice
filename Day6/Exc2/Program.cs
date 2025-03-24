using Exc2;

var chef1 = new Chef("Олег");
var chef2 = new Chef("Генадий");
var chef3 = new Chef("Богдан");

var supplier = new Supplier("FreshFoods");

Restaurant[] restaurants = [
    new("Бархат", [chef1, chef2], supplier),
    new("Фреш", [chef3], supplier)
];

foreach (var restaurant in restaurants)
{
    restaurant.Menu.ShowMenu();
    restaurant.Supplier.SupplyProducts();
    restaurant.PrepareDishes();
    Console.WriteLine();
}