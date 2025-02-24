using System.Data.SqlClient;
using System.Data;
using WeatherBot.API.Services;
using WeatherBot.API.Services.Implementations;

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
            services.AddSingleton<ICityLocationResolver, GeocodingService>();
            services.AddSingleton<ICityWeatherResolver, CurrentWeatherService>();
            services.AddSingleton<IMessageCreator, WeatherReportCreationService>();
        }

        public static void AddTelegramBot(this IServiceCollection services)
        {
            services.AddSingleton<TelegramClientService>();
            services.AddHostedService<TelegramClientService>();
        }
    }
}
