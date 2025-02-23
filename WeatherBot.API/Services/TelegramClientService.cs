using static System.Runtime.InteropServices.JavaScript.JSType;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.RegularExpressions;

namespace WeatherBot.API.Services
{
    public class TelegramClientService
    {
        private readonly IConfiguration _configuration;
        private readonly WeatherReportCreationService _weatherReportCreationService;
        private TelegramBotClient _bot;

        public TelegramClientService(IConfiguration configuration, WeatherReportCreationService weatherReportCreationService)
        {
            _configuration = configuration;
            _weatherReportCreationService = weatherReportCreationService;
        }

        public void Configure()
        {
            var botToken = _configuration["TelegramBotToken"];

            using var cts = new CancellationTokenSource();
            _bot = new TelegramBotClient(botToken, cancellationToken: cts.Token);

            _bot.OnError += OnError;
            _bot.OnMessage += OnMessage;
            _bot.OnUpdate += OnUpdate;
        }

        private async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine(exception);
        }

        private async Task OnMessage(Message msg, UpdateType type)
        {
            var commandPattern = @"^(\/\w+)\s*?(\w+)?$";
            var rg = new Regex(commandPattern);

            if (!rg.IsMatch(msg.Text))
            {
                await _bot.SendMessage(msg.Chat, "Invalid request.\nIs your command right?");
            }

            var match = rg.Match(msg.Text);

            if (match.Groups[1].Value == "/start")
            {
                await _bot.SendMessage(msg.Chat, "Welcome! Now you have to select city (/weather {city})",
                    replyMarkup: new InlineKeyboardButton[] { "Left", "Right" });
            }
            else if (match.Groups[1].Value == "/weather" && match.Groups.Count == 3)
            {
                string city = match.Groups[2].Value;
                await _bot.SendMessage(msg.Chat, await _weatherReportCreationService.GetMessageAsync(city), ParseMode.Html);
            }

        }

        private async Task OnUpdate(Update update)
        {
            if (update is { CallbackQuery: { } query })
            {
                await _bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
                await _bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
            }
        }
    }
}
