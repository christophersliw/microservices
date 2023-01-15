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
    
    
    public async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken)
    {
        var uri = BuildUri(path);

        return await _baseClient.GetAsync<T>(uri, cancellationToken);
    }

    public async Task<TResponse> PostAsync<TResponse, TRequestContent>(string path, TRequestContent requestContentObject,
        CancellationToken cancellationToken)
    {
        var uri = BuildUri(path);

        return await _baseClient.PostAsync<TResponse, TRequestContent>(uri, requestContentObject, cancellationToken);
    }

    public async Task PostAsync<TRequestContent>(string path, TRequestContent requestContentObject, CancellationToken cancellationToken)
    {
        var uri = BuildUri(path);

        await _baseClient.PostAsync<TRequestContent>(uri, requestContentObject, cancellationToken);
    }

    public async Task<TResponse> PutAsync<TResponse, TRequestContent>(string path, TRequestContent requestContentObject,
        CancellationToken cancellationToken)
    {
        var uri = BuildUri(path);

        return await _baseClient.PutAsync<TResponse, TRequestContent>(uri, requestContentObject, cancellationToken);
    }

    public async Task PutAsync<TRequestContent>(string path, TRequestContent requestContentObject, CancellationToken cancellationToken)
    {
        var uri = BuildUri(path);

        await _baseClient.PutAsync<TRequestContent>(uri, requestContentObject, cancellationToken);
    }

    private Uri BuildUri(string path)
    {
        return _baseClient.BuildUri(path);
    }
}