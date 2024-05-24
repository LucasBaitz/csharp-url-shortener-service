using UrlShortener.Application.DTOs;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Intefaces
{
    public interface IUrlService
    {
        Task<IEnumerable<Url>> GetAllUrls();
        Task<string> GetUrlByShortenedUrl(string url);
        Task<Url?> GetUrlById(Guid urlId);
        Task DeleteUrl(Guid urlId);
        Task<Url> CreateShortUrl(CreateShortUrlDTO urlDto);

    }
}
