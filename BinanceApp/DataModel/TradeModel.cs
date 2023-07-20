using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    public class TradeModel
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Time { get; set; }
    }
}
