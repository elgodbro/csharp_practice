using System.Text.RegularExpressions;

var input = "Hello world";

var startsWithUpperCase = Regex.IsMatch(input, @"^[A-ZА-Я]");
Console.WriteLine(startsWithUpperCase);