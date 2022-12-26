using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
    
    public async Task<TResponse> GetAsync<TResponse>(Uri uri, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"BaseClient > GetAsync ({uri.AbsoluteUri})");
        
        var response = await _client.GetAsync(uri, cancellationToken);
        
        _logger.LogInformation($"BaseClient > GetAsync ({uri.AbsoluteUri} status code:{response.StatusCode})");
        
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<TResponse>();

        return result;
    }

    public async Task<TResponse> PostAsync<TResponse, TRequestContent>(Uri uri, TRequestContent requestContentObject, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"BaseClient > PostAsync ({uri.AbsoluteUri})");
        
        var jsonObject = JsonConvert.SerializeObject(requestContentObject);

        var requestContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(uri, requestContent, cancellationToken);
        
        _logger.LogInformation($"BaseClient > PostAsync ({uri.AbsoluteUri} status code:{response.StatusCode})");
        
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<TResponse>();

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