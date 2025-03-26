namespace Exc3;

public interface ITaskManager<T>
{
    void AddTask(T task);
    void CompleteTask(T task);
}