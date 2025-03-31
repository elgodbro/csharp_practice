namespace Exc3;

public class YouTubeChannel(string name)
{
    private readonly List<ISubscriber> _subscribers = [];
    
    public void Subscribe(ISubscriber subscriber)
    {
        _subscribers.Add(subscriber);
        Console.WriteLine($"Пользователь подписался на {name}");
    }
    
    public void Unsubscribe(ISubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
        Console.WriteLine($"\nПользователь отписался от {name}");
    }
    
    private void NotifySubscribers(string videoTitle)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Update(videoTitle, name);
        }
    }
    
    public void UploadVideo(string title)
    {
        Console.WriteLine($"\n{name} загрузил новое видео: {title}");
        NotifySubscribers(title);
    }
}