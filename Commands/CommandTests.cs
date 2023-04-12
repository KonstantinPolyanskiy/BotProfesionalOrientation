using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    public sealed class CommandTests : IRunnable
    {
        private readonly Config _config;

        public CommandTests(Config config)
        {
            _config = config;
        }

        public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;
            int messageId = update.CallbackQuery.Message.MessageId;

            await botClient.DeleteMessageAsync
            (
                chatId: chatId,
                messageId: messageId
            );

            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: _config.TextMessages["tests"],
                replyMarkup: _config.ReplyMarkups["tests"]
            );
        }

        public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            var chatId = update.Message.Chat.Id;

            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: _config.TextMessages["tests"],
                replyMarkup: _config.ReplyMarkups["tests"]
            );
        }
    }
}
