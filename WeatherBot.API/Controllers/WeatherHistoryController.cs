using Microsoft.AspNetCore.Mvc;
using WeatherBot.API.Models;
using WeatherBot.API.Services;

namespace WeatherBot.API.Controllers
{
    [ApiController]
    [Route("weatherHistory")]
    public class WeatherHistoryController : ControllerBase
    {
        private readonly GeocodingService _geocodingService;
        private readonly CurrentWeatherService _currentWeatherService;
        private readonly DBQueryService _dBQueryService;

        public WeatherHistoryController(GeocodingService geocodingService,
            CurrentWeatherService currentWeatherService,
            DBQueryService dBQueryService)
        {
            _geocodingService = geocodingService;
            _currentWeatherService = currentWeatherService;
            _dBQueryService = dBQueryService;
        }


        //[HttpGet("{city}", Name = "GetCity")]
        //public async Task<CurrentWeatherCityModel> GetAsync(string city)
        //{
        //    GeocodingCityModel geocodingCity = await _geocodingService.GetCityModelAsync(city);
        //    CurrentWeatherCityModel currentWeatherCityModel = await _currentWeatherService.GetCityModelAsync(geocodingCity);
        //    return currentWeatherCityModel;
        //}


        [HttpGet(Name = "GetWeatherHistory")]
        public async Task<IEnumerable<WeatherHistoryItem>> GetWeatherHistoryAsync()
        {
            return await _dBQueryService.ReadWeatherHistoryAsync();
        }

        [HttpGet("{id}", Name = "GetWeatherHistoryItem")]
        public async Task<WeatherHistoryItem> GetWeatherHistoryItemAsync(int id)
        {
            return await _dBQueryService.ReadWeatherHistoryItemAsync(id);
        }

        [HttpPost(Name = "PostWeatherHistoryItem")]
        public async Task PostUserAsync([FromBody] WeatherHistoryItem whItem)
        {
            await _dBQueryService.SaveWeatherHistoryItemAsync(whItem);
        }
    }
}
