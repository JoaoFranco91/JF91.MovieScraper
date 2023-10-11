using System.Text.RegularExpressions;
using JF91.MovieScraper.Backbone.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JF91.MovieScraper.Backbone.Services;

public class MovieRenamer : IMovieRenamer
{
    private readonly ILogger<MovieRenamer> _logger;
    private readonly ITmdbService _tmdbService;
    private readonly IFileManagerService _fileManagerService;

    private readonly string dirPath = string.Empty;
    private readonly string[] subtitleExtensions = { ".srt", ".sub" };
    private readonly string[] mediaExtensions = { ".mp4", ".mkv", ".avi", ".wmv" };
    private readonly string regexPattern = @"^(?<Name>.+?)(?!\.[12]\d\d\d\.\d{,3}[ip]\.)\.(?<Year>\d\d\d\d)";

    public MovieRenamer
    (
        ILogger<MovieRenamer> logger,
        ITmdbService tmdbService,
        IFileManagerService fileManagerService,
        IConfiguration configuration
    )
    {
        _logger = logger;
        _tmdbService = tmdbService;
        _fileManagerService = fileManagerService;
        
        dirPath = configuration.GetSection("MoviesFolder").Value;
    }

    public async Task ExecuteMovieRenamer()
    {
        _logger.LogInformation("Starting Movie Renamer");
        var movieDirectories = GetMovieDirectories(dirPath);
        var movieFiles = GetMovieFiles(dirPath);

        _logger.LogInformation("Processing Movie Folders");
        await ProcessMovieDirectories
        (
            dirPath,
            movieDirectories
        );
        
        await ProcessMovieFiles
        (
            dirPath,
            movieFiles
        );
    }

    public async Task ProcessMovieDirectories
    (
        string dirPath,
        string[] movieDirs
    )
    {
        _logger.LogWarning($"Movies Found: {movieDirs.Length}");
        
        foreach (var movieDir in movieDirs)
        {
            _logger.LogInformation("#####################################################");
            _logger.LogInformation($"Processing folder: {movieDir}");
            
            string formatName = FormatDirectoryFullName(movieDir);

            var results = Regex.Matches
                (
                    formatName,
                    regexPattern,
                    RegexOptions.ExplicitCapture // Only what we ask for (?<> ), ignore non captures
                    | RegexOptions.Multiline // ^ makes each line a separate one.
                    | RegexOptions.IgnorePatternWhitespace
                ) // Allows us to comment pattern only.
                .OfType<Match>()
                .Select
                (
                    mt => new
                    {
                        Movie = Regex.Replace
                        (
                            mt.Groups["Name"].Value,
                            @"\.",
                            " "
                        ),
                        Year = mt.Groups["Year"].Value,
                    }
                )
                .FirstOrDefault();

            string movieName = results.Movie;
            string movieYear = results.Year;

            var tmdbMovie = await _tmdbService.SearchMovie
            (
                movieName,
                movieYear
            );

            if (tmdbMovie != null)
            {
                var tmdbMovieTitle = tmdbMovie.title;
                tmdbMovieTitle = tmdbMovieTitle.Replace(":", " - ");
                tmdbMovieTitle = tmdbMovieTitle.Replace("/", "-");
                tmdbMovieTitle = tmdbMovieTitle.Replace(@"\", "-");
                tmdbMovieTitle = tmdbMovieTitle.Replace(">", "-");
                tmdbMovieTitle = tmdbMovieTitle.Replace("<", "-");
                tmdbMovieTitle = tmdbMovieTitle.Replace("|", "-");
                tmdbMovieTitle = tmdbMovieTitle.Replace("?", string.Empty);
                tmdbMovieTitle = tmdbMovieTitle.Replace("\"", string.Empty);
                tmdbMovieTitle = tmdbMovieTitle.Replace("*", string.Empty);
                
                var files = _fileManagerService.GetFilesInfoInFolder(movieDir);

                _logger.LogInformation("Deleting non media files.");
                var filesToDelete = files.Where
                (
                    x =>
                        !subtitleExtensions.Contains(x.Extension)
                        && !mediaExtensions.Contains(x.Extension)
                );
                foreach (var fileToDelete in filesToDelete)
                {
                    fileToDelete.Attributes = FileAttributes.Normal;
                    File.Delete(fileToDelete.FullName);
                }
                
                _logger.LogInformation("Deleting extra folders.");
                var foldersToDelete = Directory.GetDirectories(movieDir);
                foreach (var folderToDelete in foldersToDelete)
                {
                    Directory.Delete(folderToDelete, true);
                }

                _logger.LogInformation("Renaming media files.");
                files = files.Except(filesToDelete).ToList();
                foreach (var file in files)
                {
                    if (file.Name != $"{tmdbMovieTitle} ({movieYear}){file.Extension}")
                    {
                        file.Attributes = FileAttributes.Normal;
                        _fileManagerService.RenameFile
                        (
                            file.FullName,
                            $"{file.DirectoryName}/{tmdbMovieTitle} ({movieYear}){file.Extension}"
                        );
                    }
                }

                _logger.LogInformation("Renaming media folder");
                var sourceDir = new DirectoryInfo(movieDir);
                if (sourceDir.Name != $"{tmdbMovieTitle} ({movieYear})")
                {
                    Directory.Move
                    (
                        movieDir,
                        $"{dirPath}/{tmdbMovieTitle} ({movieYear})"
                    );
                }
            }
            
            _logger.LogInformation("#####################################################");
        }
    }

    public async Task ProcessMovieFiles
    (
        string dirPath,
        string[] movieFiles
    )
    {
        
    }

    public string[] GetMovieDirectories
    (
        string dirPath
    )
    {
        if (!Directory.Exists(dirPath))
        {
            throw new Exception
            (
                "Movies directory was not found.",
                new DirectoryNotFoundException()
            );
        }

        return Directory.GetDirectories(dirPath);
    }

    public string[] GetMovieFiles
    (
        string dirPath
    )
    {
        if (!Directory.Exists(dirPath))
        {
            throw new Exception
            (
                "Movies directory was not found.",
                new DirectoryNotFoundException()
            );
        }

        return Directory.GetFiles(dirPath);
    }

    public string FormatDirectoryFullName
    (
        string dirPath
    )
    {
        string formatName = dirPath.Substring(dirPath.LastIndexOf(@"\") + 1);

        formatName = formatName.Replace
        (
            " ",
            "."
        );

        formatName = formatName.Replace
        (
            "(",
            ""
        );

        formatName = formatName.Replace
        (
            ")",
            ""
        );

        formatName = formatName.Replace
        (
            "[",
            ""
        );

        formatName = formatName.Replace
        (
            "]",
            ""
        );

        return formatName;
    }
}