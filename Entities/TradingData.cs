using System.Data;

namespace Entities
{
    public class TradingData
    {
        
        public long Id { get; set; }
        public string CoinName { get; set; }
        public DateTime MyProperty { get; set; }
        public string Position { get; set; }
        public int Leverage { get; set; }
        public int Amount { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal PositionValue { get; set; }
        public decimal PositionMargin { get; set; }

    }
}