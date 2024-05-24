namespace UrlShortener.Domain.Errors
{
    public sealed class NotValidDataException : Exception
    {
        public NotValidDataException(string message) : base(message) { }
    }
}
