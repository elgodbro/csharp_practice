using Exc2;

var taskManager = new PriorityTaskManager<string>();
        
taskManager.AddTask("Написать отчет", 3);
taskManager.AddTask("Провести встречу", 5);
taskManager.AddTask("Исправить ошибку", 1);

Console.WriteLine($"Всего задач: {taskManager.TaskCount}");
Console.WriteLine($"Следующая задача: {taskManager.PreviewNextTask().Task} [Приоритет: {taskManager.PreviewNextTask().Priority}]");
Console.WriteLine($"Выполнена задача: {taskManager.ExecuteNextTask().Task} [Приоритет: {taskManager.ExecuteNextTask().Priority}]");
Console.WriteLine($"Следующая задача: {taskManager.PreviewNextTask().Task} [Приоритет: {taskManager.PreviewNextTask().Priority}]");