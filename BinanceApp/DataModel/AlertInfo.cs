using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    public class AlertInfo
    {
        public DateTime LocalTime { get; set; }

        public DateTime CloseTime { get; set; }
        public decimal Price { get; set; }
        public decimal ST_SF { get; set; }
        public decimal LT_SF { get; set; }
        public decimal DML  { get; set; }
        public decimal DMH { get; set; }
        public decimal MH { get; set; }
        public decimal ML { get; set; }
        public SignalState Signal { get; set; }
    }
    
}
