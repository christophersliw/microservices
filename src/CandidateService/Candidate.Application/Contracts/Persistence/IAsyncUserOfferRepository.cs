using Candidate.Domain.Entities;
using Common.Installers.Persistance.Contracts;

namespace Candidate.Application.Contracts.Persistence;

public interface IAsyncUserOfferRepository : IAsyncRepository<UserOffer>, IRepository<UserOffer>
{

}