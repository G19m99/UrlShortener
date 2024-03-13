using UrlShortener.Data.Interfaces;
using StackExchange.Redis;
using System.Text.Json;
namespace UrlShortener.Data.Repositories;

public class ShortUrlsRepository : IShortUrlsRepository
{
    private readonly IDatabase _db;
    public ShortUrlsRepository(RedisService redis)
    {
        ArgumentNullException.ThrowIfNull(redis);
        _db = redis.Database;
    }
    public bool CreateShortUrl(string hashedKey, string longUrl)
    {
        return _db.StringSet(hashedKey, JsonSerializer.Serialize(longUrl));
    }

    public bool DeleteShortUrl(string key)
    {
        return _db.KeyDelete(key);
    }

    public bool FindShortUrl(RedisKey key)
    {
        return _db.KeyExists(key);
    }

    public string? GetShortUrl(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        RedisValue url = _db.StringGet(key, flags);
        if (!url.HasValue || url.IsNull) return default;
        return JsonSerializer.Deserialize<string>(url!);
    }
}