using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Databases;
using TelegramBot.Databases.Types;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Services
{
    public sealed class ResultService : IResultService
    {
        private readonly DatabaseFactory _factory;

        public ResultService(DatabaseFactory factory)
        {
            _factory = factory;
        }

        public async Task<Result[]> GetAllHollandResultsAsync()
        {
            using (var c = _factory.CreateContext())
            {
                return await c.HollandResults.ToArrayAsync();
            }
        }

        public async Task<Result[]> GetAllKlimovResultsAsync()
        {
            using (var c = _factory.CreateContext())
            {
                return await c.KlimovResults.ToArrayAsync();
            }
        }
    }
}
