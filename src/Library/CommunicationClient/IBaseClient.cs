namespace CommunicationClient;

public interface IBaseClient
{
    Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken);
    Task<TResponse> PostAsync<TResponse, TRequestContent>(Uri uri, TRequestContent requestContentObject,
        CancellationToken cancellationToken);
    Task PostAsync<TRequestContent>(Uri uri, TRequestContent requestContentObject,
        CancellationToken cancellationToken);
    Task<TResponse> PutAsync<TResponse, TRequestContent>(Uri uri, TRequestContent requestContentObject,
        CancellationToken cancellationToken);
    
    Task PutAsync<TRequestContent>(Uri uri, TRequestContent requestContentObject,
        CancellationToken cancellationToken);
    Uri BuildUri(string path, string query);
    public Uri BuildUri(string path);
}