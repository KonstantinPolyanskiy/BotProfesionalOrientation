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
    public sealed class CommandAboutBot : IRunnable
    {
        private readonly Config _config;

        public CommandAboutBot(Config config)
        {
            _config = config;
        }

        public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotSupportedException("Команда AboutBot работает только с MessageUpdate");
        }

        public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
#nullable disable
            var chatId = update.Message.Chat.Id;

            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: _config.TextMessages["aboutbot"],
                replyMarkup: _config.ReplyMarkups["aboutbot"]
            );
        }
    }
}
