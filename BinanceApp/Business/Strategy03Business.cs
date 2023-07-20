using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.DataModel;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.Business
{
    public class Strategy03Business
    {
        public SignalState signalState { get; set; }

        private IBinanceKline lastcandle = null;

        public MacdParams macdParams;

        
        public decimal currentPrice;
        public bool allMacdWasCalculated = false;
       
        //public KlineInterval interval { get; set; }
        public string coinName { get; set; }

        public BindingList<string> messageList = new BindingList<string>();

 //       private List<IBinanceKline> candels;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<MacdResult> macdResult;

        private List<IBinanceKline> samples = null;
        public List<IBinanceKline> samples1Min = null;
        public List<IBinanceKline> samples15Min = null;
        public Strategy03Business(string _coinName, KlineInterval _interval = KlineInterval.OneMinute)
        {
            signalState = SignalState.wait;
            //interval = _interval;
            coinName = _coinName;
            macdParams = new MacdParams(12, 26, 9);
        }

        public void SetMacdParams(int fastPeriod, int slowPeriod, int signalPeriod)
        {
            macdParams = new MacdParams(fastPeriod, slowPeriod, signalPeriod);
        }
       
       


       
    }
}
