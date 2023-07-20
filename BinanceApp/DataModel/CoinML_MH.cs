using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    public class CoinDML
    {
        public string CoinName { get; set; }
        public decimal DML_1Min { get; set; }
        public decimal DML_5Min { get; set; }
        public decimal DML_15Min { get; set; }
        public decimal DML_30Min { get; set; }
        public decimal DML_60Min { get; set; }
        public decimal DML_4Hour { get; set; }
        public decimal DML_1Day { get; set; }
    }
    public class CoinDMH

    {
        public string CoinName { get; set; }
        public decimal DMH_1Min { get; set; }
        public decimal DMH_5Min { get; set; }
        public decimal DMH_15Min { get; set; }
        public decimal DMH_30Min { get; set; }
        public decimal DMH_60Min { get; set; }
        public decimal DMH_4Hour { get; set; }
        public decimal DMH_1Day { get; set; }
    }
}
