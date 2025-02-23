using Swashbuckle.AspNetCore.Annotations;

namespace WeatherBot.API.Models
{
    public class User
    {
        [SwaggerSchema(ReadOnly=true)]
        public int Id { get; set; }
        public string Username { get; set; }
        [SwaggerSchema(ReadOnly=true)]
        public List<WeatherHistoryItem>? WeatherHistory { get; set; }
    }
}
