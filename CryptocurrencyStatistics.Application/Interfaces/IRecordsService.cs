using System;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Domain;

namespace CryptocurrencyStatistics.Application.Interfaces
{
    public interface IRecordsService
    {
        public Task<Record> GetLastRecord(string pairName, CancellationToken cancellationToken = default(CancellationToken));

        public Task<Record> GeTRecordAtDate(string pairName, DateTime requestedTime, CancellationToken cancellationToken = default(CancellationToken));

        public Task CreateRecord(Record record, CancellationToken cancellationToken = default(CancellationToken));
    }
}
