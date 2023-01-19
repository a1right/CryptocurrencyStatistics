
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.RelationalStorage.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CryptocurrencyStatistics.Relational
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRecordsDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RecordsDbContext>(options =>
                options.UseNpgsql(connectionString));
            return services;
        }
        public static IServiceCollection AddRecordsDbContextInMemory(this IServiceCollection services)
        {
            services.AddDbContext<RecordsDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb(Testing environment)"));
            return services;
        }

        public static IServiceCollection AddRecordsRelationalRepository(this IServiceCollection services)
        {
            services.AddScoped<IRecordsRelationalRepository, RecordsRelationalRepository>();
            return services;
        }
    }
}
