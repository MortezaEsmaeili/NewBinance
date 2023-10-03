using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.BotState
{
    public class TradeBoundry
    {
        public decimal[] BuyPrice = new decimal[5];
        public bool[] BuyPermition = new bool[5];

        public decimal[] SellPrice = new decimal[5];
        public bool[] SellPermition = new bool[5];

        public TradeBoundry(CandleDto candle)
        {
            decimal upperPrice = 0, lowerPrice = 0;
            if (candle.openPrice > candle.closePrice)
            { 
                upperPrice = candle.openPrice;
                lowerPrice = candle.closePrice;
            }
            else
            {
                upperPrice = candle.closePrice;
                lowerPrice = candle.openPrice;
            }

            decimal deltaBuy = candle.highPrice - upperPrice;
            decimal deltaSell= candle.lowPrice - lowerPrice;

            for(int i=0; i<5; i++)
            {
                BuyPermition[i] = true;
                SellPermition[i] = true;
                BuyPrice[i] = upperPrice + i * deltaBuy;
                SellPrice[i] = lowerPrice - i * deltaSell;
            }
        }

        internal TradeCommand CheckState(decimal price)
        {
            throw new NotImplementedException();
        }
    }
}
