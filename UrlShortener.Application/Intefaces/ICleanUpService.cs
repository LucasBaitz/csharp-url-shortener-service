namespace UrlShortener.Application.Intefaces
{
    public interface ICleanUpService<T> where T : class
    {
        Task CleanUp();
    }
}
