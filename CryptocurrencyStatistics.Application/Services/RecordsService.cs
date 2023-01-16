using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyStatistics.Application.Services
{
    public class RecordsService : IRecordsService
    {
        private readonly IRecordsDbContext _context;

        public RecordsService(IRecordsDbContext context)
        {
            _context = context;
        }

        public async Task<Record> GetLastRecord(int dateTimeOffset,CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
                return null;
            var record = await _context.Records.OrderBy(x => x.CreatedDateTime)
                                               .LastOrDefaultAsync(cancellationToken);
            record.CreatedDateTime += TimeSpan.FromHours(dateTimeOffset);
            return record;
        }
        public async Task<Record> GeTRecordAtDate(DateTime requestedTime, int dateTimeOffset,CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
                return null;
            var record = await _context.Records.Where(x => x.CreatedDateTime >= requestedTime.ToUniversalTime())
                                               .OrderBy(x => x.CreatedDateTime)
                                               .FirstOrDefaultAsync(cancellationToken);
            record.CreatedDateTime += TimeSpan.FromHours(dateTimeOffset);
            return record;
        }

        public async Task CreateRecord(Record record, CancellationToken cancellationToken = default(CancellationToken))
        {
            _context.Records.Add(record);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
