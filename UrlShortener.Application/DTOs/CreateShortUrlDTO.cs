namespace UrlShortener.Application.DTOs
{
    public record CreateShortUrlDTO(string OriginalUrl, string? ShortenedUrl) { }
}
