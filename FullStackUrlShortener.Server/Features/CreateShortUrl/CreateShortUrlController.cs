using FullStackUrlShortener.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullStackUrlShortener.Server.Features.CreateShortUrl;

[ApiController]
[Route("api/urls")]
public class CreateShortUrlController(ICreateShortUrlHandler handler) : Controller
{
    [HttpPost]
    public async Task<ActionResult<string>> CreateSmallUrl([FromBody] CreateShortUrlRequest req)
    {
        if (string.IsNullOrEmpty(req.Url))
        {
            return BadRequest("URL cannot be empty");
        }
        if (!Uri.IsWellFormedUriString(req.Url, UriKind.Absolute))
        {
            return BadRequest("Invalid URL format");
        }

        ShortenedUrl result = await handler.GenerateShortUrl(req);
        return Ok(result.ShortUrl);
    }
}
