using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.Relational;
using CryptocurrencyStatistics.RelationalStorage.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;

namespace CryptocurrencyStatistics.RelationalStorage.Repositories
{
    public class RecordsRepository : IRecordsRepository
    {
        private readonly RecordsDbContext _context;

        public RecordsRepository(RecordsDbContext context)
        {
            _context = context;
        }
        public async Task<Record> GetLastRecord(string pairName, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return null;
            var statisticData = await _context.StatisticDatas.Include(x => x.Pair)
                                                             .Where(x => x.Pair.Name == pairName)
                                                             .OrderBy(x => x.CreatedDateTime)
                                                             .LastOrDefaultAsync(cancellationToken);
            if (statisticData is null)
                return null;
            var record = new Record()
            {
                Id = statisticData.Id,
                PairName = statisticData.Pair.Name,
                CreatedDateTime = statisticData.CreatedDateTime,
                Value = statisticData.Value,
            };
            return record;
        }
        public async Task<Record> GetRecordAtDate(string pairName, DateTime requestedTime, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return null;
            var statisticData = await _context.StatisticDatas.Include(x => x.Pair)
                                                   .Where(x => x.Pair.Name == pairName && x.CreatedDateTime >= requestedTime)
                                                   .OrderBy(x => x.CreatedDateTime)
                                                   .FirstOrDefaultAsync(cancellationToken);
            if (statisticData is null) 
                return null;
            var record = new Record()
            {
                Id = statisticData.Id,
                PairName = statisticData.Pair.Name,
                CreatedDateTime = statisticData.CreatedDateTime,
                Value = statisticData.Value,
            };
            return record;
        }
        public async Task CreateRecord(Record record, CancellationToken cancellationToken)
        {
            var pair = await _context.Pairs.FirstOrDefaultAsync(x => x.Name == record.PairName, cancellationToken);
            if (pair is null)
            {
                pair = new Pair() { Name = record.PairName };
                _context.Pairs.Add(pair);
                await _context.SaveChangesAsync(cancellationToken);
            }
            var statisticData = new StatisticData()
            {
                PairId = pair.Id,
                CreatedDateTime = record.CreatedDateTime,
                Value = record.Value,
            };
            _context.Add(statisticData);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
