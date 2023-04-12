using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Services.Interfaces
{
    public interface ICertificateService
    {
        public Task<Stream> CreateHollandCertificateAsync(string name, string type);
        public Task<Stream> CreateKlimovCertificateAsync(string name, string type);
    }
}
