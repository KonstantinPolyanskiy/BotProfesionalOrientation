using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Types.Builders;

public class AnswerBuilder
{
    private readonly Question question;

    public AnswerBuilder(Question question)
    {
        this.question = question;
    }

    public AnswerBuilder AddAnswer(string value, Action action)
    {
        question.Answers.Add
            (
                new Answer(action, value)
            );

        return this;
    }
}
