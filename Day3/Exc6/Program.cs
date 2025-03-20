using System.Text.RegularExpressions;

Console.Write("Введите строку: ");
var input = Console.ReadLine();

var matches = Regex.Matches(input, @"\b[a-zA-Z]+\b");

Console.WriteLine($"Количество слов, содержащих только латинские буквы: {matches.Count}");