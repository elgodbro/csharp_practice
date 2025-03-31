namespace Exc3;

public class User(string name) : ISubscriber
{
    public void Update(string videoTitle, string channelName)
    {
        Console.WriteLine($"{name}: Новое видео '{videoTitle}' на канале {channelName}!");
    }
}