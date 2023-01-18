
using System.Collections.Generic;

namespace CryptocurrencyStatistics.RelationalStorage.Models
{
    public class Pair
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<StatisticData> StatisticDatas { get; set; }
    }
}
