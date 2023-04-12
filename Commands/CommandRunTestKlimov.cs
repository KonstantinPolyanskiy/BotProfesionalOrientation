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

namespace TelegramBot.Commands
{
    public sealed class CommandRunTestKlimov : IRunnable
    {
        private readonly ITestService _testService;

        public CommandRunTestKlimov(ITestService testService)
        {
            _testService = testService;
        }

        public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotImplementedException();
        }

        public Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
