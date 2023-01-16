namespace CryptocurrencyStatistics.WebApi.Models
{

    public class EthUsdResponseTemplate
    {
        public Eth_Usd eth_usd { get; set; }
    }

    public class Eth_Usd
    {
        public float high { get; set; }
        public float low { get; set; }
        public float avg { get; set; }
        public float vol { get; set; }
        public float vol_cur { get; set; }
        public int last { get; set; }
        public float buy { get; set; }
        public float sell { get; set; }
        public int updated { get; set; }
    }

}
