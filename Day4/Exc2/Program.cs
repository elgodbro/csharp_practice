using Exc2;

List<City> cities = [
    new City(),
    new City(),
    new City(),
    new City(),
    new City()
];

var newCity = new City { Name = "Grodno", Population = 12500000 };
CityManager.AddCity(cities, newCity);

Console.WriteLine("Статистика по городам:");
CityManager.PrintStatistics(cities);

var mostPopularCity = CityManager.FindMostPopularCity(cities);
Console.WriteLine($"\nГород с наибольшим населением: {mostPopularCity?.Name} ({mostPopularCity?.Population} чел)\n");

CityManager.SortCity(cities, (c1, c2) => c1.Population.CompareTo(c2.Population));
Console.WriteLine("Сортировка городов по населению (возрастание):");
foreach (var city in cities)
{
    Console.WriteLine($"{city.Name}: {city.Population} чел");
}

var largeCities = CityManager.FilterCity(cities, city => city.Population > 1000000);
Console.WriteLine("\nГорода с населением больше 1 млн:");
foreach (var city in largeCities)
{
    Console.WriteLine($"{city.Name}: {city.Population} чел");
}