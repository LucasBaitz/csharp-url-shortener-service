using UrlShortener.Application.DTOs;
using UrlShortener.Application.Intefaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Errors;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Infrastructure.Common.Interfaces;

namespace UrlShortener.Application.Services
{
    public class UrlService : IUrlService
    {
        private const int MinLength = 5;
        private const int MaxLength = 10;

        private readonly IUrlRepository _urlRepository;
        private readonly ISystemTime _clock;
        private readonly IRandomPathGenerator _randomPathGenerator;

        public UrlService(IUrlRepository urlRepository, ISystemTime clock, IRandomPathGenerator randomPathGenerator)
        {
            _urlRepository = urlRepository;
            _clock = clock;
            _randomPathGenerator = randomPathGenerator;
        }

        public async Task<IEnumerable<Url>> GetAllUrls()
        {
            return await _urlRepository.GetAllShortUrls();
        }

        public async Task<Url> CreateShortUrl(CreateShortUrlDTO urlDto)
        {
            string? shortenedUrl = urlDto.ShortenedUrl;
            if (!string.IsNullOrEmpty(shortenedUrl))
            {
                if (shortenedUrl.Length > MaxLength || shortenedUrl.Length < MinLength)
                {
                    throw new NotValidDataException("The provided path must be at least 5 and not more then 10 chars long.");
                }

                var pathAlreadyInUse = await _urlRepository.IsPathInUse(shortenedUrl);

                if (pathAlreadyInUse)
                {
                    throw new NotValidDataException("The provided path is not avalible");
                }
            }
            else
            {
                shortenedUrl = _randomPathGenerator.GenerateRandomPath(MinLength, MaxLength);
            }


            Url newUrl = new(shortenedUrl, urlDto.OriginalUrl, _clock.UtcNow.AddDays(7));
            newUrl.CreatedAt = _clock.UtcNow;

            Url createdUrl = await _urlRepository.AddShortUrl(newUrl);
            return createdUrl;

        }

        public async Task DeleteUrl(Guid urlId)
        {
            await _urlRepository.DeleteUrlById(urlId);
        }

        public async Task<string> GetUrlByShortenedUrl(string url)
        {
            Url urlEntity = await _urlRepository.GetUrlByShortenedUrl(url);

            return urlEntity.OriginalUrl;
        }

        public async Task<Url?> GetUrlById(Guid urlId)
        {
            Url? url = await _urlRepository.GetUrlById(urlId);

            if (url is null) 
            {
                throw new ResourceNotFoundException($"Url Entity with the provided ID {urlId} was not found.");
            }

            return url;
        }
    }
}
