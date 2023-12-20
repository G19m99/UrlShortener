using FullStackUrlShortener.Server.Models;

namespace FullStackUrlShortener.Server.Services.UrlShortener;

public interface IShortenRepository
{
    Task<urlShortenResponse> Create(string url);
    Task<urlShortenResponse> Get(string key);
    Task DeleteKey(string key);
}
