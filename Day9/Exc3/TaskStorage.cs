namespace Exc3;

public class TaskStorage<T> where T : Task
{
    private readonly List<T> _tasks = [];
    
    public void Add(T task) => _tasks.Add(task);

    public void Remove(T task) => _tasks.Remove(task);

    public List<T> GetAllTasks() => _tasks;

    public T? FindTaskByName(string name) => _tasks.FirstOrDefault(t => t.Name == name);
}