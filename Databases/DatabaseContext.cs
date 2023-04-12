using Microsoft.EntityFrameworkCore;

namespace TelegramBot.Databases;
using TelegramBot.Databases.Types;

public sealed class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<HollandResult> HollandResults { get; set; }
    public DbSet<KlimovResult> KlimovResults { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HollandResult>()
            .HasData
            (
                new HollandResult() { Id = 1, Name = "Реалистичный", Description = "Описание типа" },
                new HollandResult() { Id = 2, Name = "Интеллектуальный", Description = "Описание типа" },
                new HollandResult() { Id = 3, Name = "Социальный", Description = "Описание типа" },
                new HollandResult() { Id = 4, Name = "Конвенциальный", Description = "Описание типа" },
                new HollandResult() { Id = 5, Name = "Предприимчивый", Description = "Описание типа" },
                new HollandResult() { Id = 6, Name = "Артистический", Description = "Описание типа" } 
           );

        modelBuilder.Entity<KlimovResult>()
            .HasData
            (
                new KlimovResult() { Id = 1, Name = "Человек-природа", Description = "Описание типа" },
                new KlimovResult() { Id = 2, Name = "Человек-техника", Description = "Описание типа" },
                new KlimovResult() { Id = 3, Name = "Человек-знаковая система", Description = "Описание типа" },
                new KlimovResult() { Id = 4, Name = "Человек-искусство", Description = "Описание типа" },
                new KlimovResult() { Id = 5, Name = "Человек-человек", Description = "Описание типа" }
           );
    }
}
