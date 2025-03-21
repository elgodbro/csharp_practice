namespace Exc3;

public sealed class Patient()
    : Person()
{
    private static readonly string[] Diagnoses = ["Пневмония", "ОРИ", "ВИЧ", "Эпилепсия"];
    public string Diagnosis { get; set; } = Faker.Extensions.EnumerableExtensions.Rand(Diagnoses);

    public override string ToString()
    {
        return $"Пациент: {FullName}, Возраст: {Age}, Диагноз: {Diagnosis}, Состояние: {HealthStatus}";
    }
}