using StackExchange.Redis;

namespace UrlShortener.Data;

public class RedisService
{
    private readonly ConfigurationOptions _config;
    private readonly Lazy<ConnectionMultiplexer> _connection;

    public RedisService(string connectionString)
    {
        _config = ConfigurationOptions.Parse(connectionString);
        _connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_config));
    }

    public ConnectionMultiplexer Connection => _connection.Value;

    public IDatabase Database => Connection.GetDatabase();
}