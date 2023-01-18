using System.Threading.Tasks;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.RelationalStorage.EntityConfigurations;
using CryptocurrencyStatistics.RelationalStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyStatistics.Relational
{
    public class RecordsDbContext : DbContext
    {
        public DbSet<Pair> Pairs { get; set; }
        public DbSet<StatisticData> StatisticDatas { get; set; }

        public RecordsDbContext(DbContextOptions<RecordsDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PairConfiguration());
            modelBuilder.ApplyConfiguration(new StatisticDataConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
