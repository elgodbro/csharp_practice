namespace Exc3;

public class SmartSpeaker(string name) : SmartDevice(name), ICanPlayMusic
{
    public void PlayMusic()
    {
        Console.WriteLine($"{Name} воспроизводит музыку");
    }
}