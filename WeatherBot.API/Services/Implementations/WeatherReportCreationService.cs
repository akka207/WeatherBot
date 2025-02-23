using WeatherBot.API.Models;
using WeatherBot.API.Utils;

namespace WeatherBot.API.Services.Implementations
{
    public class WeatherReportCreationService : IMessageCreator
    {
        private readonly ICityLocationResolver _cityLocationResolver;
        private readonly ICityWeatherResolver _cityWeatherResolver;

        public WeatherReportCreationService(ICityLocationResolver cityLocationResolver,
            ICityWeatherResolver currentWeatherService)
        {
            _cityLocationResolver = cityLocationResolver;
            _cityWeatherResolver = currentWeatherService;
        }

        public async Task<string> GetMessageAsync(params string[] strings)
        {
            string city = strings[0];
            GeocodingCityModel? geocodingCity = await _cityLocationResolver.GetCityModelAsync(city);
            if (geocodingCity == null)
            {
                return "City not found";
            }
            CurrentWeatherCityModel? model = await _cityWeatherResolver.GetCityModelAsync(geocodingCity);
            if (model == null)
            {
                return "Weather not found";
            }

            string uniteWeathers = "";
            foreach (Weather weather in model.Weathers)
            {
                uniteWeathers += $"{weather.Main} ({weather.Description})\n";
            }

            return
                $"<b>Weather for <u>{city}</u></b>\n" +
                $"{Math.Round(WeatherUnitsConvertingService.KelvinToCelsius(model.Main.Temp))}°C " +
                $"(feels like {Math.Round(WeatherUnitsConvertingService.KelvinToCelsius(model.Main.FeelsLike))}°C)\n" +
                $"Weather: {uniteWeathers}" +
                $"Wind: {model.Wind.Speed}m/s \"{WeatherUnitsConvertingService.DegreeToDirection(model.Wind.Deg)}\"\n" +
                $"Clouds: {model.Clouds.All}%";
        }
    }
}
