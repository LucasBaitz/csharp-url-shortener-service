using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Infrastructure.Common;
using UrlShortener.Infrastructure.Common.Interfaces;
using UrlShortener.Infrastructure.Persistence;
using UrlShortener.Infrastructure.Repositories;
using UrlShortener.Infrastructure.Services;

namespace UrlShortener.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("SqliteConnectionString")!;

            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseSqlite(connectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddTransient<ISystemTime, Clock>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHostedService<CleanupService>();

            return services;
        }
    }
}
