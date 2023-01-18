
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.DocumentOrientedStorage.Models;
using MongoDB.Driver;

namespace CryptocurrencyStatistics.DocumentOrientedStorage
{
    public class RecordsMongoDb
    {
        private readonly IMongoCollection<RecordDocument> _records;
        public RecordsMongoDb(RecordsMongoDbSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);
            _records = mongoDatabase.GetCollection<RecordDocument>(settings.RecordsCollectionName);
        }

        public async Task<IEnumerable<RecordDocument>> GetAll(CancellationToken cancellationToken)
        {
            return await _records.Find(_ => true).ToListAsync(cancellationToken);
        }

        public async Task<RecordDocument> GetById(string id, CancellationToken cancellationToken)
        {
            return await _records.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<RecordDocument> GetByExternalId(int id, CancellationToken cancellationToken)
        {
            return await _records.Find(x => x.ExternalId == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task Create(RecordDocument record, CancellationToken cancellationToken)
        {
            await _records.InsertOneAsync(record, cancellationToken: cancellationToken);
        }
        public async Task Create(IEnumerable<RecordDocument> records, CancellationToken cancellationToken)
        {
            await _records.InsertManyAsync(records, cancellationToken: cancellationToken);
        }
    }
}
