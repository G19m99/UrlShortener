using FullStackUrlShortener.Server.Services.UrlShortener;
using Microsoft.AspNetCore.Mvc;

namespace FullStackUrlShortener.Server.Controllers;

[ApiController]
public class RedirectController(IShortenRepository repo) : Controller
{
    private readonly IShortenRepository _repo = repo;

    [HttpGet("{key}")]
    public async Task<IActionResult> RedirectToUrl([FromRoute] string key)
    {
        var urlObj = await _repo.Get(key);
        return Redirect(urlObj.LongUrl);
    }
}
