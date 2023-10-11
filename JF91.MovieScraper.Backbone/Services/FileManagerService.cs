using JF91.MovieScraper.Backbone.Contracts;

namespace JF91.MovieScraper.Backbone.Services;

public class FileManagerService : IFileManagerService
    {
        public bool CheckFileExists(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }

        public void CopyFile(string sourceFile, string targetFile)
        {
            try
            {
                if (File.Exists(targetFile))
                {
                    DeleteFile(targetFile);
                }
                File.Copy(sourceFile, targetFile);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }

        public int CountFiles(string targetFolder)
        {
            try
            {
                int fileCount = Directory.GetFiles(targetFolder).Length;

                return fileCount;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }

        public void CreateFile(string targetFile, string extension)
        {
            try
            {
                if (File.Exists(targetFile))
                {
                    DeleteFile(targetFile);
                }

                File.Create(targetFile);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }

        public void DeleteFile(string targetFile)
        {
            try
            {
                if (File.Exists(targetFile))
                {
                    File.Delete(targetFile);
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }

        public FileInfo GetFileInfo(string sourceFile)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(sourceFile);

                return fileInfo;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }

        public string? GetFileInfoByExtensionInFolder(string sourceFolder, string extension)
        {
            var files = Directory.GetFiles(sourceFolder, extension);
            return files.FirstOrDefault();
        }

        public List<FileInfo> GetFilesInfoInFolder(string sourceFolder)
        {
            try
            {
                var files = Directory.GetFiles(sourceFolder);
                var filesInfo = new List<FileInfo>();

                foreach(var file in files)
                {
                    filesInfo.Add(new FileInfo(file));
                }

                return filesInfo;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }

        public FileInfo GetFirstFileInFolder(string sourceFolder)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Directory.GetFiles(sourceFolder).FirstOrDefault());

                return fileInfo;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }

        public void MoveFile(string sourceFile, string targetFile)
        {
            try
            {
                if (File.Exists(targetFile))
                {
                    DeleteFile(targetFile);
                }
                File.Move(sourceFile, targetFile);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }

        public void RenameFile(string sourceFile, string targetFile)
        {
            try
            {
                if (File.Exists(targetFile))
                {
                    DeleteFile(targetFile);
                }
                File.Move(sourceFile, targetFile);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File Was Not Found or Path is Incorrect", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException("File Path Contains Too Many Characters. Please Reduce The Path Lenght", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Folder Was Not Found or Path is Incorrect", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong", ex);
            }
        }
    }