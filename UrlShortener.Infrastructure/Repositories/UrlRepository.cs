using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Errors;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Infrastructure.Common.Interfaces;
using UrlShortener.Infrastructure.Persistence;

namespace UrlShortener.Infrastructure.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly DbSet<Url> _urls;
        private readonly IUnitOfWork _unitOfWork;

        public UrlRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _urls = context.Urls;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Url>> GetAllShortUrls()
        {
            return await _urls.ToListAsync();
        }

        public async Task<Url> AddShortUrl(Url url)
        {
            var createdUrl = await _urls.AddAsync(url);

            await _unitOfWork.SaveChanges();
            return createdUrl.Entity;
        }

        public async Task DeleteUrlById(Guid entityId)
        {
            Url? urlEntitiy = await _urls.FirstOrDefaultAsync(u => u.Id == entityId);

            if (urlEntitiy is null)
            {
                throw new ResourceNotFoundException($"Unable to DELETE URL with ID '{entityId}'.");
            }

            _urls.Remove(urlEntitiy);
            await _unitOfWork.SaveChanges();
        }

        public async Task<Url?> GetUrlById(Guid id)
        {
            return await _urls.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Url> GetUrlByShortenedUrl(string shortenedUrl)
        {

            Url? urlEntity = await _urls.FirstOrDefaultAsync(u => u.ShortenedUrl == shortenedUrl);

            if (urlEntity is null)
            {
                throw new ResourceNotFoundException($"Unable to GET URL with Uri '{shortenedUrl}'.");
            }

            return urlEntity;
        }

        public async Task<bool> IsPathInUse(string shortenedUrl)
        {
            return await _urls.AnyAsync(u => u.ShortenedUrl.Equals(shortenedUrl));
        }
    }
}
