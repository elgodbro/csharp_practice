namespace Exc5;

public class Guitar: MusicalInstrument
{
    public override void PlaySound()
    {
        Console.WriteLine("Гитара гитарит");
    }

    public override void Tune()
    {
        Console.WriteLine("Тюн гитары");
    }
}