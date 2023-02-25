using CommunicationClient;
using Recruitment.API.Client.Responses;

namespace Recruitment.API.Client.Resources;

public class OfferItemResource : IOfferItemResource
{
    private readonly IBaseClient _client;

    public OfferItemResource(IBaseClient client)
    {
        _client = client;
    }
    
    public async Task<OfferItemResponse> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var uri = BuildUri(id);
        return await _client.GetAsync<OfferItemResponse>(uri, cancellationToken);
    }

    private Uri BuildUri(Guid id)
    {
        return _client.BuildUri($"api/recruitmentseservice/offer/{id}");
    }
}