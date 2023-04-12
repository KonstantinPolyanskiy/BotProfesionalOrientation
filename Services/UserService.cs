using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using TelegramBot.Databases;
using TelegramBot.Databases.Types;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Services;

public sealed class UserService : IUserService
{
    private readonly DatabaseFactory _factory;

    public UserService(DatabaseFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> CreateUserAsync(long userId)
    {
        using (var c = _factory.CreateContext())
        {
            await c.Users.AddAsync( new User() { Id = userId } );

            return await c.SaveChangesAsync();
        }
    }

    public async Task<User[]> GetAllUsersPassedHollandTestAsync()
    {
        using (var c = _factory.CreateContext())
        {
            return await c.Users
                .Include(u => u.HollandResult)
                .Include(u => u.KlimovResult)
                .Where(u => u.HollandResult != null)
                .ToArrayAsync();
        }
    }

    public async Task<User[]> GetAllUsersPassedKlimovTestAsync()
    {
        using (var c = _factory.CreateContext())
        {
            return await c.Users
                .Include(u => u.HollandResult)
                .Include(u => u.KlimovResult)
                .Where(u => u.KlimovResult != null)
                .ToArrayAsync();
        }
    }

    public async Task<User> GetUserAsync(long userId)
    {
        using (var c = _factory.CreateContext())
        {
            return await c.Users
                .Include(u => u.HollandResult)
                .Include(u => u.KlimovResult)
                .SingleAsync(u => u.Id == userId);
        }
    }

    public async Task<int> UpdateUserAsync
    (
        long userId, 
        string? name = null, 
        string? klimovCertificateFileId = null, 
        string? hollandCertificateFileId = null, 
        int? klimovResultId = null, 
        int? hollandResultId = null
    )
    {
        using (var c = _factory.CreateContext())
        {
            User user = await c.Users.SingleAsync (u => u.Id == userId);

            if (name is not null) user.Name = name;
            if (klimovCertificateFileId is not null) user.KlimovCertificateFileId = klimovCertificateFileId;
            if (hollandCertificateFileId is not null) user.HollandCertificateFileId = hollandCertificateFileId;
            if (klimovResultId is not null) user.KlimovResult = await c.KlimovResults.SingleAsync(k => k.Id == klimovResultId);
            if (hollandResultId is not null) user.HollandResult = await c.HollandResults.SingleAsync(h => h.Id == hollandResultId);

            return await c.SaveChangesAsync();
        }
    }

    public async Task<bool> UserExistsAsync(long userId)
    {
        using (var c = _factory.CreateContext())
        {
            return await c.Users.AnyAsync(u => u.Id == userId);
        }
    }
}
