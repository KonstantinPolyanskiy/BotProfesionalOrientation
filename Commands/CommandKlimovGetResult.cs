using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;

namespace TelegramBot.Commands
{
    public sealed class CommandKlimovGetResult : IRunnable
    {
        public async Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotImplementedException();
        }

        public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
