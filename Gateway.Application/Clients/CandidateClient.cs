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
    
    private Uri BuildUri(string path)
    {
        return _baseClient.BuildUri(path);
    }
}