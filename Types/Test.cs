using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Types;

public abstract class Test
{
    protected readonly List<Question> questions;

    public int CurrentIndex { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    /// <summary>
    /// Переходит к следующему вопросу
    /// Возвращает false, если вопросов нет
    /// </summary>
    public bool MoveNext { get =>  ++CurrentIndex < questions.Count - 1; }

    public bool HasQuestions { get => CurrentIndex < questions.Count - 1; }

    public Question CurrentQuestion { get => questions[CurrentIndex]; }

    public IList<Answer> CurrentAnswers { get => CurrentQuestion.Answers; }

    public virtual void SelectAnswer(int index)
    {
        CurrentQuestion.SelectAnswer(index);
    }

    public abstract int GetResult();

    public Test(string name, string description, int capacity = 10)
    {
        Name = name;
        Description = description;

        CurrentIndex = 0;

        questions = new List<Question>(capacity);
    }
}
