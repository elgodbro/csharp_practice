namespace Exc3;

public sealed class Doctor() : Person()
{
    private static readonly string[] Specs = ["Кардиолог", "Терапевт", "Хирург", "Психолог"];
    public string Specialty { get; set; } = Faker.Extensions.EnumerableExtensions.Rand(Specs);

    public override string ToString()
    {
        return $"Врач: {FullName}, Возраст: {Age}, Специализация: {Specialty}";
    }
}