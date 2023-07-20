using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    public class MacdParams
    {
        public int FastPeriod { get; set; }
        public int SlowPeriod { get; set; }
        public int SignalPeriod { get; set; }
        public MacdParams(int fastPeriod, int slowPeriod, int signalPeriod)
        {
            FastPeriod = fastPeriod;
            SlowPeriod = slowPeriod;
            SignalPeriod = signalPeriod;
        }
    }
}
