
namespace TelegramBot.Databases.Types;

public sealed class User
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? KlimovCertificateFileId { get; set; }
    public KlimovResult? KlimovResult { get; set; }

    public string? HollandCertificateFileId { get; set; }
    public HollandResult? HollandResult { get; set; }
}
