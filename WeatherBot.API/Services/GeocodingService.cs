using Newtonsoft.Json;
using WeatherBot.API.Models;

namespace WeatherBot.API.Services
{
    public class GeocodingService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeocodingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<GeocodingCityModel> GetCityModelAsync(string city)
        {
            var apiPattern = _configuration["GeocodingAPI"];
            var appid = _configuration["OpenWeatherAppId"];
            var url = string.Format(apiPattern ?? "", city, appid);

            var responce = await _httpClient.GetAsync(url);
            var responceContent = await responce.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<GeocodingCityModel>>(responceContent).FirstOrDefault();
        }
    }
}
