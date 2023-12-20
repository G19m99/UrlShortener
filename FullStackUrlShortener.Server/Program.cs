using FullStackUrlShortener.Server.Services.Redis;
using FullStackUrlShortener.Server.Services.UrlShortener;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRedisService>(new RedisService(builder.Configuration.GetConnectionString("Redis")!));
builder.Services.AddScoped<IShortenRepository, ShortenRepo>();
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
