using CommunicationClient;
using Microsoft.Extensions.Logging;

namespace Gateway.Application.Clients;

public class CandidateClient : ICandidateClient
{
    private IBaseClient _baseClient;
    
    public CandidateClient(HttpClient httpClient, ILoggerFactory loggerFactory)
    {
        _baseClient = new BaseClient(httpClient, httpClient.BaseAddress.ToString(), loggerFactory.CreateLogger<BaseClient>());
    }
    
    
    public async Task<T> GetAsync<T>(string path,string query, CancellationToken cancellationToken)
    {
        var uri = BuildUri(path, query);

        return await _baseClient.GetAsync<T>(uri, cancellationToken);
    }

    public async Task<TResponse> PostAsync<TResponse, TRequestContent>(string path,string query, TRequestContent requestContentObject,
        CancellationToken cancellationToken)
    {
        var uri = BuildUri(path, query);

        return await _baseClient.PostAsync<TResponse, TRequestContent>(uri, requestContentObject, cancellationToken);
    }

    public async Task PostAsync<TRequestContent>(string path, string query, TRequestContent requestContentObject, CancellationToken cancellationToken)
    {
        var uri = BuildUri(path, query);

        await _baseClient.PostAsync<TRequestContent>(uri, requestContentObject, cancellationToken);
    }

    public async Task<TResponse> PutAsync<TResponse, TRequestContent>(string path, string query, TRequestContent requestContentObject,
        CancellationToken cancellationToken)
    {
        var uri = BuildUri(path, query);

        return await _baseClient.PutAsync<TResponse, TRequestContent>(uri, requestContentObject, cancellationToken);
    }

    public async Task PutAsync<TRequestContent>(string path, string query, TRequestContent requestContentObject, CancellationToken cancellationToken)
    {
        var uri = BuildUri(path, query);

        await _baseClient.PutAsync<TRequestContent>(uri, requestContentObject, cancellationToken);
    }

    private Uri BuildUri(string path, string query)
    {
        return _baseClient.BuildUri(path, query);
    }
}