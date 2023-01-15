using Candidate.API.Client.Resources;
using CommunicationClient;
using Microsoft.Extensions.Logging;

namespace Candidate.API.Client;

public class CandidateClient : ICandidateClient
{
    public CandidateClient(HttpClient client, ILoggerFactory loggerFactory)
    {
        UpdateApplicationStatusResource = new UpdateAplicationsStatusResource(
            new BaseClient(client,
                client.BaseAddress.ToString(),
                loggerFactory.CreateLogger<BaseClient>()));
    }
    
    public IUpdateApplicationStatusResource UpdateApplicationStatusResource { get; }
}