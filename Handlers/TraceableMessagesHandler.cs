using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;
using TelegramBot.Handlers.Interfaces;

namespace TelegramBot.Handlers
{
    public sealed class TraceableMessagesHandler : ITraceableMessagesHandler
    {
        private readonly Dictionary<long, IRunnable> commands;

        public void AddCommand(long chatId, IRunnable command) => commands[chatId] = command;

        public TraceableMessagesHandler()
        {
            commands = new Dictionary<long, IRunnable>(5);
        }

        private string[] GetCommandWithArguments(string? message)
        {
            if (message == null)
                return Array.Empty<string>();

            return message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        public async Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
#nullable disable
            string message = update.Message.Text;
            long chatId = update.Message.Chat.Id;

            if (commands.TryGetValue(chatId, out IRunnable command))
            {
                await command.RunFromMessageAsync(botClient, update, cancellationToken, GetCommandWithArguments(message));

                commands.Remove(chatId);
            }
        }
    }
}
