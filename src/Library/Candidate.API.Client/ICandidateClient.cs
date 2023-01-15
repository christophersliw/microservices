using Candidate.API.Client.Resources;

namespace Candidate.API.Client;

public interface ICandidateClient
{
    IUpdateApplicationStatusResource UpdateApplicationStatusResource { get; }
}