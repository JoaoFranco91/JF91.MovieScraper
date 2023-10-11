namespace JF91.MovieScraper.Backbone.Contracts;

public interface IFileManagerService
{
    bool CheckFileExists(string path);
    void CreateFile(string targetFile, string extension);
    void DeleteFile(string targetFile);
    void CopyFile(string sourceFile, string targetFile);
    void MoveFile(string sourceFile, string targetFile);
    void RenameFile(string sourceFile, string targetFile);
    int CountFiles(string targetFolder);
    List<FileInfo> GetFilesInfoInFolder(string sourceFolder);
    FileInfo GetFileInfo(string sourceFile);
    FileInfo GetFirstFileInFolder(string sourceFolder);
    string? GetFileInfoByExtensionInFolder(string sourceFolder, string extension);
}