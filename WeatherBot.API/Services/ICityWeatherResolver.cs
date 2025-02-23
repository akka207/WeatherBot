using WeatherBot.API.Models;

namespace WeatherBot.API.Services
{
    public interface ICityWeatherResolver
    {
        Task<CurrentWeatherCityModel> GetCityModelAsync(GeocodingCityModel geocodingCity);
    }
}
