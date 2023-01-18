
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.DocumentOrientedStorage.Utils;
using CryptocurrencyStatistics.Domain;

namespace CryptocurrencyStatistics.DocumentOrientedStorage.Repositories
{
    public class RecordsDocumentOrientedRepository : IRecordsDocumentOrientedRepository
    {
        private readonly RecordsMongoDb _mongoDb;

        public RecordsDocumentOrientedRepository(RecordsMongoDb mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public async Task<IEnumerable<Record>> GetAll(CancellationToken cancellationToken)
        {
            var documents = await _mongoDb.GetAll(cancellationToken);
            return documents.ToRecords();
        }

        public async Task<Record> GetById(int id, CancellationToken cancellationToken)
        {
            var document = await _mongoDb.GetByExternalId(id, cancellationToken);
            return document.ToRecord();
        }

        public async Task Create(Record record, CancellationToken cancellationToken)
        {
            await _mongoDb.Create(record.ToRecordDocument(), cancellationToken);
        }
        public async Task Create(IEnumerable<Record> records, CancellationToken cancellationToken)
        {
            await _mongoDb.Create(records.ToRecordDocuments(), cancellationToken);
        }
    }
}
