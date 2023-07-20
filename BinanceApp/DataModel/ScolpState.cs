using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    enum ScolpState
    {
        initial,
        overMajorHigh,
        backFromMajorHigh,
        UnderMajorLow,
        backFromMajorLow,
        StopScolp,
        near2MajorHigh,
        near2MajorLow
    }
    public enum SignalState
    {
        wait,
        Ready,
        BuyPosition,
        SellPosition
    }
}
