using Exc3;
using Task = Exc3.Task;

var taskManager = new TaskManager<Task>();

taskManager.AddTask(new Task("Написать отчет"));
taskManager.AddTask(new Task("Провести встречу"));
taskManager.AddTask(new Task("Исправить ошибку"));

taskManager.PrintTasks();

taskManager.CompleteTask(new Task("Провести встречу"));

taskManager.PrintTasks();

Console.WriteLine("После сортировки:");
taskManager.SortTasksByName();
taskManager.PrintTasks();