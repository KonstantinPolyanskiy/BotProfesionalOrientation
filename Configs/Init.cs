using System.Text.Json;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Utilities;
using SixLabors.Fonts;
using System.Runtime.CompilerServices;

namespace TelegramBot.Configs;

sealed public partial class Config
{
    public Config()
    {
        BotKeyApi = "6249847423:AAFSTQzCg6aVgvVKlXaDCst4OLWLCEeIQNk";
        CertificateFontPath = "Resources/arial_bolditalicmt.ttf";
        CertificateImagePath = "Resources/cert_image.jpeg";

        ColorHex = Color.White.ToHex();

        FontSize = 22;
        DebugMode = true;

        StartTextPoint = new PointF(200, 200);

        ReplyMarkups = new Dictionary<string, IReplyMarkup>(StringComparer.OrdinalIgnoreCase);
        ReplyMarkups["main"] = Utils.CreateReplyMarkup
                (
                    new[] { Utils.CreateButton("👤 О боте"), Utils.CreateButton("📄 Тесты") },
                    new[] { Utils.CreateButton("📖 Результаты"), Utils.CreateButton("👩‍🚒 Профессии") }
                );

        ReplyMarkups["aboutbot"] = Utils.CreateReplyMarkup
                (
                    new[] { Utils.CreateButton("🏠 Главная"), Utils.CreateButton("📄 Тесты") },
                    new[] { Utils.CreateButton("📖 Результаты"), Utils.CreateButton("👩‍🚒 Профессии") }
                );

        ReplyMarkups["results"] = Utils.CreateReplyMarkup
                (
                    new KeyboardButton[] { Utils.CreateButton("📕 Результаты Голланда"), Utils.CreateButton("📗 Результаты Климова") },
                    new KeyboardButton[] { Utils.CreateButton("📊 Общая статистика"), Utils.CreateButton("📋 Сертификат") },
                    new[] { Utils.CreateButton("🏠 В главное меню") }
                );

        ReplyMarkups["tests"] = Utils.CreateReplyMarkup
            (
                new[] { Utils.CreateButton("📝 Тест Голланда"), Utils.CreateButton("📝 Тест Климова") },
                new[] { Utils.CreateButton("🏠 В главное меню") }
            );

        ReplyMarkups["testhollandalreadypassed"] = Utils.CreateInlineKeyboard
            (
                new[] { Utils.CreateInlineButton("Да", "/runtestholland") },
                new[] { Utils.CreateInlineButton("Нет", "/main") }
            );

        ReplyMarkups["testklimovalreadypassed"] = Utils.CreateInlineKeyboard
            (
                new[] { Utils.CreateInlineButton("Да", "/runtestklimov") },
                new[] { Utils.CreateInlineButton("Нет", "/main") }
            );

        ReplyMarkups["testhollandnotpassed"] = Utils.CreateInlineKeyboard
            (
                new[] { Utils.CreateInlineButton("Да", "/runtestholland") },
                new[] { Utils.CreateInlineButton("Нет", "/main") }
            );

        ReplyMarkups["testklimovnotpassed"] = Utils.CreateInlineKeyboard
            (
                new[] { Utils.CreateInlineButton("Да", "/runtestholland") },
                new[] { Utils.CreateInlineButton("Нет", "/main") }
            );

        ReplyMarkups["userhasnottested"] = Utils.CreateInlineKeyboard
            (
                new[] { Utils.CreateInlineButton("Да", "/tests") },
                new[] { Utils.CreateInlineButton("Нет", "/main") }
            );

        TextMessages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        TextMessages["main"] = "Бот для прохождение тестов";
        TextMessages["aboutbot"] = "Тут будет инструкция";
        TextMessages["results"] = "Результаты";
        TextMessages["tests"] = "Выберите тест для прохождения";
        TextMessages["testpassed"] = "Тест пройден!\nПерейдите в главное меню для просмотра результатов";
        TextMessages["testalreadypassed"] = "Вы уже проходили данный тест!\nХотите перепройти?";
        TextMessages["testhollandnotpassed"] = "Вы не проходили тест Голланда!\nПерейти к выполнению?";
        TextMessages["testklimovnotpassed"] = "Вы не проходили тест Климова!\nПерейти к выполнению?";
        TextMessages["userhasnottested"] = "Вы ещё не проходили тестирование!\nПерейти в меню тестов?";
        TextMessages["hollandcertificate"] = "Выдается участнику {0} за прохождение теста Голланда\nТип личности - {1}";

        DatabasePath = "database.sqlite";
    }

    public static bool ConfigExists() => File.Exists(ConfigName);

    public static void CreateDefaultConfig()
    {
        Config config = new Config();

        using (var fs = new FileStream(ConfigName, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            JsonSerializer.Serialize(fs, config, JsonOptions);
        }
    }

    public static Config LoadConfig()
    {
        using (var fs = new FileStream(ConfigName, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            var config = JsonSerializer.Deserialize<Config>(fs, JsonOptions) ?? throw new JsonException("Не удалось десериализовать конфиг.");

            config.Color = Color.ParseHex(config.ColorHex);

            config.FontCollection = new FontCollection();
            FontFamily fm = config.FontCollection.Add(config.CertificateFontPath);
            config.Font = fm.CreateFont(config.FontSize, FontStyle.Regular);

            return config;
        }
    }
}
