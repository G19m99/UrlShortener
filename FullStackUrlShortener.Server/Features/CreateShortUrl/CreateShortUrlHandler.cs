using FullStackUrlShortener.Server.Infrastructure.Redis;
using FullStackUrlShortener.Server.Shared;
using System.Security.Cryptography;

namespace FullStackUrlShortener.Server.Features.CreateShortUrl;

public interface ICreateShortUrlHandler
{
    Task<ShortenedUrl> GenerateShortUrl(CreateShortUrlRequest req);
}

internal class CreateShortUrlHandler(IRedisService redis) : ICreateShortUrlHandler
{
    const string baseUrl = "https://localhost:7164/";
    private readonly IRedisService _redis = redis;

    public async Task<ShortenedUrl> GenerateShortUrl(CreateShortUrlRequest req)
    {
        try
        {
            string hashedKey = HashingFunc(req.Url);
            while (CheckDup(hashedKey))
            {
                var existingUrl = await Get(hashedKey);
                if (existingUrl.LongUrl == req.Url) return existingUrl;
                hashedKey = ResolveHashConflict(hashedKey);
            }
            _redis.JsonSet(hashedKey, req.Url);
            var res = new ShortenedUrl
            {
                Key = hashedKey,
                LongUrl = req.Url,
                ShortUrl = baseUrl + hashedKey
            };
            return res;
        }
        catch (Exception ex)
        {
            throw new Exception($"Request Failed {ex.Message}");
        }
    }

    public Task<ShortenedUrl> Get(string key)
    {
        string longUrl = _redis.JsonGet<string>(key) ?? throw new KeyNotFoundException();
        ShortenedUrl result = new()
        {
            Key = key,
            LongUrl = longUrl,
            ShortUrl = baseUrl + key
        };

        return Task.FromResult(result);
    }

    private static string HashingFunc(string key)
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
        byte[] hashBytes = MD5.HashData(inputBytes);

        string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        return hashString[..8];
    }

    private bool CheckDup(string hashKey)
    {
        return _redis.KeyExist(hashKey);
    }

    private static string ResolveHashConflict(string originalHash)
    {
        return originalHash + "a";
    }
}