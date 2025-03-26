namespace Exc2;

public class PriorityTaskManager<T>
{
    private readonly MyPriorityQueue<T> _priorityQueue = new();
    
    public void AddTask(T task, int priority)
    {
        _priorityQueue.Enqueue(task, priority);
    }
    
    public (T Task, int Priority) ExecuteNextTask()
    {
        var item = _priorityQueue.Dequeue();
        return (item.Item, item.Priority);
    }

    public (T Task, int Priority) PreviewNextTask()
    {
        var item = _priorityQueue.Peek();
        return (item.Item, item.Priority);
    }

    public int GetTaskPriority() => _priorityQueue.Count;

    public int TaskCount => _priorityQueue.Count;
}