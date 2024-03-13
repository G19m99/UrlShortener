using StackExchange.Redis;

namespace UrlShortener.Data.Interfaces;

public interface IShortUrlsRepository
{
    string? GetShortUrl(RedisKey key, CommandFlags flags = CommandFlags.None);
    bool CreateShortUrl(string hashedKey, string longUrl);
    bool FindShortUrl(RedisKey key);
    bool DeleteShortUrl(string key);
}