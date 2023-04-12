using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;
using TelegramBot.Configs;
using TelegramBot.Databases.Types;
using TelegramBot.Services;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Commands
{
    public sealed class CommandGenerateCertificate : IRunnable
    {
        private readonly Config _config;
        private readonly ICertificateService _certificateService;

        public CommandGenerateCertificate(Config config, ICertificateService certificateService)
        {
            _config = config;
            _certificateService = certificateService;
        }

        public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotSupportedException();
        }

        public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            long chatId = update.Message.Chat.Id;
            IInputFile inputFile;

            using (var streamImage = await _certificateService.CreateHollandCertificateAsync("Тест Тест", "Реалистичный"))
            {
                inputFile = new InputFile(streamImage);

                await botClient.SendPhotoAsync
                (
                    chatId: chatId,
                    caption: "Сертификат Голланда",
                    photo: inputFile
                );
            }
        }
    }
}
