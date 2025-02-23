namespace WeatherBot.API.Services
{
    public interface IMessageCreator
    {
        Task<string> GetMessageAsync(params string[] strings);
    }
}
