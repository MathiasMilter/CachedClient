namespace CachedClient.Infrastructure.Clients.WeatherClient;

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

public class OpenWeatherClient : IWeatherClient
{
    private readonly IOptions<OpenWeatherClientConfiguration> _openWeatherConfig;
    private readonly HttpClient _httpClient;

    public OpenWeatherClient(IOptions<OpenWeatherClientConfiguration> openWeatherConfig, HttpClient httpClient)
    {
        _openWeatherConfig = openWeatherConfig;
        _httpClient = httpClient;
    }
    
    public async Task<WeatherReport> GetCurrentWeather()
    {
        var response = await _httpClient
            .GetAsync(
                $"/data/2.5/weather?lat={55.676098}&lon={12.568337}&appid={_openWeatherConfig.Value.ApiKey}");

        var weatherResponse = JsonConvert
            .DeserializeObject<WeatherResponse>(
                await response.Content.ReadAsStringAsync());

        return new WeatherReport()
        {
            WeatherDescription = weatherResponse?.weather?.FirstOrDefault()?.description ?? "",
            WeatherType = weatherResponse?.weather?.FirstOrDefault()?.main ?? ""
        };
    }
}

public class WeatherResponse
{
    public List<Weather> weather { get; set; }
}
public class Weather
{
    public string main { get; set; }
    public string description { get; set; }
}


