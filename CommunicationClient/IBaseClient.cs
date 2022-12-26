namespace CommunicationClient;

public interface IBaseClient
{
    Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken);
    Task<TResponse> PostAsync<TResponse, TRequestContent>(Uri uri, TRequestContent requestContentObject,
        CancellationToken cancellationToken);
    Uri BuildUri(string format);
}