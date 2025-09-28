using StackExchange.Redis;

namespace FullStackUrlShortener.Server.Infrastructure.Redis;

public interface IRedisService
{
    bool KeyExist(RedisKey key);
    T? JsonGet<T>(RedisKey key, CommandFlags flags = CommandFlags.None);
    bool JsonSet(RedisKey key, object value);
    bool DeleteKey(RedisKey key);
}
