using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.EntitiesConfiguration
{
    internal sealed class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.OriginalUrl)
                .IsRequired();

            builder.Property(u => u.ShortenedUrl)
                .IsRequired();
        }
    }
}
