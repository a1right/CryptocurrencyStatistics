namespace CryptocurrencyStatistics.DocumentOrientedStorage
{
    public class RecordsMongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string RecordsCollectionName { get; set; } = null!;
    }
}
