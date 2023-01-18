using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.Relational;
using CryptocurrencyStatistics.RelationalStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyStatistics.RelationalStorage.Repositories
{
    public class RecordsRelationalRepository : IRecordsRelationalRepository
    {
        private readonly RecordsDbContext _context;

        public RecordsRelationalRepository(RecordsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Record>> GetAll(CancellationToken cancellationToken)
        {
            if(cancellationToken.IsCancellationRequested) 
                return null;
            var statisticDatas = await _context.StatisticDatas.Include(x => x.Pair).ToListAsync(cancellationToken);
            var records = statisticDatas.Select(x => new Record()
            {
                Id = x.Id,
                PairName = x.Pair.Name,
                CreatedDateTime = x.CreatedDateTime,
                Value = x.Value,
            }).ToList();
            return records;
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
