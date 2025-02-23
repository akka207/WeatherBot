using Newtonsoft.Json;

namespace WeatherBot.API.Models
{
    public class CurrentWeatherCityModel
    {
        [JsonProperty("coord")]
        public Coord Coord { get; set; }
        [JsonProperty("weather")]
        public List<Weather> Weathers { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public CurrentWeatherCityModel() { }

        public CurrentWeatherCityModel(Coord coord, List<Weather> weathers, Main main, Wind wind, Clouds clouds, string name)
        {
            Coord = coord;
            Weathers = weathers;
            Main = main;
            Wind = wind;
            Clouds = clouds;
            Name = name;
        }
    }

    public class Coord
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }
        [JsonProperty("lat")]
        public double Lat { get; set; }

        public Coord() { }
        public Coord(double lon, double lat)
        {
            Lon = lon;
            Lat = lat;
        }
    }

    public class Weather
    {
        [JsonProperty("main")]
        public string Main { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        public Weather() { }
        public Weather(string main, string description)
        {
            Main = main;
            Description = description;
        }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public float Temp { get; set; }
        [JsonProperty("feels_like")]
        public float FeelsLike { get; set; }
        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        public Main() { }
        public Main(float temp, float feelsLike, int pressure, int humidity)
        {
            Temp = temp;
            FeelsLike = feelsLike;
            Pressure = pressure;
            Humidity = humidity;
        }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }
        [JsonProperty("deg")]
        public int Deg { get; set; }

        public Wind() { }
        public Wind(float speed, int deg)
        {
            Speed = speed;
            Deg = deg;
        }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }

        public Clouds() { }
        public Clouds(int all)
        {
            All = all;
        }
    }
}
