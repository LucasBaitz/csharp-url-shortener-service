using System.Runtime.InteropServices.Marshalling;
using System.Text.RegularExpressions;

namespace UrlShortener.Domain.Entities
{
    public sealed class Url : Entity
    {
        public string ShortenedUrl { get; private set; }
        public string OriginalUrl { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        public Url(string shortenedUrl, string originalUrl, DateTime expirationDate)
        {
            if (!IsValidUrl(originalUrl)) 
            {
                throw new ArgumentException("The provided URL is not valid.");
            }

            ShortenedUrl = shortenedUrl;
            OriginalUrl = originalUrl;
            ExpirationDate = expirationDate;
        }

        private bool IsValidUrl(string url)
        {
            var regex = new Regex(@"^https?:\/\/[\w\-\.]+(\.[\w\-]+)+[/#?]?.*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
            return regex.IsMatch(url);
        }
    }
}
