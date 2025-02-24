using Microsoft.AspNetCore.Mvc;
using WeatherBot.API.Models;
using WeatherBot.API.Services.Implementations;

namespace WeatherBot.API.Controllers
{
    [ApiController]
    [Route("weatherHistory")]
    public class WeatherHistoryController : ControllerBase
    {
        private readonly DBQueryService _dBQueryService;

        public WeatherHistoryController(DBQueryService dBQueryService)
        {
            _dBQueryService = dBQueryService;
        }


        [HttpGet]
        public async Task<IEnumerable<WeatherHistoryItem>> GetWeatherHistoryAsync()
        {
            return await _dBQueryService.ReadWeatherHistoryAsync();
        }

        [HttpGet("{id}")]
        public async Task<WeatherHistoryItem> GetWeatherHistoryItemAsync(int id)
        {
            return await _dBQueryService.ReadWeatherHistoryItemAsync(id);
        }

        [HttpPost]
        public async Task PostUserAsync([FromBody] WeatherHistoryItem whItem)
        {
            await _dBQueryService.SaveWeatherHistoryItemAsync(whItem);
        }
    }
}
