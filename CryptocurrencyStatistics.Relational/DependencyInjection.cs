using System;
using System.Collections.Generic;
using System.Text;
using CryptocurrencyStatistics.Application.Interfaces;
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
            services.AddScoped<IRecordsDbContext, RecordsDbContext>();
            return services;
        }
        public static IServiceCollection AddRecordsDbContextInMemory(this IServiceCollection services)
        {
            services.AddDbContext<RecordsDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb(Testing environment)"));
            return services;
        }
    }
}
