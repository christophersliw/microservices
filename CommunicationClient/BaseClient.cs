using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace CommunicationClient;

public class BaseClient : IBaseClient
{
    private readonly string _baseUri;
    private readonly ILogger<BaseClient> _logger;
    private readonly HttpClient _client;

    public BaseClient(HttpClient client, string baseUri, ILogger<BaseClient> logger)
    {
        _client = client;
        _baseUri = baseUri;
        _logger = logger;
    }
    
    public async Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"BaseClient > GetAsync ({uri.AbsoluteUri})");
        
        var response = await _client.GetAsync(uri, cancellationToken);
        
        _logger.LogInformation($"BaseClient > GetAsync ({uri.AbsoluteUri} status code:{response.StatusCode})");
        
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