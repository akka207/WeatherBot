namespace WeatherBot.API.Utils
{
    public static class ConfigurationResolver
    {
        public static string GetMSSQLConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("MSSQLConnectionString");
        }

        public static string GetOpenWeatherGeocodingApiPatern(this IConfiguration configuration)
        {
            return configuration["OpenWeather:GeocodingAPI"];
        }

        public static string GetOpenWeatherCurrentWeatherApiPatern(this IConfiguration configuration)
        {
            return configuration["OpenWeather:CurrentWeatherAPI"];
        }

        public static string GetOpenWeatherAppId(this IConfiguration configuration)
        {
            return configuration["OpenWeather:OpenWeatherAppId"];
        }

        public static string GetTelegramBotToken(this IConfiguration configuration)
        {
            return configuration["Telegram:TelegramBotToken"];
        }
    }
}
