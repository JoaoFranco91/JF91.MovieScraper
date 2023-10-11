namespace JF91.MovieScraper.Backbone.Contracts;

public interface IMovieRenamer
{
    Task ExecuteMovieRenamer();

    Task ProcessMovieDirectories
    (
        string dirPath,
        string[] movieDirs
    );
    
    Task ProcessMovieFiles
    (
        string dirPath,
        string[] movieFiles
    );

    string[] GetMovieDirectories
    (
        string dirPath
    );

    string[] GetMovieFiles
    (
        string dirPath
    );

    string FormatDirectoryFullName
    (
        string dirPath
    );
}