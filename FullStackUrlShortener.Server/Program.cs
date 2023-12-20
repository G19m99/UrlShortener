using FullStackUrlShortener.Server.Services.Redis;
using FullStackUrlShortener.Server.Services.UrlShortener;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddStackExchangeRedisCache(config =>
//{
//    config.Configuration = builder.Configuration.GetConnectionString("Redis");
//});
//builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
//{
//    string config = builder.Configuration.GetConnectionString("Redis");
//    return ConnectionMultiplexer.Connect(config);
//});
//builder.Services.AddSingleton(provider =>
//{
//    var connectionMultiplexer = provider.GetRequiredService<IConnectionMultiplexer>();
//    return connectionMultiplexer.GetDatabase();
//});
builder.Services.AddSingleton<IRedisService>(new RedisService(builder.Configuration.GetConnectionString("Redis")));
builder.Services.AddScoped<IShortenRepository, ShortenRepo>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
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
