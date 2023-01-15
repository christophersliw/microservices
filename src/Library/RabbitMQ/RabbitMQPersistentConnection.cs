using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ;

public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<RabbitMQPersistentConnection> _logger;
    IConnection _connection;
    bool _disposed;

    object sync_object = new object();

    public RabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogger<RabbitMQPersistentConnection> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }
    
    
    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;

        try
        {
            _connection.Dispose();
        }
        catch (IOException ex)
        {
            _logger.LogCritical(ex.ToString());
        }
    }

    public bool IsConnected
    {
        get
        {
            return _connection != null && _connection.IsOpen && !_disposed;
        }
    }
    
    public bool TryConnect()
    {
        _logger.LogInformation("RabbitMQ Client proba polaczenia");

        lock (sync_object)
        {
            _connection = _connectionFactory.CreateConnection();


            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                _logger.LogInformation("RabbitMQ Client uzyskal polaczenie do '{HostName}'", _connection.Endpoint.HostName);

                return true;
            }
            else
            {
                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                return false;
            }
        }
    }

    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }

        return _connection.CreateModel();
    }
    
    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

        TryConnect();
    }

    private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

        TryConnect();
    }

    private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

        TryConnect();
    }
}