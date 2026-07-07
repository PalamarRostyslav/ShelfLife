using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShelfLife.Catalog.Application.Common.Interfaces;
using ShelfLife.Catalog.Infrastructure.Persistence;
using ShelfLife.Catalog.Infrastructure.Persistence.Interceptors;
using ShelfLife.Catalog.Infrastructure.Persistence.Repositories;

namespace ShelfLife.Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CatalogDb")
                ?? throw new InvalidOperationException("Connection string 'CatalogDb' was not found.");

            services.AddScoped<DomainEventsToOutboxInterceptor>();

            services.AddDbContext<CatalogDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString);
                options.AddInterceptors(sp.GetRequiredService<DomainEventsToOutboxInterceptor>());
            });

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CatalogDbContext>());
            services.AddScoped<ICatalogDbContext>(sp => sp.GetRequiredService<CatalogDbContext>());

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IShelfRepository, ShelfRepository>();

            return services;
        }
    }
}
