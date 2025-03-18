Console.Write("Введите масштаб карты (км/см): ");
var scale = Convert.ToDouble(Console.ReadLine());

Console.Write("Введите расстояние между точками на карте (см): ");
var distanceOnMap = Convert.ToDouble(Console.ReadLine());

var realDistance = distanceOnMap * scale;

Console.WriteLine($"Расстояние между населенными пунктами: {realDistance} км");