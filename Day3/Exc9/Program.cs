using System.Text;

var sb = new StringBuilder("this is a test string");
var substring = "test string";

var endsWithSubstring = sb.ToString().EndsWith(substring);
Console.WriteLine(endsWithSubstring);