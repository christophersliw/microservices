namespace Gateway.Application.Clients;

public interface ICandidateClient
{
    Task<T> GetAsync<T>(string path, CancellationToken cancellationToken);
    Task<TResponse> PostAsync<TResponse, TRequestContent>(string path, TRequestContent requestContentObject, CancellationToken cancellationToken);
    Task PostAsync<TRequestContent>(string path, TRequestContent requestContentObject, CancellationToken cancellationToken);
    Task<TResponse> PutAsync<TResponse, TRequestContent>(string path, TRequestContent requestContentObject, CancellationToken cancellationToken);
    Task PutAsync<TRequestContent>(string path, TRequestContent requestContentObject, CancellationToken cancellationToken);
}