using Candidate.API.Client.RequestContent;
using Candidate.API.Client.Responses;
using CommunicationClient;

namespace Candidate.API.Client.Resources;

public class UpdateAplicationsStatusResource : IUpdateApplicationStatusResource
{
    private readonly IBaseClient _client;
    
    public UpdateAplicationsStatusResource(IBaseClient client)
    {
        _client = client;
    }
    
    public async Task<UpdateApplicationStatusResponse> UpdateStatus(UpdateStatusRequest requestContent, CancellationToken cancellationToken)
    {
        var uri = BuildUri();
        return await _client.PostAsync<UpdateApplicationStatusResponse, UpdateStatusRequest>(uri, requestContent, cancellationToken);
    }
    
    private Uri BuildUri()
    {
        return _client.BuildUri($"api/candidateservice/changestatus");
    }
}