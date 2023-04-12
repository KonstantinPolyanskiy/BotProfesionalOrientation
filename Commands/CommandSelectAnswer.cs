using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;
using TelegramBot.Configs;
using TelegramBot.Services.Interfaces;
using TelegramBot.Utilities;

namespace TelegramBot.Commands
{
    public sealed class CommandSelectAnswer : IRunnable
    {
        private readonly Config _config;
        private readonly IUserService _userService;
        private readonly ITestService _testService;

        public CommandSelectAnswer(Config config , ITestService testService, IUserService userService)
        {
            _config = config;
            _testService = testService;
            _userService = userService;
        }

        public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            if (args.Length < 2) return;
            if (!int.TryParse(args[1], out int selectedIndex)) return;

            long chatId = update.CallbackQuery.Message.Chat.Id;
            long userId = update.CallbackQuery.From.Id;
            int messageId = update.CallbackQuery.Message.MessageId;

            // Пользователь не проходит тест
            if (!_testService.UserPassesTest(userId))
            {
                await botClient.DeleteMessageAsync
                (
                    chatId: chatId,
                    messageId: messageId
                );

                return;
            }

            // Есть вопросы
            if ( _testService.HasQuestions(userId) )
            {
                _testService.SelectAnswer(userId, selectedIndex);

                await botClient.EditMessageTextAsync
                (
                    chatId: chatId,
                    messageId: messageId,
                    text: $"\n{_testService.GetCurrentIndex(userId) + 1}. {_testService.GetQuestionName(userId)}",
                    replyMarkup: Utils.CreateInlineKeyboardWithAnswers(_testService.GetQuestionAnswers(userId))
                );

                return;
            }

            await botClient.DeleteMessageAsync
            (
                chatId: chatId,
                messageId: messageId
            );

            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: _config.TextMessages["testpassed"],
                replyMarkup: _config.ReplyMarkups["tests"]
            );

            if ( _testService.IsHollandTestRunning(userId) )
            {
                await _userService.UpdateUserAsync
                (
                    userId: userId,
                    hollandResultId: _testService.GetResult(userId)
                );

                return;
            }

            if (_testService.IsKlimovTestRunning(userId))
            {
                await _userService.UpdateUserAsync
                (
                    userId: userId,
                    klimovResultId: _testService.GetResult(userId)
                );

                return;
            }
        }

        public Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotSupportedException();
        }
    }
}
