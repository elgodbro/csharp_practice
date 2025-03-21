namespace Exc5;

public abstract class MusicalInstrument
{
    public abstract void PlaySound();
    
    public virtual void Tune()
    {
        Console.WriteLine("Тюн инструмента");
    }
}