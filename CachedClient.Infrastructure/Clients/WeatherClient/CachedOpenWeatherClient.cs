namespace CachedClient.Infrastructure.Clients.WeatherClient;

using Domain.Entities;
using Domain.Interfaces;
using LazyCache;

public class CachedOpenWeatherClient : CachedClient, IWeatherClient
{
    private readonly IWeatherClient _weatherClient;
    
    public CachedOpenWeatherClient(IAppCache cache, IWeatherClient weatherClient) : base(cache)
    {
        _weatherClient = weatherClient;
    }

    public async Task<WeatherReport> GetCurrentWeather()
    {
        return await Cache.GetOrAddAsync("GetCurrentWeather",
            () => _weatherClient.GetCurrentWeather(),
            DateTimeOffset.UtcNow.AddMinutes(15));
    }
}