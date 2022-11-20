namespace Gateway.Application.Clients;

public interface ICandidateClient
{
    Task<T> GetAsync<T>(string path, CancellationToken cancellationToken);
}