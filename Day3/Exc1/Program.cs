var nums = new double[10];

for (var i = 0; i < nums.Length; i++)
{
    nums[i] = Math.Round(Random.Shared.NextDouble() * 100, 2);
}
Console.WriteLine(string.Join("; ", nums));

double product = 1;
for (var i = 0; i < nums.Length; i++)
{
    if (i % 2 == 0) product *= nums[i];
}

Console.WriteLine($"Произведение элементов, стоящих на четных позициях: {product:F2}");