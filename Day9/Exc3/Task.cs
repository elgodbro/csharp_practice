namespace Exc3;

public class Task(string name)
{
    public string Name { get; set; } = name;
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public override string ToString() => $"Задача: {Name}, Статус: {(IsCompleted ? "Выполнена" : "Не выполнена")}, Создана: {CreatedAt}";
}