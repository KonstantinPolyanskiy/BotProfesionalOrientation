using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;
using TelegramBot.Configs;
using TelegramBot.Databases.Types;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Commands
{
    public sealed class CommandStats : IRunnable
    {
        private readonly Config _config;
        private readonly IUserService _userService;
        private readonly IResultService _resultService;

        public CommandStats(Config config, IUserService userService, IResultService resultService)
        {
            _config = config;
            _userService = userService;
            _resultService = resultService;
        }

        public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotSupportedException();
        }

        public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            var builder = new StringBuilder();
            var hollandUsers = await _userService.GetAllUsersPassedHollandTestAsync();
            var klimovUsers = await _userService.GetAllUsersPassedKlimovTestAsync();

            int allCount = hollandUsers.Length + klimovUsers.Length;
            int klimovCount = klimovUsers.Length;
            int hollandCount = hollandUsers.Length;

            if (klimovCount == 0) ++klimovCount;
            if (hollandCount == 0) ++hollandCount;

            builder.AppendLine($"Пройдено тестов: {allCount}");
            builder.AppendLine("Результаты Климова:");

            foreach (var result in await _resultService.GetAllKlimovResultsAsync())
            {
                double countUsersResult = klimovUsers.Count(u => u.KlimovResult?.Id == result.Id);

                builder.Append(string.Format("{0} - {1:P2}\n", result.Name, countUsersResult / klimovCount));
            }

            builder.Append("\nРезультаты Голланда:\n");

            foreach (var result in await _resultService.GetAllHollandResultsAsync())
            {
                double countUsersResult = hollandUsers.Count(u => u.HollandResult?.Id == result.Id);

                builder.Append(string.Format("{0} - {1:P2}\n", result.Name, countUsersResult / hollandCount));
            }

            await botClient.SendTextMessageAsync
            (
                chatId: update.Message.Chat.Id,
                text: builder.ToString(),
                replyMarkup: _config.ReplyMarkups["main"]
            );
        }
    }
}
