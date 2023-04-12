using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Types;

public class Answer
{
    private Action action;

    public string Name { get; private set; }

    public void Execute() => action();

    public Answer(Action action, string name)
    {
        this.action = action;
        Name = name;
    }
}
