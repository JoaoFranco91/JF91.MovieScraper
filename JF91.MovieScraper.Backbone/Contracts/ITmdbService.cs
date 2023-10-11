using JF91.MovieScraper.Models;

namespace JF91.MovieScraper.Backbone.Contracts;

public interface ITmdbService
{
    Task<SearchMovie.Result> SearchMovie
    (
        string name,
        string year
    );
}