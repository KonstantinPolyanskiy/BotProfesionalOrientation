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
    public sealed class CommandRunTestHolland : IRunnable
    {
        private readonly Config _config;
        private readonly ITestService _testService;
        private readonly IUserService _userService;

        public CommandRunTestHolland(Config config, ITestService testService, IUserService userService)
        {
            _config = config;
            _testService = testService;
            _userService = userService;
        }

        public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;
            long userId = update.CallbackQuery.From.Id;
            int messageId = update.CallbackQuery.Message.MessageId;

            _testService.RunHollandTest(userId);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(_testService.GetTestName(userId));
            stringBuilder.AppendLine(_testService.GetTestDescription(userId));
            stringBuilder.AppendLine($"\n{_testService.GetCurrentIndex(userId) + 1}. {_testService.GetQuestionName(userId)}");

            await botClient.EditMessageTextAsync
            (
                chatId: chatId,
                text: stringBuilder.ToString(),
                messageId: messageId,
                replyMarkup: Utils.CreateInlineKeyboardWithAnswers(_testService.GetQuestionAnswers(userId))
            );
        }

        public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            long chatId = update.Message.Chat.Id;
            long userId = update.Message.From.Id;

            if ( _testService.UserPassesTest(userId) )
            {
                // пользователь уже проходит тест
            }

            // Пользователь прошел тест
            if ( ( await _userService.GetUserAsync(userId) ).HollandResult is not null)
            {
                await botClient.SendTextMessageAsync
                (
                    chatId: chatId,
                    text: _config.TextMessages["testalreadypassed"],
                    replyMarkup: _config.ReplyMarkups["testhollandalreadypassed"]
                );

                return;
            }

            _testService.RunHollandTest(userId);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine( _testService.GetTestName(userId) );
            stringBuilder.AppendLine(_testService.GetTestDescription(userId));
            stringBuilder.AppendLine($"\n{_testService.GetCurrentIndex(userId) + 1}. {_testService.GetQuestionName(userId)}");

            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: stringBuilder.ToString(),
                replyMarkup: Utils.CreateInlineKeyboardWithAnswers( _testService.GetQuestionAnswers(userId ) )
            );
        }
    }
}
