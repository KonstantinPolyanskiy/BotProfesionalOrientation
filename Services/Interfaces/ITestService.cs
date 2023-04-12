using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Services.Interfaces
{
    public interface ITestService
    {
        public void RunHollandTest(long userId);

        public void RunKlimovTest(long userId);

        public bool UserPassesTest(long userId);

        public void DeleteRunningTest(long userId);

        public string GetTestName(long userId);

        public string GetTestDescription(long userId);

        public string GetQuestionName(long userId);

        public string[] GetQuestionAnswers(long userId);

        public void SelectAnswer(long userId, int index);

        public bool HasQuestions(long userId);

        public int GetResult(long userId);

        public int GetCurrentIndex(long userId);

        public bool IsHollandTestRunning(long userId);

        public bool IsKlimovTestRunning(long userId);
    }
}
