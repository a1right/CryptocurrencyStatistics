

using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.DocumentOrientedStorage.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CryptocurrencyStatistics.DocumentOrientedStorage
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, RecordsMongoDbSettings settings)
        {
            var client = new RecordsMongoDb(settings);
            services.AddSingleton(client);
            return services;
        }

        public static IServiceCollection AddRecordsDocumentOrientedRepository(this IServiceCollection services)
        {
            services.AddSingleton<IRecordsDocumentOrientedRepository, RecordsDocumentOrientedRepository>();
            return services;
        }
    }
}
