using Candidate.Application.Responses;

namespace Candidate.Application.Services;

public interface IOfferClientService
{
    Task<OfferResponse> GetById(int id, OfferResponse defaultItem, CancellationToken cancellationToken);
}