using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using TelegramBot.Handlers.Interfaces;
using TelegramBot.Configs;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Handlers
{
    internal delegate Task Handler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

    public sealed class UpdateHandler : IUpdateHandler
    {
        private readonly IUserService _userService;

        private event Handler MessageHandler;
        private event Handler CallbackHandler;

        public UpdateHandler(Config config, IUserService userService, ITestService testService, IResultService resultService, ICertificateService certificateService)
        {
            ITraceableMessagesHandler traceableMessagesHandler = new TraceableMessagesHandler();

            ICommandHandler commandHandler = new CommandHandler(config, testService, userService, resultService, certificateService, traceableMessagesHandler);

            _userService = userService;

            MessageHandler += commandHandler.HandleMessageAsync;
            MessageHandler += traceableMessagesHandler.HandleMessageAsync;

            CallbackHandler += commandHandler.HandleCallbackAsync;
        }

#pragma warning disable CS1998
        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
#pragma warning restore CS1998
        {
            Console.WriteLine(exception.Message);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            long? userId = update.Message?.From?.Id;

            if ( userId is not null && ! await _userService.UserExistsAsync(userId.Value) )
            {
                await _userService.CreateUserAsync(userId.Value);
            }

            switch (update.Type)
            {
                case UpdateType.Message:
                    if (MessageHandler != null) await MessageHandler(botClient, update, cancellationToken);
                    break;

                case UpdateType.CallbackQuery:
                    if (CallbackHandler != null) await CallbackHandler(botClient, update, cancellationToken);
                    break;
            }
        }
    }
}
