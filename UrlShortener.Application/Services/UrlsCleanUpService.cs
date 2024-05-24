using UrlShortener.Application.Intefaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Persistence;

namespace UrlShortener.Application.Services
{
    public class UrlsCleanUpService : ICleanUpService<Url>
    {
        private readonly ApplicationDbContext _context;

        public UrlsCleanUpService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CleanUp()
        {
            var expiredUrls = _context.Urls.Where(u => u.ExpirationDate <= DateTime.UtcNow).ToList();

            if (expiredUrls.Any())
            {
                _context.Urls.RemoveRange(expiredUrls);
                await _context.SaveChangesAsync();
            }
        }
    }
}
