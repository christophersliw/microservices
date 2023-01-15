using Recruitment.API.Client.Resources;

namespace Recruitment.API.Client;

public interface IOfferClient
{
     IOfferItemResource Item { get; }
}