using FullStackUrlShortener.Server.Models;
using FullStackUrlShortener.Server.Services.UrlShortener;
using Microsoft.AspNetCore.Mvc;

namespace FullStackUrlShortener.Server.Controllers;

[ApiController]
[Route("api/url-shorten")]
public class UrlShortenerController(IShortenRepository repo) : Controller
{
    private readonly IShortenRepository _repo = repo;

    [HttpPost]
    public async Task<IActionResult> CreateSmallUrl([FromBody] UrlShortenRequest req)
    {
        return Ok(await _repo.Create(req.Url));
    }
    [HttpGet("{key}")]
    public async Task<IActionResult> GetUrl([FromRoute] string key)
    {
        var urlObj = await _repo.Get(key);
        return Ok(urlObj);
    }
    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteKey([FromRoute] string key)
    {
        await _repo.DeleteKey(key);
        return Ok(); 
    }
}
