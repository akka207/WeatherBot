using WeatherBot.API.Models;

namespace WeatherBot.API.Services
{
    public interface ICityLocationResolver
    {
        Task<GeocodingCityModel> GetCityModelAsync(string city);
    }
}
