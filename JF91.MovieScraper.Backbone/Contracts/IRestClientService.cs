using RestSharp;
using RestSharp.Authenticators;

namespace JF91.MovieScraper.Backbone.Contracts;

public interface IRestClientService
{
    RestClient NewClient(string endpoint);
    RestRequest NewRequest(Method method);
    Task<RestResponse> NewResponse(RestClient client, RestRequest request);

    IAuthenticator AddAuthenticator(string clientId, string clientSecret);
    void AddHeader(RestRequest request, string name, string value);
    void AddParameter(RestRequest request, string name, string value);

    dynamic Deserialize<T>(RestResponse response);
    object Serialize<T>(T obj);

    Task<RestResponse> SendRequestAsync(RestClient restClient, RestRequest restRequest, int tentative);
}