namespace Exc4;

public class FileManager : IReadFile, IWriteFile
{
    void IReadFile.AccessFile(string fileName)
    {
        Console.WriteLine($"Чтение файла: {fileName}");
    }
    
    void IWriteFile.AccessFile(string fileName)
    {
        Console.WriteLine($"Запись в файл: {fileName}");
    }
}