using StackExchange.Redis;
using System.Text.Json;

namespace FullStackUrlShortener.Server.Infrastructure.Redis;

public class RedisService : IRedisService
{
    private readonly ConfigurationOptions config;
    private Lazy<IConnectionMultiplexer> _Connection;

    public RedisService(string connectionString)
    {
        config = ConfigurationOptions.Parse(connectionString);
        _Connection = new Lazy<IConnectionMultiplexer>(() =>
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config);

            return redis;
        });
    }
    public IConnectionMultiplexer Connection { get { return _Connection.Value; } }
    public IDatabase Database => Connection.GetDatabase();

    public bool KeyExist(RedisKey key)
    {
        return Database.KeyExists(key);
    }

    public T? JsonGet<T>(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        RedisValue rv = Database.StringGet(key, flags);
        if (!rv.HasValue || rv.IsNull)
        {
            return default;
        }
        T rvDeserialized = JsonSerializer.Deserialize<T>(rv!)!;
        return rvDeserialized;
    }

    public bool JsonSet(RedisKey key, object value)
    {
        if (value == null) return false;
        return Database.StringSet(key, JsonSerializer.Serialize(value));
    }

    public bool DeleteKey(RedisKey key)
    {
        return Database.KeyDelete(key);
    }


}
