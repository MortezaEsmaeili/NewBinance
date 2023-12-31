﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TradeBotRange
    {
        public long Id { get; set; }
        public string CoinName { get; set; }
        public decimal UpSellPrice { get; set; }
        public decimal LowSellPrice { get; set; }
        public decimal SellStopLossPrice { get; set; }
        public decimal SellTakeProfitPrice { get; set; }
        public decimal UpBuyPrice { get; set; }
        public decimal LowBuyPrice { get; set; }
        public decimal BuyStopLossPrice { get; set; }
        public decimal BuyTakeProfitPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
