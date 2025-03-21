namespace Exc3;

public abstract class Person()
{
    public string FullName { get; set; } = Faker.Name.FullName();
    public int Age { get; set; } = Faker.Number.RandomNumber(18, 70);
    public int HealthStatus { get; set; } = Faker.Number.RandomNumber(1, 11); //10 - Как огурчик, 1 - как умерчик
}