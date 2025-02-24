using Microsoft.AspNetCore.Mvc;
using WeatherBot.API.Models;
using WeatherBot.API.Services.Implementations;

namespace WeatherBot.API.Controllers
{
    [ApiController]
    [Route("broadcast")]
    public class BroadcastController : ControllerBase
    {
        private readonly DBQueryService _dBQueryService;
        private readonly TelegramClientService _telegramClientService;

        public BroadcastController(DBQueryService dBQueryService,
            TelegramClientService telegramClientService)
        {
            _dBQueryService = dBQueryService;
            _telegramClientService = telegramClientService;
        }


        [HttpPost("sendWeatherToAll")]
        public async Task PostWeatherAsync()
        {
            await _telegramClientService.BroadcastWeatherMessage();
        }
    }
}
