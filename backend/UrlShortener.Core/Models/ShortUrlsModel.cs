namespace UrlShortener.Core.Models;

public class ShortUrlsModel
{
    public string Key { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
    public string LongUrl { get; set; } = string.Empty;
}