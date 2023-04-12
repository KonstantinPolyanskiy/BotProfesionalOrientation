using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Types.Builders;

public class QuestionsBuilder
{
    private readonly IList<Question> questions;

    private Question? currentQuestion;

    public QuestionsBuilder(IList<Question> questions)
    {
        this.questions = questions;

        currentQuestion = null;
    }

    public AnswerBuilder CreateQuestion(string value)
    {
        currentQuestion = new Question(value);

        questions.Add(currentQuestion);

        return new AnswerBuilder(currentQuestion);
    }
}
