using Newtonsoft.Json;
using WeatherBot.API.Models;
using WeatherBot.API.Utils;

namespace WeatherBot.API.Services.Implementations
{
    public class CurrentWeatherService : ICityWeatherResolver
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurrentWeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<CurrentWeatherCityModel> GetCityModelAsync(GeocodingCityModel geocodingCity)
        {
            string apiPattern = _configuration.GetOpenWeatherCurrentWeatherApiPatern();
            string appid = _configuration.GetOpenWeatherAppId();
            string url = string.Format(apiPattern ?? "", geocodingCity.Lat, geocodingCity.Lon, appid);

            var responce = await _httpClient.GetAsync(url);
            string responceContent = await responce.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CurrentWeatherCityModel>(responceContent);
        }

    }
}
