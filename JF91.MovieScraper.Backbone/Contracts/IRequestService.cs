namespace JF91.MovieScraper.Backbone.Contracts;

public interface IRequestService
{
    Task RequestHttpResource
    (
        string url,
        string resource,
        Dictionary<string, string> parameters
    );
}