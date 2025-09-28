namespace FullStackUrlShortener.Server.Shared;

public class ShortenedUrl
{
    public string Key { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
    public string LongUrl { get; set; } = string.Empty;
}
