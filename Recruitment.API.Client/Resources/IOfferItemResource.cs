using Recruitment.API.Contract.Item;

namespace Recruitment.API.Client.Resources;

public interface IOfferItemResource
{
    Task<OfferItemResponse> Get(int id, CancellationToken cancellationToken = default);
}