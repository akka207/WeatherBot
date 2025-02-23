using Newtonsoft.Json;

namespace WeatherBot.API.Models
{
    public class GeocodingCityModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        public GeocodingCityModel() { }
        public GeocodingCityModel(string name, double lat, double lon, string country)
        {
            Name = name;
            Lat = lat;
            Lon = lon;
            Country = country;
        }
    }
}
