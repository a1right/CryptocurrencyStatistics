using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Domain;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyStatistics.Application.Interfaces
{
    public interface IRecordsDbContext
    {
        public DbSet<Record> Records { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
