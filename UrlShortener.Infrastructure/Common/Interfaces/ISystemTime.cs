namespace UrlShortener.Infrastructure.Common.Interfaces
{
    public interface ISystemTime
    {
        DateTime UtcNow { get; }
    }
}
