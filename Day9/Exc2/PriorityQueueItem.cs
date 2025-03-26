namespace Exc2;

public class PriorityQueueItem<T>(T item, int priority)
{
    public T Item { get; } = item;
    public int Priority { get; } = priority;
}