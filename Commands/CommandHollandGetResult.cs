using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;
using TelegramBot.Configs;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Commands
{
    public sealed class CommandHollandGetResult : IRunnable
    {
        private readonly Config _config;
        private readonly IUserService _userService;

        public CommandHollandGetResult(Config config, IUserService userService)
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

            var user = await _userService.GetUserAsync(userId);

            if ( user.HollandResult is null )
            {
                await botClient.SendTextMessageAsync
                (
                    chatId: chatId,
                    text: _config.TextMessages["testhollandnotpassed"],
                    replyMarkup: _config.ReplyMarkups["testhollandnotpassed"]
                );

                return;
            }

            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: $"Ваш тип личности - {user.HollandResult.Name}.\n{user.HollandResult.Description}",
                replyMarkup: _config.ReplyMarkups["main"]
            );
        }
    }
}
