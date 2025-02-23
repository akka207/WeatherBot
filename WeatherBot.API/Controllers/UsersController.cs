using Microsoft.AspNetCore.Mvc;
using WeatherBot.API.Models;
using WeatherBot.API.Services;

namespace WeatherBot.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly GeocodingService _geocodingService;
        private readonly CurrentWeatherService _currentWeatherService;
        private readonly DBQueryService _dBQueryService;

        public UsersController(GeocodingService geocodingService,
            CurrentWeatherService currentWeatherService,
            DBQueryService dBQueryService)
        {
            _geocodingService = geocodingService;
            _currentWeatherService = currentWeatherService;
            _dBQueryService = dBQueryService;
        }


        [HttpGet(Name = "GetUsers")]
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dBQueryService.ReadAllUsersAsync();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<User> GetUserAsync(int id)
        {
            return await _dBQueryService.ReadUserAsync(id);
        }

        [HttpPost(Name = "PostUser")]
        public async Task PostUserAsync([FromBody] User user)
        {
            await _dBQueryService.SaveUserAsync(user);
        }
    }
}
