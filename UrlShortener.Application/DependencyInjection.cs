using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Intefaces;
using UrlShortener.Application.Services;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IRandomPathGenerator, RandomPathGenerator>();
            services.AddScoped<ICleanUpService<Url>, UrlsCleanUpService>();

            return services;
        }
    }
}
