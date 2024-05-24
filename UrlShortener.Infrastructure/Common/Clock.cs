using UrlShortener.Infrastructure.Common.Interfaces;

namespace UrlShortener.Infrastructure.Common
{
    public sealed class Clock : ISystemTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
