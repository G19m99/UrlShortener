using FullStackUrlShortener.Server.Models;
using FullStackUrlShortener.Server.Services.Redis;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace FullStackUrlShortener.Server.Services.UrlShortener;

public class ShortenRepo : IShortenRepository
{
    const string baseUrl = "https://localhost:7164/";
    private readonly IRedisService _redis;
    public ShortenRepo(IRedisService redis)
    {
        _redis = redis;
    }
    public async Task<urlShortenResponse> Create(string url)
    {
        try
        {
            string hashedKey = HashingFunc(url);
            while (CheckDup(hashedKey))
            {
                var existingUrl = await Get(hashedKey);
                if (existingUrl.LongUrl == url) return existingUrl;
                hashedKey = ResolveHashConflict(hashedKey);
            }
            _redis.JsonSet(hashedKey, url);
            var res = new urlShortenResponse
            {
                Key = hashedKey,
                LongUrl = url,
                ShortUrl = baseUrl + hashedKey
            };
            return res;
        }
        catch(Exception ex)
        {
            throw new Exception($"Request Failed {ex.Message}");
        }

    }

    public Task DeleteKey(string key)
    {
        bool keyExists = _redis.KeyExist(key);
        if (keyExists)
        {
            _redis.DeleteKey(key);
        }
        return Task.CompletedTask;
    }

    public Task<urlShortenResponse> Get(string key)
    {
        try 
        {
            var longUrl = _redis.JsonGet<string>(key) ?? throw new KeyNotFoundException();
            var obj = new urlShortenResponse
            {
                Key = key,
                LongUrl = longUrl,
                ShortUrl = baseUrl + key
            };
            
            return Task.FromResult<urlShortenResponse>(obj);
        }
        catch (Exception ex)
        {
            throw new Exception($"Not found {ex.Message}");
        }
    }

    private static string HashingFunc(string key)
    {
        using var md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

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
