using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.BotState
{
    public class TradeBox
    {
        public decimal upperBuyPrice { get; set; }
        public decimal lowerBuyPrice { get; set; }
        public decimal takeProfitBuyPrice { get; set; }
        public decimal stopLossBuyPrice { get; set; }

        public decimal upperSellPrice { get; set; }
        public decimal lowerSellPrice { get; set; }
        public decimal takeProfitSellPrice { get; set; }
        public decimal stopLossSellPrice { get; set; }
    }
}
