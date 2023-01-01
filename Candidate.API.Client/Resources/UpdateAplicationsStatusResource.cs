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
        var uri = BuildUri(requestContent.UserOfferId);
        await _client.PutAsync<UpdateStatusRequest>(uri, requestContent, cancellationToken);

        return new UpdateApplicationStatusResponse();
    }
    
    private Uri BuildUri(Guid id)
    {
        return _client.BuildUri($"api/candidateservice/candidate/{id}/changestatus");
    }
}