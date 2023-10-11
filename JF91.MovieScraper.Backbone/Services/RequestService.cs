using JF91.MovieScraper.Backbone.Contracts;
using RestSharp;

namespace JF91.MovieScraper.Backbone.Services;

public class RequestService : IRequestService
{
    private readonly IRestClientService _restClientService;

    public RequestService(IRestClientService restClientService)
    {
        _restClientService = restClientService;
    }

    public async Task RequestHttpResource
    (
        string url,
        string resource,
        Dictionary<string, string> parameters
    )
    {
        var path = Path.Combine
        (
            url,
            resource
        );
        var client = _restClientService.NewClient(path);
        var request = _restClientService.NewRequest(Method.Put);

        request.AddHeader("Authorization", $"Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI3YjRmMGMzYjgyOWQyOTZiZDI0NjliMjFkNGRjNDU3ZCIsInN1YiI6IjY1MjY3YzAwZWE4NGM3MDEyZDZkZTdjMyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.wg5he-oXYczAWGEPgRGz9ycTCBacQURm5ZV9VdDTme4");
        foreach(var parameter in parameters)
        {
            request.AddQueryParameter(parameter.Key, parameter.Value);
        }

        var response = await _restClientService.SendRequestAsync
        (
            client,
            request,
            0
        );
    }
}