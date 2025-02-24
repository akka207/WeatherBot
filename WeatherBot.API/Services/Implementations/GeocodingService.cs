using Newtonsoft.Json;
using WeatherBot.API.Models;
using WeatherBot.API.Utils;

namespace WeatherBot.API.Services.Implementations
{
    public class GeocodingService: ICityLocationResolver
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
            string apiPattern = _configuration.GetOpenWeatherGeocodingApiPatern();
            string appid = _configuration.GetOpenWeatherAppId();
            string url = string.Format(apiPattern ?? "", city, appid);

            var responce = await _httpClient.GetAsync(url);
            var responceContent = await responce.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<GeocodingCityModel>>(responceContent).FirstOrDefault();
        }
    }
}
