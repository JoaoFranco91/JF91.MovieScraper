using JF91.MovieScraper.Backbone.Contracts;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace JF91.MovieScraper.Backbone.Services;

public class RestClientService : IRestClientService
{
    public RestClient NewClient(string endpoint)
        => new RestClient(endpoint);

    public RestRequest NewRequest(Method method)
        => new RestRequest() { Method = method };

    public async Task<RestResponse> NewResponse(RestClient client, RestRequest request)
        => await client.ExecuteAsync(request);

    public IAuthenticator AddAuthenticator(string clientId, string clientSecret)
        => new HttpBasicAuthenticator(clientId, clientSecret);

    public void AddHeader(RestRequest request, string name, string value)
        => request.AddHeader(name, value);

    public void AddParameter(RestRequest request, string name, string value)
        => request.AddParameter(name, value);

    public dynamic Deserialize<T>(RestResponse response)
        => JsonConvert.DeserializeObject<T>(response.Content);

    public object Serialize<T>(T obj)
        => JsonConvert.SerializeObject(obj);

    public async Task<RestResponse> SendRequestAsync(RestClient restClient, RestRequest restRequest, int tentative)
    {
        var response = await restClient.ExecuteAsync(restRequest);

        if (response != null)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK || response.Content.Length <= 0)
            {
                tentative++;
                if (tentative <= 10) await SendRequestAsync(restClient, restRequest, tentative);
            }
        }
        else
        {
            tentative++;
            if (tentative <= 10) await SendRequestAsync(restClient, restRequest, tentative);
        }

        return response;
    }
}