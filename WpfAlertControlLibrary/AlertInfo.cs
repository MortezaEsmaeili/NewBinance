using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAlertControlLibrary
{
    public class AlertInfo
    {
        public DateTime CloseTime { get; set; }
        public DateTime LocalTime { get; set; }
        public decimal Price { get; set; }
        public decimal ST_SF { get; set; }
        public decimal LT_SF { get; set; }
        public decimal DML  { get; set; }
        public decimal DMH { get; set; }
        public decimal MH { get; set; }
        public decimal ML { get; set; }
        private AlertSignal aSignal { get; set; }
        public string Signal { get { return aSignal.ToString(); } }
    }
    public enum AlertSignal
    {
        Wait, 
        Sell,
        Buy
    }
}
