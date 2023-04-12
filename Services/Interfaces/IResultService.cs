using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Databases.Types;

namespace TelegramBot.Services.Interfaces
{
    public interface IResultService
    {
        public Task<Result[]> GetAllKlimovResultsAsync();

        public Task<Result[]> GetAllHollandResultsAsync();
    }
}
