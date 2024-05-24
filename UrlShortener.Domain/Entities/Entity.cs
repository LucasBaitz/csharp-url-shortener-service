namespace UrlShortener.Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; set; }
    }
}
