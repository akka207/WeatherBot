using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using WeatherBot.API.Services;

namespace WeatherBot.API.Utils
{
    public static class Initializers
    {
        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDbConnection>((options) =>
            {
                return new SqlConnection(connectionString);
            });
            services.AddSingleton<DBQueryService>();
        }

        public static void AddWeatherServices(this IServiceCollection services)
        {
            services.AddSingleton<GeocodingService>();
            services.AddSingleton<CurrentWeatherService>();
            services.AddSingleton<WeatherUnitsConvertingService>();
            services.AddSingleton<WeatherReportCreationService>();
        }

        public static void AddTelegramBot(this IServiceCollection services)
        {
            services.AddSingleton<TelegramClientService>();
            services.AddHostedService<TelegramClientService>();
        }
    }
}
