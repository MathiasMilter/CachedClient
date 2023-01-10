using CachedClient.Domain.Interfaces;
using CachedClient.Infrastructure.Clients.WeatherClient;
using LazyCache;

var builder = WebApplication.CreateBuilder(args);

var accuWeatherUrl = new Uri(builder.Configuration
    .GetSection(nameof(OpenWeatherClientConfiguration))
    .Get<OpenWeatherClientConfiguration>()
    .URL);

builder.Services.AddHttpClient<OpenWeatherClient>(options =>
{
    options.BaseAddress = accuWeatherUrl;
});

builder.Services.AddScoped<IWeatherClient>(
    x => new CachedOpenWeatherClient(x.GetService<IAppCache>(), x.GetService<OpenWeatherClient>()));

builder.Services.Configure<OpenWeatherClientConfiguration>(
    builder.Configuration.GetSection(nameof(OpenWeatherClientConfiguration)));

builder.Services.AddLazyCache();

var app = builder.Build();

app.MapGet("/", (IWeatherClient weatherClient) => weatherClient.GetCurrentWeather());

app.Run();