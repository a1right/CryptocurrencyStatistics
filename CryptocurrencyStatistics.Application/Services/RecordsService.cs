using System;
using System.Linq;
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

        public async Task<Record> GetLastRecord(string pairName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
                return null;
            var record = await _context.Records.Where(x => x.PairName == pairName)
                                               .OrderBy(x => x.CreatedDateTime)
                                               .LastOrDefaultAsync(cancellationToken);
            return record;
        }
        public async Task<Record> GetRecordAtDate(string pairName ,DateTime requestedTime,CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
                return null;
            var record = await _context.Records.Where(x => x.CreatedDateTime >= requestedTime.ToUniversalTime() && x.PairName == pairName)
                                               .OrderBy(x => x.CreatedDateTime)
                                               .FirstOrDefaultAsync(cancellationToken);
            return record;
        }

        public async Task CreateRecord(Record record, CancellationToken cancellationToken = default(CancellationToken))
        {
            _context.Records.Add(record);
            await _context.SaveChangesAsync(cancellationToken);
            Console.WriteLine($"--> New record recieved: Id: {record.Id}, PairName: {record.PairName}, Created datetime: {record.CreatedDateTime}, Value: {record.Value}");
        }
    }
}
