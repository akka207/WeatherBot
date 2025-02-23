using Swashbuckle.AspNetCore.Annotations;

namespace WeatherBot.API.Models
{
    public class User
    {
        [SwaggerSchema(ReadOnly = true)]
        public long Id { get; set; }
        public string? SelectedCity { get; set; } = null;
        public long ChatId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public List<WeatherHistoryItem>? WeatherHistory { get; set; }
    }
}
