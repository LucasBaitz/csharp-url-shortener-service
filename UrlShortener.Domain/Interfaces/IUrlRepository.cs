using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Interfaces
{
    public interface IUrlRepository
    {
        Task<Url> AddShortUrl(Url url);
        Task<IEnumerable<Url>> GetAllShortUrls();
        Task<Url> GetUrlByShortenedUrl(string path);
        Task<bool> IsPathInUse(string path);
        Task<Url?> GetUrlById(Guid id);
        Task DeleteUrlById(Guid id);
    }
}
