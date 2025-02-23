using WeatherBot.API.Models;

namespace WeatherBot.API.Services
{
    public class WeatherReportCreationService
    {
        private readonly GeocodingService _geocodingService;
        private readonly CurrentWeatherService _currentWeatherService;
        private readonly WeatherUnitsConvertingService _unitsConvertor;

        public WeatherReportCreationService(GeocodingService geocodingService,
            CurrentWeatherService currentWeatherService,
            WeatherUnitsConvertingService unitsConvertor)
        {
            _geocodingService = geocodingService;
            _currentWeatherService = currentWeatherService;
            _unitsConvertor = unitsConvertor;
        }

        public async Task<string> GetMessageAsync(params string[] strings)
        {
            string city = strings[0];
            GeocodingCityModel geocodingCity = await _geocodingService.GetCityModelAsync(city);
            CurrentWeatherCityModel model = await _currentWeatherService.GetCityModelAsync(geocodingCity);
            
            string uniteWeathers = "";
            foreach (Weather weather in model.Weathers)
            {
                uniteWeathers += $"{weather.Main} ({weather.Description})\n";
            }

            return
                $"<b>Weather for <u>{city}</u></b>\n" +
                $"{Math.Round(_unitsConvertor.KelvinToCelsius(model.Main.Temp))}°C " +
                $"(feels like {Math.Round(_unitsConvertor.KelvinToCelsius(model.Main.FeelsLike))}°C)\n" +
                $"{uniteWeathers}" +
                $"Wind: {model.Wind.Speed}m/s {_unitsConvertor.DegreeToDirection(model.Wind.Deg)}\n" +
                $"Clouds: {model.Clouds.All}%";
        }
    }
}
