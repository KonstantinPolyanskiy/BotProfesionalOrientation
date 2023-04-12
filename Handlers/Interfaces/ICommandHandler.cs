using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers.Interfaces
{
    public interface ICommandHandler
    {
        public Task HandleCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        public Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
