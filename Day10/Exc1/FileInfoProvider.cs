namespace Day10;

public class FileInfoProvider
{
    public (long Size, DateTime Created, DateTime Modified) GetFileInfo(string path)
    {
        var info = new FileInfo(path);
        return (info.Length, info.CreationTime, info.LastWriteTime);
    }

    public bool CompareSizes(string path1, string path2) => new FileInfo(path1).Length == new FileInfo(path2).Length;

    public (bool Read, bool Write, bool Execute) CheckPermissions(string path)
    {
        return (
            CanRead(path),
            CanWrite(path),
            CanExecute(path)
        );
    }

    private bool CanRead(string path)
    {
        try { File.OpenRead(path).Close(); return true; }
        catch { return false; }
    }

    private bool CanWrite(string path)
    {
        try { File.OpenWrite(path).Close(); return true; }
        catch { return false; }
    }

    private bool CanExecute(string path)
    {
        try { System.Diagnostics.Process.Start(path); return true; }
        catch { return false; }
    }
}