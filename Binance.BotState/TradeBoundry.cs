using System;
using System.CodeDom.Compiler;
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
                SellPrice[i] = upperPrice + i * deltaBuy;
                BuyPrice[i] = lowerPrice - i * deltaSell;
            }
        }

        public TradeCommand CheckState(decimal price, string position)
        {
            TradeCommand tradeCommand = null;
            if (price <= BuyPrice[0] && position == "Buy")
            {
                for(int i=0; i<=5; i++)
                {
                    if (BuyPermition[i] && BuyPrice[i] >= price)
                    {
                        tradeCommand = new TradeCommand();
                        tradeCommand.command = CommandType.Buy;
                        tradeCommand.nextState = TradeState.OpenBuyPosition;
                        BuyPermition[i] = false;
                        break;
                    }
                }
                bool temp = false;
                for (int i = 0; i < 5; i++)
                    temp = temp || BuyPermition[i];
                if(!temp)
                {
                    tradeCommand.nextState = TradeState.WaitForTakeProfitOrStopLoss;
                }
            }
            if(price >= SellPrice[0] && position == "Sell")
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (SellPermition[i] && SellPrice[i] <= price)
                    {
                        tradeCommand = new TradeCommand();
                        tradeCommand.command = CommandType.Sell;
                        tradeCommand.nextState = TradeState.OpenSellPosition;
                        SellPermition[i] = false;
                        break;
                    }
                }
                bool temp = false;
                for (int i = 0; i < 5; i++)
                    temp = temp || SellPermition[i];
                if (!temp)
                {
                    tradeCommand.nextState = TradeState.WaitForTakeProfitOrStopLoss;
                }
            }
            return tradeCommand;
        }
    }
}
