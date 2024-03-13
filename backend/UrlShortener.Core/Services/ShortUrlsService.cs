using System.Data.Common;
using System.Security.Cryptography;
using UrlShortener.Core.Interfaces;
using UrlShortener.Core.Models;
using UrlShortener.Data.Interfaces;

namespace UrlShortener.Core.Services;

public class ShortUrlsService(IShortUrlsRepository shortUrlsRepository) : IShortUrlsService
{
    //TODO: base url should be set based on the running port
    private const string baseUrl = "http://localhost:5145/";
    private readonly IShortUrlsRepository _urlRepository = shortUrlsRepository;

    public ShortUrlsModel? CreateShortUrl(string longUrl)
    {
        string hashedKey = HashGenerator(longUrl);

        while (IsDuplicate(hashedKey))
        {
            ShortUrlsModel dataWithsameKey = GetShortUrl(hashedKey);
            if (dataWithsameKey.LongUrl == longUrl) return dataWithsameKey;
            hashedKey = HashGenerator(ResolveHashConflict(hashedKey));
        }

        bool created = _urlRepository.CreateShortUrl(hashedKey, longUrl);
        if (!created) return default;

        ShortUrlsModel result = new()
        {
            Key = hashedKey,
            LongUrl = longUrl,
            ShortUrl = baseUrl + hashedKey
        };

        return result;
    }

    public bool DeleteShortUrl(string key)
    {
        bool keyExists = _urlRepository.FindShortUrl(key);
        if (!keyExists) throw new KeyNotFoundException();

        return _urlRepository.DeleteShortUrl(key);
    }

    public ShortUrlsModel GetShortUrl(string key)
    {
        string url = _urlRepository.GetShortUrl(key) ?? throw new KeyNotFoundException();
        ShortUrlsModel result = new()
        {
            Key = key,
            LongUrl = url,
            ShortUrl = baseUrl + key
        };

        return result;
    }

    private static string HashGenerator(string key)
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
        byte[] hashBytes = MD5.HashData(inputBytes);

        string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        return hashString[..8];
    }

    private bool IsDuplicate(string hashKey)
    {
        return _urlRepository.FindShortUrl(hashKey);
    }

    private static string ResolveHashConflict(string originalHash)
    {
        return originalHash + "a";
    }
}