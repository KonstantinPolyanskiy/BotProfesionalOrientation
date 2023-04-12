using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Configs;
using TelegramBot.Services.Interfaces;
using TelegramBot.Commands.Interfaces;

namespace TelegramBot.Commands
{
    public sealed class CommandSetName : IRunnable
    {
        private readonly Config _config;
        private readonly IUserService _userService;

        public CommandSetName(Config config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotSupportedException();
        }

        public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            long chatId = update.Message.Chat.Id;
            long userId = update.Message.From.Id;
            string name = update.Message.Text;

            await _userService.UpdateUserAsync(userId, name: name);

            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: "Имя было изменено!",
                replyMarkup: _config.ReplyMarkups["main"]
            );
        }
    }
}
