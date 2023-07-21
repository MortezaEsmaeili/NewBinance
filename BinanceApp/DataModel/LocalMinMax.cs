using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    public class LocalMinMax
    {
        public DateTime Date { get; set; }
        public decimal Macd { get; set; }
        public decimal LeftDelta { get; set; }
        public decimal RightDelta { get; set; }
        public decimal MinDelta { get; set; }
        public bool Sign { get; set; }
        public int index { get; set; }
    }
    
}
