namespace Exc3;

public class Hospital(Person[] people)
{
    public Patient GetMostCriticalPatient()
    {
        return people.OfType<Patient>().OrderBy(p => p.HealthStatus).FirstOrDefault();
    }
    
    public Doctor[] GetDoctorsBySpecialty(string specialty)
    {
        return people.OfType<Doctor>().Where(d => d.Specialty.Equals(specialty, StringComparison.OrdinalIgnoreCase)).ToArray();
    }
}