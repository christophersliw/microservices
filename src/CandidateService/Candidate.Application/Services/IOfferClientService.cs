using Candidate.Application.Responses;

namespace Candidate.Application.Services;

public interface IOfferClientService
{
    Task<OfferResponse> GetById(Guid id, OfferResponse defaultItem, CancellationToken cancellationToken);
}