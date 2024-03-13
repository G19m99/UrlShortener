using UrlShortener.Core.Models;

namespace UrlShortener.Core.Interfaces;

public interface IShortUrlsService
{
    ShortUrlsModel? CreateShortUrl(string key);
    bool DeleteShortUrl(string key);
    ShortUrlsModel GetShortUrl(string key);
}