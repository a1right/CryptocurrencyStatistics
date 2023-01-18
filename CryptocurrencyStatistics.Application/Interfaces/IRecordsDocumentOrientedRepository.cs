using CryptocurrencyStatistics.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CryptocurrencyStatistics.Application.Interfaces
{
    public interface IRecordsDocumentOrientedRepository
    {
        public Task<IEnumerable<Record>> GetAll(CancellationToken cancellationToken);

        public Task<Record> GetById(int id, CancellationToken cancellationToken);

        public Task Create(Record record, CancellationToken cancellationToken);
        public Task Create(IEnumerable<Record> records, CancellationToken cancellationToken);
    }
}
