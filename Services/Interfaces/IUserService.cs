using TelegramBot.Databases.Types;

namespace TelegramBot.Services.Interfaces;

public interface IUserService
{
    public Task<User> GetUserAsync(long userId);

    public Task<bool> UserExistsAsync(long userId);

    public Task<int> CreateUserAsync(long userId);

    public Task<User[]> GetAllUsersPassedHollandTestAsync();

    public Task<User[]> GetAllUsersPassedKlimovTestAsync();

    public Task<int> UpdateUserAsync
    (
        long userId, 
        string? name = null, 
        string? klimovCertificateFileId = null,
        string? hollandCertificateFileId = null,
        int? klimovResultId = null,
        int? hollandResultId = null
    );
}
