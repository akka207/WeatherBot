using System.Runtime.CompilerServices;

namespace WeatherBot.API.Utils
{
    public enum MessageCommand
    {
        Start,
        Weather
    }

    public static class MessageCommandConverter
    {
        public static string Format(this MessageCommand command)
        {
            switch (command)
            {
                case MessageCommand.Start:
                    return "/start";
                case MessageCommand.Weather:
                    return "/weather";
                default:
                    return "";
            }
        }
    }
}
