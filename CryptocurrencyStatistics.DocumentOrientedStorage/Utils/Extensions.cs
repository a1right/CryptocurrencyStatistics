
using System.Collections.Generic;
using System.Linq;
using CryptocurrencyStatistics.DocumentOrientedStorage.Models;
using CryptocurrencyStatistics.Domain;

namespace CryptocurrencyStatistics.DocumentOrientedStorage.Utils
{
    internal static class Extensions
    {
        public static IEnumerable<Record> ToRecords(this IEnumerable<RecordDocument> documents)
        {
            var records = documents.Select(x => new Record()
            {
                Id = x.ExternalId,
                PairName = x.PairName,
                CreatedDateTime = x.CreatedDateTime,
                Value = x.Value,
            }).ToList();
            return records;
        }
        public static Record ToRecord(this RecordDocument document)
        {
            var record = new Record()
            {
                Id = document.ExternalId,
                PairName = document.PairName,
                CreatedDateTime = document.CreatedDateTime,
                Value = document.Value,
            };
            return record;
        }
        public static IEnumerable<RecordDocument> ToRecordDocuments(this IEnumerable<Record> records)
        {
            var documents = records.Select(x => new RecordDocument()
            {
                ExternalId = x.Id,
                PairName = x.PairName,
                CreatedDateTime = x.CreatedDateTime,
                Value = x.Value,
            }).ToList();
            return documents;
        }
        public static RecordDocument ToRecordDocument(this Record record)
        {
            var document = new RecordDocument()
            {
                ExternalId = record.Id,
                PairName = record.PairName,
                CreatedDateTime = record.CreatedDateTime,
                Value = record.Value,
            };
            return document;
        }
    }
}
