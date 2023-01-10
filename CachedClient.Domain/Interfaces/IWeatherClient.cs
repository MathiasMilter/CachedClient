namespace CachedClient.Domain.Interfaces;

using Entities;

public interface IWeatherClient
{
    Task<WeatherReport> GetCurrentWeather();
}