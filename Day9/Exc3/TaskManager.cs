namespace Exc3;

public class TaskManager<T> : ITaskManager<T> where T : Task
{
    private readonly TaskStorage<T> _taskStorage = new();

    public void AddTask(T task)
    {
        _taskStorage.Add(task);
        Console.WriteLine($"Добавлена новая задача: {task.Name}");
    }

    public void CompleteTask(T task)
    {
        var foundTask = _taskStorage.FindTaskByName(task.Name);
        
        if (foundTask != null)
        {
            foundTask.IsCompleted = true;
            Console.WriteLine($"Задача завершена: {foundTask.Name}");
        }
        else
            Console.WriteLine($"Задача '{task.Name}' не найдена");
    }

    public void PrintTasks()
    {
        Console.WriteLine("\nСписок всех задач:");
        foreach (var task in _taskStorage.GetAllTasks())
        {
            Console.WriteLine(task);
        }

        Console.WriteLine();
    }

    public void SortTasksByName()
    {
        var tasks = _taskStorage.GetAllTasks();
        tasks.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));
    }
}