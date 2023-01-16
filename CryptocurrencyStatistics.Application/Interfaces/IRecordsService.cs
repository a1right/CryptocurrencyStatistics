using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Domain;

namespace CryptocurrencyStatistics.Application.Interfaces
{
    public interface IRecordsService
    {
        public Task<Record> GetLastRecord(int dateTimeOffset,
            CancellationToken cancellationToken = default(CancellationToken));

        public Task<Record> GeTRecordAtDate(DateTime requestedTime, int dateTimeOffset,
            CancellationToken cancellationToken = default(CancellationToken));

        public Task CreateRecord(Record record, CancellationToken cancellationToken = default(CancellationToken));
    }
}
