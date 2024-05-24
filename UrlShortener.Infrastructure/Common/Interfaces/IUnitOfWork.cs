namespace UrlShortener.Infrastructure.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChanges();
    }
}
