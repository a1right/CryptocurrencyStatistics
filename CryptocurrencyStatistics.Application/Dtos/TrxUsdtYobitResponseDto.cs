
namespace CryptocurrencyStatistics.Application.Dtos
{

    public class TrxUsdtYobitResponseDto
    {
        public Trx_Usdt trx_usdt { get; set; }
    }

    public class Trx_Usdt
    {
        public float high { get; set; }
        public float low { get; set; }
        public float avg { get; set; }
        public float vol { get; set; }
        public float vol_cur { get; set; }
        public float last { get; set; }
        public float buy { get; set; }
        public float sell { get; set; }
        public int updated { get; set; }
    }

}
