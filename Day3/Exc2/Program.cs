var nums = new double[25];

for (var i = 0; i < nums.Length; i++)
{
    nums[i] = Math.Round(Random.Shared.NextDouble() * 100 - 50, 2);
}
Console.WriteLine($"Сгенерированный массив: {string.Join("; ", nums)}");

var min = nums[0];
var max = nums[0];
foreach (var num in nums)
{
    if (num < min) min = num;
    if (num > max) max = num;
}

for (var i = 0; i < nums.Length; i++)
{
    nums[i] *= nums[i] > 0 ?  Math.Pow(min, 2) : Math.Pow(max, 2);
}

Console.WriteLine($"Min: {min}, Max: {max}");
Console.WriteLine($"Полученная послдовательность: {string.Join("; ", nums)}");