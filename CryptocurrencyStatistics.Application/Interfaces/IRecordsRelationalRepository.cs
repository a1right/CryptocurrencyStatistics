using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Domain;

namespace CryptocurrencyStatistics.Application.Interfaces
{
    public interface IRecordsRelationalRepository

    {
        public Task<IEnumerable<Record>> GetAll(CancellationToken cancellationToken);
        public Task<Record> GetLastRecord(string pairName, CancellationToken cancellationToken);

        public Task<Record> GetRecordAtDate(string pairName, DateTime requestedTime, CancellationToken cancellationToken);

        public Task CreateRecord(Record record, CancellationToken cancellationToken);
    }
}
