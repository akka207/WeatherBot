using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.RegularExpressions;
using WeatherBot.API.Utils;
using WeatherBot.API.Models;

namespace WeatherBot.API.Services.Implementations
{
    public class TelegramClientService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageCreator _messageCreator;
        private readonly DBQueryService _dbQueryService;

        private static TelegramBotClient _bot;

        public TelegramClientService(IConfiguration configuration,
            IMessageCreator messageCreator,
            DBQueryService dBQueryService)
        {
            _configuration = configuration;
            _messageCreator = messageCreator;
            _dbQueryService = dBQueryService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var botToken = _configuration.GetTelegramBotToken();

            using var cts = new CancellationTokenSource();
            _bot = new TelegramBotClient(botToken, cancellationToken: cts.Token);

            _bot.OnError += OnError;
            _bot.OnMessage += OnMessage;
            _bot.OnUpdate += OnUpdate;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


        public async Task BroadcastWeatherMessage()
        {
            List<Models.User> users = await _dbQueryService.ReadAllUsersAsync();

            foreach (Models.User user in users)
            {
                await SendMessageAsync(user.ChatId, user.Id, user.SelectedCity);
            }
        }


        private async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine(exception);
        }

        private async Task OnMessage(Message msg, UpdateType type)
        {
            var commandPattern = @"^(\/\w+)\s*?(\S+)?$";
            var rg = new Regex(commandPattern);

            if (!rg.IsMatch(msg.Text))
            {
                await _bot.SendMessage(msg.Chat, "Invalid request.\nIs your command right?");
            }

            var match = rg.Match(msg.Text);

            if (match.Groups[1].Value == MessageCommand.Start.Format())
            {
                await _bot.SendMessage(msg.Chat,
                    $"Welcome!\nNow you have to select city\n{MessageCommand.Weather.Format()} {{city}}");

                Models.User user = new Models.User();
                user.Id = msg.From!.Id;
                user.ChatId = msg.Chat.Id;
                await _dbQueryService.SaveOrUpdateUserAsync(user);
            }
            else if (match.Groups[1].Value == MessageCommand.Weather.Format() && match.Groups.Count == 3)
            {
                Models.User user = await _dbQueryService.ReadUserAsync(msg.From.Id);
                string city = match.Groups[2].Value;

                user.SelectedCity = city;
                await _dbQueryService.SaveOrUpdateUserAsync(user);

                await SendMessageAsync(msg.Chat.Id, msg.From.Id, city);
            }
        }

        private async Task OnUpdate(Update update)
        {
            if (update is { CallbackQuery: { } query })
            {
                await SendMessageAsync(query.Message.Chat.Id, query.From.Id, query.Data);

                await _bot.DeleteMessage(query.Message.Chat.Id, query.Message.MessageId);
            }
        }

        private async Task SendMessageAsync(long chatId, long userId, string city)
        {
            await _bot.SendMessage(chatId,
                    await _messageCreator.GetMessageAsync(city),
                    ParseMode.Html,
                    replyMarkup: new InlineKeyboardButton("Update", $"{city}"));

            WeatherHistoryItem weatherHistoryItem = new WeatherHistoryItem();
            weatherHistoryItem.UserId = userId;
            weatherHistoryItem.Query = city;
            weatherHistoryItem.Date = DateTime.Now;
            await _dbQueryService.SaveWeatherHistoryItemAsync(weatherHistoryItem);
        }
    }
}
