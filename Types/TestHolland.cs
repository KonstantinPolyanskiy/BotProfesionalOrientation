using TelegramBot.Types.Builders;
using TelegramBot.Services;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Types;

internal enum TypeHolland
{
    Realistic,
    Intelligent,
    Social,
    Conventional,
    Entrepreneurial,
    Artistic,
}

public class TestHolland : Test
{
    private static readonly int _resultsCount = 6;

    private readonly Random _random;

    private readonly int[] scores = new int[_resultsCount];

    private readonly string[] questionNames = new string[]
    {
        "Какая профессия Вам больше нравится?",
        "Чтобы вы предпочли?"
    };

    private string randomQuestionName
    {
        get => questionNames[_random.Next(questionNames.Length)];
    }

    public TestHolland(Random? random = null) : base("Голланда", "Тест на определение профессиональной ориентации", 42)
    {
        _random = random ?? new Random();

        var questionsBuilder = new QuestionsBuilder(questions);

        // 1
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Инженер-технолог", () => ++scores[(int) TypeHolland.Realistic])
            .AddAnswer("Инженер-конструктор", () => ++scores[(int)TypeHolland.Intelligent]);

        // 2
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Электрорадиотехник", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Врач-терапевт", () => ++scores[(int)TypeHolland.Social]);

        // 3
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Оператор станков с числовым программным управлением", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Кодировщик (обработка информации)", () => ++scores[(int)TypeHolland.Conventional]);

        // 4
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Фотограф", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Коммерсант", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 5
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Спасатель МЧС", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Дизайнер", () => ++scores[(int)TypeHolland.Artistic]);

        // 6
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Политолог", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Психиатр", () => ++scores[(int)TypeHolland.Social]);

        // 7
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Ученый химик", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Бухгалтер", () => ++scores[(int)TypeHolland.Conventional]);

        // 8
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Философ", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Частный предприниматель", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 9
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Лингвист", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Модельер", () => ++scores[(int)TypeHolland.Artistic]);

        // 10
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Инспектор службы занятости населения", () => ++scores[(int)TypeHolland.Social])
            .AddAnswer("Статист", () => ++scores[(int)TypeHolland.Conventional]);

        // 11
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Социальный педагог", () => ++scores[(int)TypeHolland.Social])
            .AddAnswer("Биржевой маклер", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 12
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Тренер", () => ++scores[(int)TypeHolland.Social])
            .AddAnswer("Искусствовед", () => ++scores[(int)TypeHolland.Artistic]);

        // 13
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Нотариус", () => ++scores[(int)TypeHolland.Conventional])
            .AddAnswer("Менеджер", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 14
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Перфораторщик", () => ++scores[(int)TypeHolland.Conventional])
            .AddAnswer("Художник", () => ++scores[(int)TypeHolland.Artistic]);

        // 15
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Лидер политической партии, общего движения", () => ++scores[(int)TypeHolland.Artistic])
            .AddAnswer("Писатель", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 16
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Закройщик", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Метеоролог", () => ++scores[(int)TypeHolland.Intelligent]);

        // 17
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Водитель", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Работник пресс-службы", () => ++scores[(int)TypeHolland.Social]);

        // 18
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Чертежник", () => ++scores[(int)TypeHolland.Conventional])
            .AddAnswer("Риэлтер", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 19
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Специалист по ремонту компьютеров и оргтехники", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Секретарь-референт", () => ++scores[(int)TypeHolland.Conventional]);

        // 20
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Микробиолог", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Психолог", () => ++scores[(int)TypeHolland.Social]);

        // 21
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Видеооператор", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Режиссер", () => ++scores[(int)TypeHolland.Artistic]);

        // 22
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Экономист", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Провизор", () => ++scores[(int)TypeHolland.Conventional]);

        // 23
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Зоолог", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Главный инженер", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 24
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Программист", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Архитектор", () => ++scores[(int)TypeHolland.Artistic]);

        // 25
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Работник инспекции по делам несовершеннолетних", () => ++scores[(int)TypeHolland.Social])
            .AddAnswer("Коммивояжер", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 26
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Преподаватель", () => ++scores[(int)TypeHolland.Social])
            .AddAnswer("Биржевой маклер", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 27
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Воспитатель", () => ++scores[(int)TypeHolland.Social])
            .AddAnswer("Декоратор", () => ++scores[(int)TypeHolland.Artistic]);

        // 28
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Реставратор", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Зав. отделом предприятия", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 29
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Корректор", () => ++scores[(int)TypeHolland.Conventional])
            .AddAnswer("Литератор и кинокритик", () => ++scores[5]);

        // 30
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Фермер", () => ++scores[(int)TypeHolland.Entrepreneurial])
            .AddAnswer("Визажист", () => ++scores[(int)TypeHolland.Artistic]);

        // 31
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Парикмахер", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Социолог", () => ++scores[(int)TypeHolland.Intelligent]);

        // 32
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Экспедитор", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Редактор", () => ++scores[(int)TypeHolland.Conventional]);

        // 33
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Ветеринар", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Финансовый директор", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 34
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Автомеханик", () => ++scores[(int)TypeHolland.Realistic])
            .AddAnswer("Стилист", () => ++scores[(int)TypeHolland.Artistic]);

        // 35
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Археолог", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Эксперт", () => ++scores[(int)TypeHolland.Conventional]);

        // 36
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Библиограф", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Корреспондент", () => ++scores[(int)TypeHolland.Social]);

        // 37
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Эколог", () => ++scores[(int)TypeHolland.Intelligent])
            .AddAnswer("Актер", () => ++scores[(int)TypeHolland.Artistic]);

        // 38
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Логопед", () => ++scores[(int)TypeHolland.Social])
            .AddAnswer("Контролер", () => ++scores[(int)TypeHolland.Conventional]);

        // 39
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Адвокат", () => ++scores[(int)TypeHolland.Social])
            .AddAnswer("Директор акционерского общества", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 40
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Кассир", () => ++scores[(int)TypeHolland.Conventional])
            .AddAnswer("Продюсер", () => ++scores[(int)TypeHolland.Entrepreneurial]);

        // 41
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Поэт", () => ++scores[(int)TypeHolland.Entrepreneurial])
            .AddAnswer("Продавец", () => ++scores[(int)TypeHolland.Artistic]);

        // 42
        questionsBuilder.CreateQuestion(randomQuestionName)
            .AddAnswer("Криминалист-баллистик", () => ++scores[(int)TypeHolland.Entrepreneurial])
            .AddAnswer("Композитор", () => ++scores[(int)TypeHolland.Artistic]);
    }

    public override int GetResult()
    {
        return Array.IndexOf(scores, scores.Max() ) + 1;
    }
}
