using SixLabors.Fonts;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Configs;

sealed public partial class Config
{
    [JsonIgnore]
    public static JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
    {
        WriteIndented = true
    };

    [JsonIgnore]
    public Dictionary<string, IReplyMarkup> ReplyMarkups { get; set; }

    [JsonIgnore]
    public FontCollection FontCollection { get; set; }

    [JsonIgnore]
    public Font Font { get; set; }

    [JsonIgnore]
    public static string ConfigName = "Config.json";

    [JsonInclude]
    public string ColorHex { get; set; }

    [JsonInclude]
    public string BotKeyApi { get; set; }

    [JsonInclude]
    public Dictionary<string, string> TextMessages { get; set; }

    [JsonInclude]
    public string DatabasePath { get; set; }

    [JsonInclude]
    public string CertificateFontPath { get; set; }

    [JsonInclude]
    public string CertificateImagePath { get; set; }

    [JsonInclude]
    public PointF StartTextPoint { get; set; }

    [JsonIgnore]
    public Color Color { get; set; }

    [JsonInclude]
    public bool DebugMode { get; set; }

    [JsonInclude]
    public int FontSize { get; set; }
}
