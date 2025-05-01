using FullStackUrlShortener.Server.Features.CreateShortUrl;
using FullStackUrlShortener.Server.Features.GetShortUrl;
using FullStackUrlShortener.Server.Services.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRedisService>(new RedisService(builder.Configuration.GetConnectionString("Redis")!));
builder.Services.AddScoped<IGetShortUrlHandler, GetShortUrlHandler>();
builder.Services.AddScoped<ICreateShortUrlHandler, CreateShortUrlHandler>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
