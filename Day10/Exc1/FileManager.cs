namespace Day10;

public class FileManager
{
    public void CreateFile(string filePath, string content) => File.WriteAllText(filePath, content);
    
    public void DeleteFile(string path, bool checkExists = false)
    {
        if (checkExists && !File.Exists(path))
            throw new FileNotFoundException("Файл не найден", path);
        
        File.Delete(path);
    }
    
    public void CopyFile(string sourcePath, string destinationPath) => File.Copy(sourcePath, destinationPath, true);
    
    public void MoveFile(string sourcePath, string destinationPath) => File.Move(sourcePath, destinationPath);
    
    public void RenameFile(string path, string newName)
    {
        var dir = Path.GetDirectoryName(path);
        var newPath = Path.Combine(dir ?? "", newName);
        File.Move(path, newPath, true);
    }
    
    public void DeleteFilesByPattern(string directory, string pattern) => Directory.GetFiles(directory, pattern).ToList().ForEach(File.Delete);

    public IEnumerable<string> GetFilesInDirectory(string directory) => Directory.GetFiles(directory);

    public void SetFileReadOnly(string path, bool readOnly)
    {
        var attributes = File.GetAttributes(path);
        attributes = readOnly 
            ? attributes | FileAttributes.ReadOnly 
            : attributes & ~FileAttributes.ReadOnly;
        File.SetAttributes(path, attributes);
    }
    
}