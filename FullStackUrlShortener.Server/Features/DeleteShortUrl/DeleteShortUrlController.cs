using Microsoft.AspNetCore.Mvc;

namespace FullStackUrlShortener.Server.Features.DeleteShortUrl;

[ApiController]
[Route("api/urls")]
public class DeleteShortUrlController(IDeleteShortUrlHandler handler) : Controller
{
    private readonly IDeleteShortUrlHandler _handler = handler;

    [HttpDelete("{key}")]
    public IActionResult DeleteKey([FromRoute] string key)
    {
        _handler.Delete(key);
        return NoContent();
    }
}
