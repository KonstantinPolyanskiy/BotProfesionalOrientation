using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Types;

public class Question
{
    public string Name { get; private set; }

    public readonly List<Answer> Answers;

    public Question(string name)
    {
        Name = name;
        Answers = new List<Answer>();
    }

    public void SelectAnswer(int index) => Answers[index].Execute();
}
