using Newtonsoft.Json;
using WeatherBot.API.Models;

namespace WeatherBot.API.Services.Implementations
{
    public class CurrentWeatherService: ICityWeatherResolver
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
            var apiPattern = _configuration["CurrentWeatherAPI"];
            var appid = _configuration["OpenWeatherAppId"];
            var url = string.Format(apiPattern ?? "", geocodingCity.Lat, geocodingCity.Lon, appid);

            var responce = await _httpClient.GetAsync(url);
            var responceContent = await responce.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CurrentWeatherCityModel>(responceContent);
        }

    }
}
