using FullStackUrlShortener.Server.Infrastructure.Redis;

namespace FullStackUrlShortener.Server.Features.DeleteShortUrl;

public interface IDeleteShortUrlHandler
{
    void Delete(string key);
}
public class DeleteShortUrlHandler(IRedisService redis) : IDeleteShortUrlHandler
{
    private readonly IRedisService _redis = redis;
    public void Delete(string key)
    {
        _redis.DeleteKey(key);
    }
}
