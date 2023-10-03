using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.BotState
{
    public class CandleDto
    {
        public decimal highPrice { get; set; }
        public decimal lowPrice { get; set; }
        public decimal openPrice { get; set; }
        public decimal closePrice { get; set; }

    }
}
