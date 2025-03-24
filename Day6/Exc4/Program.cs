using Exc4;

var fileManager = new FileManager();

IReadFile readFile = fileManager;
readFile.AccessFile("file.txt");

IWriteFile writeFile = fileManager;
writeFile.AccessFile("file.txt");