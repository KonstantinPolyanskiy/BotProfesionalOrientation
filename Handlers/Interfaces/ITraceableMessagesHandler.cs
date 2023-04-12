using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;

namespace TelegramBot.Handlers.Interfaces
{
    public interface ITraceableMessagesHandler
    {
        public void AddCommand(long chatId, IRunnable command);

        public Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
