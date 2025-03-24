namespace Exc3;

public class Smartphone(string name) : SmartDevice(name), ICanPlayMusic, ICanMakeCalls
{
    public void PlayMusic()
    {
        Console.WriteLine($"{Name} воспроизводит музыку");
    }
    
    public void MakeCall(string number)
    {
        Console.WriteLine($"{Name} звонит на номер {number}");
    }
}