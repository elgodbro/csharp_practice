using System;
using System.IO;
using Spectre.Console;
using Day10;

var fm = new FileManager();
var fip = new FileInfoProvider();

const string fileName = "Dzhangirov.ps";

var table = new Table();
table.AddColumn("[bold yellow]Операция[/]");
table.AddColumn("[bold cyan]Результат[/]");

fm.CreateFile(fileName, "Bla bla bla bl ble ble");
table.AddRow("[bold yellow]1. Создание и чтение файла[/]", "[green]Файл создан.[/]");
table.AddRow("", $"[blue]{File.ReadAllText(fileName)}[/]");

var info = fip.GetFileInfo(fileName);
table.AddRow("[bold yellow]2. Информация о файле[/]", $"Размер: {info.Size} байт, Дата создания: {info.Created}");

fm.CopyFile(fileName, "copy.txt");
table.AddRow("[bold yellow]3. Копирование файла[/]", $"Файл скопирован: [bold cyan]{File.Exists("copy.txt")}[/]");

table.AddRow("[bold yellow]4. Сравнение размеров[/]", $"Файлы одинаковы: [bold cyan]{fip.CompareSizes(fileName, "copy.txt")}[/]");

fm.SetFileReadOnly(fileName, true);
try
{
    File.WriteAllText(fileName, "Попытка записи в readonly файл");
}
catch (Exception e)
{
    table.AddRow("[bold yellow]5. Попытка записи в readonly файл[/]", $"[red]Ошибка:[/] {e.Message}");
}
finally
{
    fm.SetFileReadOnly(fileName, false);
}

var perms = fip.CheckPermissions(fileName);
table.AddRow("[bold yellow]6. Права доступа[/]", $"Чтение: [bold cyan]{perms.Read}[/], Запись: [bold cyan]{perms.Write}[/]");

Directory.CreateDirectory("new_dir");
fm.MoveFile(fileName, $"new_dir/{fileName}");
table.AddRow("[bold yellow]7. Перемещение файла[/]", $"Файл в 'new_dir': [bold cyan]{File.Exists($"new_dir/{fileName}")}[/]");

fm.RenameFile($"new_dir/{fileName}", "familiya.io");
table.AddRow("[bold yellow]8. Переименование файла[/]", $"Файл 'familiya.io' создан: [bold cyan]{File.Exists("new_dir/familiya.io")}[/]");

try 
{ 
    fm.DeleteFile("no.file"); 
}
catch (FileNotFoundException e) 
{ 
    table.AddRow("[bold yellow]9. Удаление несуществующего файла[/]", $"[red]Ошибка:[/] {e.Message}");
}

fm.CreateFile("template.ps", "Faradenza delyacocka");
fm.DeleteFilesByPattern(".", "*.ps");
table.AddRow("[bold yellow]10. Удаление файлов по шаблону[/]", "[yellow]Файлы с расширением '.ps' удалены.[/]");

var files = fm.GetFilesInDirectory(".");
table.AddRow("[bold yellow]11. Список файлов в директории[/]", string.Join("\n", files));

fm.CreateFile(fileName, "Новый файл для проверки доступа");
var newPerms = fip.CheckPermissions(fileName);
table.AddRow("[bold yellow]12. Проверка прав нового файла[/]", $"Чтение: [bold cyan]{newPerms.Read}[/], Запись: [bold cyan]{newPerms.Write}[/]");

AnsiConsole.Write(table);
