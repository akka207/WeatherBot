using Swashbuckle.AspNetCore.Annotations;

namespace WeatherBot.API.Models
{
    public class WeatherHistoryItem
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Query { get; set; }
        public long UserId { get; set; }
    }
}
