using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using TelegramBot.Configs;
using TelegramBot.Services.Interfaces;

namespace TelegramBot.Services
{
    public sealed class CertificateService : ICertificateService
    {
        private readonly Config _config;

        public CertificateService(Config config)
        {
            _config = config;
        }

        public async Task<Stream> CreateHollandCertificateAsync(string name, string type)
        {
            Stream stream = new MemoryStream();

            string text = string.Format(_config.TextMessages["hollandcertificate"], name, type);

            using (var image = new Image<Rgba32>(1000, 1000))
            {
                image.Mutate(i => i.DrawText(text, _config.Font, _config.Color, _config.StartTextPoint));

                await image.SaveAsJpegAsync(stream);
            }

            stream.Position = 0;

            return stream;
        }

        public Task<Stream> CreateKlimovCertificateAsync(string name, string type)
        {
            throw new NotImplementedException();
        }
    }
}
