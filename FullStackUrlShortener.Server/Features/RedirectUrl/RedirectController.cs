using FullStackUrlShortener.Server.Features.GetShortUrl;
using FullStackUrlShortener.Server.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FullStackUrlShortener.Server.Features.RedirectUrl;

[ApiController]
public class RedirectController(IGetShortUrlHandler getUrl) : Controller
{
    private readonly IGetShortUrlHandler _getUrl = getUrl;

    [HttpGet("{key}")]
    public IActionResult RedirectToUrl([FromRoute] string key)
    {
        ShortenedUrl url = _getUrl.Get(key);
        return Redirect(url.LongUrl);
    }
}
