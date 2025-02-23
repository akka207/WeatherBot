using Dapper;
using System.Data;
using WeatherBot.API.Models;

namespace WeatherBot.API.Services
{
    public class DBQueryService
    {
        private readonly IDbConnection _connection;

        public DBQueryService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<User>> ReadAllUsersAsync()
        {
            var sql = "SELECT * FROM Users";
            List<User> users = (await _connection.QueryAsync<User>(sql)).ToList();

            foreach (var user in users)
            {
                await IncludeAsync(user);
            }

            return users;
        }

        public async Task<User> ReadUserAsync(long id)
        {
            var sql = $"SELECT * FROM Users U WHERE U.Id={id}";
            User user = (await _connection.QueryAsync<User>(sql)).FirstOrDefault();

            await IncludeAsync(user);

            return user;
        }

        public async Task SaveOrUpdateUserAsync(User user)
        {
            List<User> users = await ReadAllUsersAsync();

            string sql;
            if (users.Any(u => u.Id == user.Id))
            {
                sql = $"UPDATE Users SET SelectedCity=@SelectedCity WHERE Id=@Id";
            }
            else
            {
                sql = "INSERT INTO Users (Id, SelectedCity, ChatId) VALUES (@Id, @SelectedCity, @ChatId)";
            }
            await _connection.ExecuteAsync(sql, user);
        }


        public async Task<List<WeatherHistoryItem>> ReadWeatherHistoryAsync()
        {
            var sql = "SELECT * FROM WeatherHistory";
            List<WeatherHistoryItem> weatherHistory = (await _connection.QueryAsync<WeatherHistoryItem>(sql)).ToList();

            return weatherHistory;
        }

        public async Task<WeatherHistoryItem> ReadWeatherHistoryItemAsync(int id)
        {
            var sql = $"SELECT * FROM WeatherHistory WH WHERE WH.Id={id}";
            WeatherHistoryItem whItem = (await _connection.QueryAsync<WeatherHistoryItem>(sql)).FirstOrDefault();

            return whItem;
        }

        public async Task SaveWeatherHistoryItemAsync(WeatherHistoryItem item)
        {
            var sql = "INSERT INTO WeatherHistory (Date, Query, UserId) VALUES (@Date, @Query, @UserId)";
            await _connection.ExecuteAsync(sql, item);
        }


        private async Task IncludeAsync(User user)
        {
            var sql = $"SELECT * FROM Users U INNER JOIN WeatherHistory WH ON U.Id=WH.UserId WHERE U.Id={user.Id}";
            user.WeatherHistory = (await _connection.QueryAsync<WeatherHistoryItem>(sql)).ToList();
        }
    }
}
