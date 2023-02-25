
using Recruitment.API.Client.Responses;

namespace Recruitment.API.Client.Resources;

public interface IOfferItemResource
{
    Task<OfferItemResponse> Get(Guid id, CancellationToken cancellationToken = default);
}