using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands;
using TelegramBot.Commands.Interfaces;
using TelegramBot.Configs;
using TelegramBot.Handlers.Interfaces;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Handlers
{
    public class CommandHandler : ICommandHandler
    {
        private readonly Dictionary<string, IRunnable> _messageCommands;
        private readonly Dictionary<string, IRunnable> _callbackCommands;

        private readonly IRunnable _commandMain;
        private readonly IRunnable _commandAboutBot;
        private readonly IRunnable _commandResults;
        private readonly IRunnable _commandTests;
        private readonly IRunnable _commandRunTestHolland;
        private readonly IRunnable _commandRunTestKlimov;
        private readonly IRunnable _commandSelectAnswer;
        private readonly IRunnable _commandStats;
        private readonly IRunnable _commandKlimovGetResult;
        private readonly IRunnable _commandHollandGetResult;
        private readonly IRunnable _commandGetCertificate;
        private readonly IRunnable _commandGenerateCertificate;

        private string[] GetArguments(string message)
        {
            return message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        public CommandHandler(Config config, ITestService testService, IUserService userService, IResultService resultService, ICertificateService certificateService, ITraceableMessagesHandler traceableMessagesHandler)
        {
            _messageCommands = new(StringComparer.OrdinalIgnoreCase);
            _callbackCommands = new(StringComparer.OrdinalIgnoreCase);

            _commandMain = new CommandMain(config);
            _commandAboutBot = new CommandAboutBot(config);
            _commandResults = new CommandResults(config);
            _commandTests = new CommandTests(config);
            _commandRunTestHolland = new CommandRunTestHolland(config, testService, userService);
            _commandRunTestKlimov = new CommandRunTestKlimov(testService);
            _commandSelectAnswer = new CommandSelectAnswer(config, testService, userService);
            _commandStats = new CommandStats(config, userService, resultService);
            _commandKlimovGetResult = new CommandKlimovGetResult();
            _commandHollandGetResult = new CommandHollandGetResult(config, userService);
            _commandGetCertificate = new CommandGetCertificate(config, userService, certificateService, traceableMessagesHandler);

            _callbackCommands["/main"] = _commandMain;
            _callbackCommands["/tests"] = _commandTests;
            _callbackCommands["/runtestholland"] = _commandRunTestHolland;
            _callbackCommands["/runtestklimov"] = _commandRunTestKlimov;
            _callbackCommands["/selectAnswer"] = _commandSelectAnswer;

            _messageCommands["/start"] = _commandMain;
            _messageCommands["🏠 Главная"] = _commandMain;
            _messageCommands["🏠 В главное меню"] = _commandMain;
            _messageCommands["👤 О боте"] = _commandAboutBot;
            _messageCommands["📖 Результаты"] = _commandResults;
            _messageCommands["📄 Тесты"] = _commandTests;
            _messageCommands["📝 Тест Голланда"] = _commandRunTestHolland;
            _messageCommands["📝 Тест Климова"] = _commandRunTestKlimov;
            _messageCommands["📊 Общая статистика"] = _commandStats;
            _messageCommands["📕 Результаты Голланда"] = _commandHollandGetResult;
            _messageCommands["📗 Результаты Климова"] = _commandKlimovGetResult;
            _messageCommands["📋 Сертификат"] = _commandGetCertificate;

            if ( config.DebugMode )
            {
                _commandGenerateCertificate = new CommandGenerateCertificate(config, certificateService);
                
                _messageCommands["/gencert"] = _commandGenerateCertificate;
            }
        }

        public async Task HandleCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string? message = update.CallbackQuery?.Data;

            if (message is null) return;

            string[] args = GetArguments(message);

            if (args.Length >= 1 && _callbackCommands.TryGetValue(args[0], out IRunnable? command) && command is not null)
            {
                await command.RunFromCallbackAsync(botClient, update, cancellationToken, args);
            }
        }

        public async Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
#nullable disable
            string? message = update.Message.Text ?? update.Message.Caption;

            if (message is not null && _messageCommands.TryGetValue(message, out IRunnable command) )
            {
                await command.RunFromMessageAsync(botClient, update, cancellationToken, GetArguments(message) );
            }
        }
    }
}
