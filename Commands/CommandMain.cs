using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;
using TelegramBot.Configs;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Commands
{
    public sealed class CommandMain : IRunnable
    {
        private readonly Config _config;

        public CommandMain(Config config)
        {
            _config = config;
        }

        public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
#nullable disable

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
                text: _config.TextMessages["main"],
                replyMarkup: _config.ReplyMarkups["main"]
            );
        }

        public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
#nullable disable
            long chatId = update.Message.Chat.Id;

            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: _config.TextMessages["main"],
                replyMarkup: _config.ReplyMarkups["main"]
            );
        }
    }
}
