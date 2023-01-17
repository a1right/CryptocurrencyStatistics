using System;
using CryptocurrencyStatistics.Relational;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyStatistics.RelationalStorage
{
    public static class DbInitializer
    {
        public static void Initialize(RecordsDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                ApplyMigrations(context);
            }
            SeedData(context);
        }

        private static void ApplyMigrations(RecordsDbContext context)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Could not run migrations: {exception.Message}");
            }
        }

        private static void SeedData(RecordsDbContext context)
        {
            
        }
    }
}
