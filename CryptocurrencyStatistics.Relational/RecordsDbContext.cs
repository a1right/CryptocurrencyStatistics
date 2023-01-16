using System.Threading.Tasks;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.Relational.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyStatistics.Relational
{
    public class RecordsDbContext : DbContext, IRecordsDbContext
    {
        public DbSet<Record> Records { get; set; }

        public RecordsDbContext(DbContextOptions<RecordsDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RecordConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
