using CommunicationClient;
using Microsoft.Extensions.Logging;
using Recruitment.API.Client.Resources;

namespace Recruitment.API.Client;

public class OfferClient : IOfferClient
{
    public OfferClient(HttpClient client, ILoggerFactory loggerFactory)
    {
        Item = new OfferItemResource(new BaseClient(client, client.BaseAddress.ToString(), loggerFactory.CreateLogger<BaseClient>()));
    }
    
    public IOfferItemResource Item { get; }
}