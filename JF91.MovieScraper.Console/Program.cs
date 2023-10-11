using JF91.SerilogWithLoki;
using JF91.MovieScraper.Backbone.Contracts;
using JF91.MovieScraper.Backbone.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

try
{
    var builder = new ConfigurationBuilder();
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile
        (
            "appsettings.json",
            optional: false,
            reloadOnChange: true
        );

    IConfigurationRoot config = builder.Build();
    SerilogExtensions.CreateLogger(config);

    var serviceProvider = new ServiceCollection()
        .AddLogging
        (
            c =>
                c.AddConsole(opt => opt.LogToStandardErrorThreshold = LogLevel.Information)
        )
        .AddSingleton<IConfiguration>(config)
        .AddSingleton<IRestClientService, RestClientService>()
        .AddSingleton<IRequestService, RequestService>()
        .AddSingleton<ITmdbService, TmdbService>()
        .AddSingleton<IFileManagerService, FileManagerService>()
        .AddSingleton<IMovieRenamer, MovieRenamer>()
        .BuildServiceProvider();

    var movieRenamer = serviceProvider.GetService<IMovieRenamer>();
    await movieRenamer.ExecuteMovieRenamer();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadKey();
}