using System.Net.Http.Json;

namespace Recruitment.API.Client.Base;

public class BaseClient : IBaseClient
{
    private readonly string _baseUri;
    private readonly HttpClient _client;

    public BaseClient(HttpClient client, string baseUri)
    {
        _client = client;
        _baseUri = baseUri;
    }
    
    public async Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync(uri, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<T>();

        return result;
    }

    public Uri BuildUri(string format)
    {
        return new UriBuilder(_baseUri)
        {
            Path = format
        }.Uri;
    }
}