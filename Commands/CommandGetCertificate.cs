using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands.Interfaces;
using TelegramBot.Configs;
using TelegramBot.Handlers.Interfaces;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Commands
{
    public sealed class CommandGetCertificate : IRunnable
    {
        private readonly Config _config;
        private readonly IUserService _userService;
        private readonly ICertificateService _certificateService;
        private ITraceableMessagesHandler _traceableMessagesHandler;

        public CommandGetCertificate(Config config, IUserService userService, ICertificateService certificateService, ITraceableMessagesHandler traceableMessagesHandler)
        {
            _config = config;
            _userService = userService;
            _certificateService = certificateService;

            _traceableMessagesHandler = traceableMessagesHandler;
        }

        public Task RunFromCallbackAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            throw new NotSupportedException();
        }

        private async Task HollandCertificateHandlerAsync(ITelegramBotClient botClient, Databases.Types.User user, long chatId)
        {
            IInputFile inputFile;

            if ( user.HollandCertificateFileId is not null )
            {
                inputFile = new InputFileId( user.HollandCertificateFileId );

                await botClient.SendPhotoAsync
                (
                    chatId: chatId,
                    caption: "Сертификат Голланда",
                    photo: inputFile
                );
            }
            else
            {
                using (var streamImage = await _certificateService.CreateHollandCertificateAsync(user.Name, user.HollandResult.Name))
                {
                    inputFile = new InputFile(streamImage);

                    var message = await botClient.SendPhotoAsync
                    (
                        chatId: chatId,
                        caption: "Сертификат Голланда",
                        photo: inputFile
                    );

                    await _userService.UpdateUserAsync(user.Id, hollandCertificateFileId: message.Photo[0].FileId);
                }
            }
        }

        public async Task RunFromMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string[] args)
        {
            long chatId = update.Message.Chat.Id;
            long userId = update.Message.From.Id;

            var user = await _userService.GetUserAsync(userId);

            if (user.HollandResult is null && user.KlimovResult is null)
            {
                await botClient.SendTextMessageAsync
                (
                    chatId: chatId,
                    text: _config.TextMessages["userhasnottested"],
                    replyMarkup: _config.ReplyMarkups["userhasnottested"]
                );

                return;
            }

            if ( user.Name is null )
            {
                await botClient.SendTextMessageAsync
                (
                    chatId: chatId,
                    text: "Введите ваше имя:"
                );

                _traceableMessagesHandler.AddCommand(chatId, new CommandSetName(_config, _userService));

                return;
            }

            await botClient.SendTextMessageAsync
            (
                chatId: chatId,
                text: "Ваши сертификаты:",
                replyMarkup: _config.ReplyMarkups["main"]
            );

            if ( user.HollandResult is not null )
            {
                await HollandCertificateHandlerAsync(botClient, user, chatId);
            }
        }
    }
}
