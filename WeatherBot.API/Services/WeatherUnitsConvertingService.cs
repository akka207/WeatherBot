namespace WeatherBot.API.Services
{
    public class WeatherUnitsConvertingService
    {
        public float KelvinToCelsius(float kelvin)
        {
            return kelvin - 272.15f;
        }

        public string DegreeToDirection(float degree)
        {
            degree %= 360;
            string direction = "";

            switch (degree)
            {
                case >= 345:
                    direction = "N";
                    break;
                case < 15:
                    direction = "N";
                    break;
                case < 35:
                    direction = "N/NE";
                    break;
                case < 55:
                    direction = "NE";
                    break;
                case < 75:
                    direction = "E/NE";
                    break;
                case < 105:
                    direction = "E";
                    break;
                case < 125:
                    direction = "E/SE";
                    break;
                case < 145:
                    direction = "SE";
                    break;
                case < 165:
                    direction = "S/SE";
                    break;
                case < 195:
                    direction = "S";
                    break;
                case < 215:
                    direction = "S/SW";
                    break;
                case < 235:
                    direction = "SW";
                    break;
                case < 255:
                    direction = "W/SW";
                    break;
                case < 285:
                    direction = "W";
                    break;
                case < 305:
                    direction = "W/NW";
                    break;
                case < 325:
                    direction = "NW";
                    break;
                case < 345:
                    direction = "NW";
                    break;
            }

            return direction;
        }
    }
}
