using FullStackUrlShortener.Server.Models;
using FullStackUrlShortener.Server.Services.Redis;
using Microsoft.AspNetCore.Mvc;

namespace FullStackUrlShortener.Server.Features.GetShortUrl;

[ApiController]
[Route("api/urls")]
public class GetShortUrlController(IGetShortUrlHandler handler) : Controller
{
    private readonly IGetShortUrlHandler _handler = handler;

    [HttpGet("{key}")]
    public IActionResult GetUrl([FromRoute] string key)
    {
        ShortenedUrl result = _handler.Get(key);
        return Ok(result);
    }
}

public interface IGetShortUrlHandler
{
    ShortenedUrl Get(string key);
}

internal class GetShortUrlHandler(IRedisService redis) : IGetShortUrlHandler
{
    const string baseUrl = "https://localhost:7164/";
    private readonly IRedisService _redis = redis;
    
    public ShortenedUrl Get(string key)
    {
        try
        {
            var longUrl = _redis.JsonGet<string>(key) ?? throw new KeyNotFoundException();
            var result = new ShortenedUrl
            {
                Key = key,
                LongUrl = longUrl,
                ShortUrl = baseUrl + key
            };

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Not found {ex.Message}");
        }
    }
}

