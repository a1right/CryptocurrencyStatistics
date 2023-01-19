using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace CryptocurrencyStatistics.DocumentOrientedStorage.Models
{
    public class RecordDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int ExternalId { get; set; }
        public string PairName { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
