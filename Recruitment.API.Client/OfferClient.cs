using Recruitment.API.Client.Base;
using Recruitment.API.Client.Resources;

namespace Recruitment.API.Client;

public class OfferClient : IOfferClient
{
    public OfferClient(HttpClient client)
    {
        Item = new OfferItemResource(new BaseClient(client, client.BaseAddress.ToString()));
    }
    
    public IOfferItemResource Item { get; }
}