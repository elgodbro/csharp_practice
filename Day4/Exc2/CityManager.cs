namespace Exc2;

public static class CityManager
{
    public static void AddCity(List<City> cities, City city)
    {
        cities.Add(city);
    }

    public static void SortCity(List<City> cities, Comparison<City> comparison)
    {
        cities.Sort(comparison);
    }

    public static List<City> FilterCity(List<City> cities, Predicate<City> predicate)
    {
        return cities.FindAll(predicate);
    }

    public static void PrintStatistics(List<City> cities)
    {
        if (cities.Count == 0)
        {
            Console.WriteLine("Список городов пуст");
            return;
        }

        var numberOfCities = cities.Count;
        var min = cities.MinBy(city => city.Population);
        var max = cities.MaxBy(city => city.Population);
        var avg = cities.Average(city => city.Population);

        Console.WriteLine($"Всего городов: {numberOfCities}" +
                          $"\nГород с мин. числ. населения: {min?.Name} ({min?.Population} чел)" +
                          $"\nГород с макс. числ. населения: {max?.Name} ({max?.Population} чел)" +
                          $"\nСредняя числ. населения: {avg:F0} чел");
    }

    public static City? FindMostPopularCity(List<City> cities)
    {
        return cities.MaxBy(city => city.Population);
    }
}