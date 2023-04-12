namespace TelegramBot.Databases.Types;

public abstract class Result
{
#nullable disable

    abstract public int Id { get; set; }

    abstract public string Name { get; set; }

    abstract public string Description { get; set; }
}
