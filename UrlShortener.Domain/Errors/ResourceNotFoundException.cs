namespace UrlShortener.Domain.Errors
{
    public sealed class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }
    }
}
