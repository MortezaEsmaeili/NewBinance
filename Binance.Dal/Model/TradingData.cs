using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Dal.Model
{
    public class TradingData
    {
        public long Id { get; set; }
        public string CoinName { get; set; }
        public DateTime OpenDate { get; set; }
        public decimal Available { get; set; }
        public string Position { get; set; }
        public int Leverage { get; set; }
        public int Amount { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal PositionValue { get; set; }
        public decimal PositionMargin { get; set; }
        public decimal PercentageinTrade { get; set; }
        public DateTime CloseDate { get; set; }
        public int DaysinPosition { get; set; }
        public decimal GrossProfig_Loss { get; set; }
        public decimal GrossProfit_Loss_Percentage { get; set; }
        public decimal AvailableAfterPosition { get; set; }
        public decimal Net_Profit_Loss { get; set; }
        public decimal Net_Portfo_Profit { get; set; }
        public decimal Cost { get; set; }
    }
}
