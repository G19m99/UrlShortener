using UrlShortener.Api.Models;
using UrlShortener.Core.Interfaces;
using UrlShortener.Core.Models;
using UrlShortener.Core.Services;
using UrlShortener.Data;
using UrlShortener.Data.Interfaces;
using UrlShortener.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = (context) =>
    {
        HttpContext httpContext = context.HttpContext;
        string traceId = httpContext.TraceIdentifier;
        string requestId = httpContext.Request.Headers["X-Request-Id"].ToString();

        context.ProblemDetails.Extensions["traceId"] = traceId;
        context.ProblemDetails.Extensions["requestId"] = requestId;
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IShortUrlsService, ShortUrlsService>();
builder.Services.AddScoped<IShortUrlsRepository, ShortUrlsRepository>();

string redisConnectionString = builder.Configuration.GetConnectionString("Redis")!;
builder.Services.AddSingleton(new RedisService(redisConnectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if(builder.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/shorturls/{key}", (string key, IShortUrlsService shortUrlsService) =>
{
    try
    {
        ShortUrlsModel url = shortUrlsService.GetShortUrl(key);
        return Results.Ok(url);
    }
    catch (KeyNotFoundException)
    {
        return Results.Problem(
            statusCode: StatusCodes.Status404NotFound,
            detail: $"key {key} not found"
        );
    }
    catch (Exception)
    {
        throw new Exception("Some error occured while attempting to retrieve records");
    }
});

app.MapPost("/api/shorturls", (CreateShortUrlModel urlReq, IShortUrlsService shortUrlsService) =>
{
    try
    {
        string cleanUrl = urlReq.LongUrl.ToLower().Trim();
        ShortUrlsModel created = shortUrlsService.CreateShortUrl(cleanUrl) ?? throw new Exception("Failed to create short url");
        return Results.Created($"/api/shorturls/{created.Key}", created);
    }
    catch (Exception)
    {
        throw new Exception("Some error occured while attempting to create a short url");
    }
});

app.MapDelete("/api/shorturls/{key}", (string key, IShortUrlsService shortUrlsService) =>
{
    try
    {
        if (shortUrlsService.DeleteShortUrl(key)) return Results.NoContent();
        throw new KeyNotFoundException();
    }
    catch (KeyNotFoundException)
    {
        return Results.Problem(
            statusCode: StatusCodes.Status404NotFound,
            detail: $"key {key} not found"
        );
    }
    catch (Exception)
    {
        throw new Exception("Some error occured while attempting to delete records");
    }
});

app.MapGet("/{shortUrl}", (string shortUrl, IShortUrlsService shortUrlsService) =>
{
    try
    {
        ShortUrlsModel urlObj = shortUrlsService.GetShortUrl(shortUrl);
        return Results.Redirect(urlObj.LongUrl, true);
    }
    catch (Exception)
    {
        throw new Exception("Some error occured while attempting to redirect to url");
    }
});
app.UseCors("AllowAll");
app.Run();

