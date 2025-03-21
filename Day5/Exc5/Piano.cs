namespace Exc5;

public class Piano : MusicalInstrument
{
    public override void PlaySound()
    {
        Console.WriteLine("Пианино пианинит");
    }

    public override void Tune()
    {
        Console.WriteLine("Тюн пианино");
    }
}