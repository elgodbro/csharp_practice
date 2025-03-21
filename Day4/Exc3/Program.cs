using Exc3;

var people = new Person[10];
for (var i = 0; i < people.Length; i++)
{
    if (i < 5) 
        people[i] = new Patient();
    else 
        people[i] = new Doctor();
}

Console.WriteLine("Список людей:");
foreach (var person in people)
{
    Console.WriteLine(person);
}

var hospital = new Hospital(people);

var criticalPatient = hospital.GetMostCriticalPatient();
Console.WriteLine("\nСамый тяжелый пациент:");
Console.WriteLine(criticalPatient);

Console.WriteLine("\nВрачи-кардиологи:");
var cardiologists = hospital.GetDoctorsBySpecialty("Кардиолог");
foreach (var doctor in cardiologists)
{
    Console.WriteLine(doctor);
}