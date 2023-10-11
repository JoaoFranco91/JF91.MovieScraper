using JF91.MovieScraper.Backbone.Contracts;
using JF91.MovieScraper.Models;
using JF91.MovieScraper.Shared.Extensions;
using Newtonsoft.Json;
using RestSharp;

namespace JF91.MovieScraper.Backbone.Services;

public class TmdbService : ITmdbService
{
    private readonly IRestClientService _restClientService;
    
    public TmdbService(IRestClientService restClientService)
    {
        _restClientService = restClientService;
    }
    
    public async Task<SearchMovie.Result> SearchMovie
    (
        string name,
        string year
    )
    {
        var client = _restClientService.NewClient("https://api.themoviedb.org/3/search/movie");
        var request = _restClientService.NewRequest(Method.Get);
        
        request.AddHeader("Authorization", $"Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI3YjRmMGMzYjgyOWQyOTZiZDI0NjliMjFkNGRjNDU3ZCIsInN1YiI6IjY1MjY3YzAwZWE4NGM3MDEyZDZkZTdjMyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.wg5he-oXYczAWGEPgRGz9ycTCBacQURm5ZV9VdDTme4");
        
        request.AddQueryParameter("query", name);
        request.AddQueryParameter("year", year);
        
        var response = await _restClientService.SendRequestAsync
        (
            client,
            request,
            0
        );

        var data = JsonConvert.DeserializeObject<SearchMovie.Root>(response.Content);

        if (data.results.Count > 1)
        {
            data.results.RemoveAll(x => x.title.DamerauLevenshteinDistanceTo(name) > 10);
        }

        return data.results.FirstOrDefault();
    }
}