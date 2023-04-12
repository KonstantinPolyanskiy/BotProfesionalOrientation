using Microsoft.EntityFrameworkCore;

namespace TelegramBot.Databases;

public sealed class DatabaseFactory
{
    private readonly DbContextOptionsBuilder _optionsBuilder;

    public DatabaseFactory(Action<DbContextOptionsBuilder> setup)
    {
        _optionsBuilder = new DbContextOptionsBuilder();

        setup(_optionsBuilder);
    }

    public DatabaseContext CreateContext() => new DatabaseContext(_optionsBuilder.Options);
}
