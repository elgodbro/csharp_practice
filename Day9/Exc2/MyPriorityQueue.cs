namespace Exc2;

public class MyPriorityQueue<T>
{
    private List<PriorityQueueItem<T>> _items = [];
    
    public void Enqueue(T item, int priority)
    {
        _items.Add(new PriorityQueueItem<T>(item, priority));
        _items = _items.OrderByDescending(x => x.Priority).ToList();
    }

    public PriorityQueueItem<T> Dequeue()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("Очередь пуста");

        var item = _items[0];
        _items.RemoveAt(0);
        return item;
    }

    public PriorityQueueItem<T> Peek()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("Очередь пуста");

        return _items[0];
    }
    
    public int Count => _items.Count;
}