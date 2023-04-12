namespace TelegramBot;

using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using TelegramBot.Configs;
using TelegramBot.Databases;
using TelegramBot.Handlers;
using TelegramBot.Services;
using TelegramBot.Services.Interfaces;

sealed public class Program
{
#nullable disable

    private static Config _config;
    private static DatabaseFactory _factory;
    private static UpdateHandler _updateHandler;
    private static ITelegramBotClient _botClient;
    private static ReceiverOptions _receiverOptions;
    private static CancellationTokenRegistration _registration;
    private static ITestService _testService;
    private static IUserService _userService;
    private static IResultService _resultService;
    private static ICertificateService _certificateService;

    private static async Task Init()
    {
        _config = Config.LoadConfig();

        _receiverOptions = new ReceiverOptions() { AllowedUpdates = Array.Empty<UpdateType>() };

        _registration = new CancellationTokenRegistration();

        _factory = new DatabaseFactory(options => options.UseSqlite($"Data Source = {_config.DatabasePath}"));

        _userService = new UserService(_factory);

        _testService = new TestService();

        _certificateService = new CertificateService(_config);

        _resultService = new ResultService(_factory);

        using (var c = _factory.CreateContext()) await c.Database.EnsureCreatedAsync();

        _updateHandler = new UpdateHandler(_config, _userService, _testService, _resultService, _certificateService);

        _botClient = new TelegramBotClient(_config.BotKeyApi);
    }

    static async Task Main(string[] args)
    {
        if ( !Config.ConfigExists() )
        {
            Config.CreateDefaultConfig();

            return;
        }

        await Init();

        _botClient.StartReceiving
        (
            _updateHandler.HandleUpdateAsync,
            _updateHandler.HandlePollingErrorAsync,
            _receiverOptions,
            _registration.Token
        );

        Console.WriteLine("Бот запущен");

        while ( ! _registration.Token.IsCancellationRequested )
        {

        }
    }
}