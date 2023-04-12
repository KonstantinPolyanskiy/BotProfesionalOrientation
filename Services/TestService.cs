using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Services.Interfaces;
using TelegramBot.Types;

namespace TelegramBot.Services;

public sealed class TestService : ITestService
{
    private readonly Dictionary<long, Test> runningTests;

    public TestService()
    {
        runningTests = new Dictionary<long, Test>();
    }

    public void DeleteRunningTest(long userId)
    {
        runningTests.Remove(userId);
    }

    public int GetCurrentIndex(long userId)
    {
        return runningTests[userId].CurrentIndex;
    }

    public string[] GetQuestionAnswers(long userId)
    {
        return runningTests[userId].CurrentAnswers.Select(a => a.Name).ToArray();
    }

    public string GetQuestionName(long userId)
    {
        return runningTests[userId].CurrentQuestion.Name;
    }

    public int GetResult(long userId)
    {
        Test test = runningTests[userId];

        int result = test.GetResult();

        DeleteRunningTest(userId);

        return result;
    }

    public string GetTestDescription(long userId)
    {
        return runningTests[userId].Description;
    }

    public string GetTestName(long userId)
    {
        return runningTests[userId].Name;
    }

    public bool HasQuestions(long userId)
    {
        return runningTests[userId].HasQuestions;
    }

    public bool IsHollandTestRunning(long userId)
    {
        return runningTests[userId] is TestHolland;
    }

    public bool IsKlimovTestRunning(long userId)
    {
        return runningTests[userId] is TestKlimov;
    }

    public void RunHollandTest(long userId)
    {
        runningTests[userId] = new TestHolland();
    }

    public void RunKlimovTest(long userId)
    {
        runningTests[userId] = new TestKlimov();
    }

    public void SelectAnswer(long userId, int index)
    {
        Test test = runningTests[userId];

        test.SelectAnswer(index);
        _ = test.MoveNext;
    }

    public bool UserPassesTest(long userId)
    {
        return runningTests.ContainsKey(userId);
    }
}
