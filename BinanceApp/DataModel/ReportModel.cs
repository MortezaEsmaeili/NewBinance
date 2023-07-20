using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    public class ReportModel
    {
        public DateTime Time { get; set; }
        public string UserName { get; set; }
        public string CoinName { get; set; }
        public string TP { get; set; }
        public string SP { get; set; }
        public string Price { get; set; }
        public string Position { get; set; }
        public string Profit { get; set; }
    }
}
