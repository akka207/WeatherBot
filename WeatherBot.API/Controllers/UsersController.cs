using Microsoft.AspNetCore.Mvc;
using WeatherBot.API.Models;
using WeatherBot.API.Services.Implementations;

namespace WeatherBot.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly DBQueryService _dBQueryService;
        private readonly TelegramClientService _telegramClientService;

        public UsersController(DBQueryService dBQueryService,
            TelegramClientService telegramClientService)
        {
            _dBQueryService = dBQueryService;
            _telegramClientService = telegramClientService;
        }


        [HttpGet]
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dBQueryService.ReadAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserAsync(int id)
        {
            return await _dBQueryService.ReadUserAsync(id);
        }

        [HttpPost]
        public async Task PostUserAsync([FromBody] User user)
        {
            await _dBQueryService.SaveOrUpdateUserAsync(user);
        }
    }
}
