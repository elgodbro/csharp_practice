var input = "hello hello world world world";

var words = input.ToLower().Split(' ');
var uniqueWords = new List<string>();

foreach (var word in words)
{
    if (!uniqueWords.Contains(word))
        uniqueWords.Add(word);
}

Console.WriteLine(string.Join(" ", uniqueWords));