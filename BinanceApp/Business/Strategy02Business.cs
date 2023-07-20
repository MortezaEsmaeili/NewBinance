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
    public class Strategy02Business
    {
       
        ScolpState scolpState;
        public SignalState signalState { get; set; }

        private IBinanceKline lastcandle = null;

        public MacdParams macdParams;
        public WeightedValue majorHighPrice;
        public WeightedValue  majorLowPrice;

        public decimal currentPrice;

        public decimal stopLostPrice = 0;
        public decimal takeProfitPrice = 0;

        public WeightedValue lowPrice ;
        public WeightedValue highPrice ;

        public int percentageMargin { get; set; }
        public KlineInterval interval { get; set; }
        public string coinName { get; set; }

        public string generalState = "Weak Area";
        public BindingList<string> messageList = new BindingList<string>();

 //       private List<IBinanceKline> candels;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        

        public List<MacdResult> macdResult;

        public List<IBinanceKline> samples = null;

        public Strategy02Business(string _coinName, KlineInterval _interval, int _percentageMargin)
        {
            scolpState = ScolpState.initial;
            signalState = SignalState.wait;
            percentageMargin = _percentageMargin;
            interval = _interval;
            coinName = _coinName;
            majorHighPrice = new WeightedValue { Value = 0, Time = DateTime.Now };
            majorLowPrice = new WeightedValue { Value = 0, Time = DateTime.Now };
            lowPrice = new WeightedValue { Value = 0, Time = DateTime.Now };
            highPrice = new WeightedValue { Value = 0, Time = DateTime.Now };
            messageAdd("Please be ready!");
            macdParams = new MacdParams(12, 26, 9);
        }

        public void SetMacdParams(int fastPeriod, int slowPeriod, int signalPeriod)
        {
            macdParams = new MacdParams(fastPeriod, slowPeriod, signalPeriod);
        }
        private void initialScolpState()
        {
            takeProfitPrice = majorHighPrice.Value;
            stopLostPrice = majorLowPrice.Value;
            decimal pricePercentage = percentageMargin * currentPrice / 100 ;
            if ((majorHighPrice.Value - majorLowPrice.Value) > pricePercentage)
                generalState = "Trade Area";
            else
            {
                generalState = "Weak Area";
                return;
            }

            if (currentPrice >= (majorHighPrice.Value -  (majorHighPrice.Value - majorLowPrice.Value) / 4) && currentPrice < majorHighPrice.Value)
            {
                messageAdd("Near to Major High");
                nearToMajorHigh();
                return;
            }

            if (currentPrice <= (majorLowPrice.Value + (majorHighPrice.Value - majorLowPrice.Value) / 4) && currentPrice > majorLowPrice.Value)
            {
                messageAdd("Near to Major Low");
                nearToMajorLow();
            }
        }

        private void nearToMajorLow()
        {
            scolpState = ScolpState.near2MajorLow;
            if (currentPrice < majorLowPrice.Value)
            {
                messageAdd("Under Major Low");
                scolpState = ScolpState.UnderMajorLow;
                signalState = SignalState.SellPosition;
                stopLostPrice = (majorLowPrice.Value< (currentPrice * 101 / 100)) ? majorLowPrice.Value : (currentPrice * 101 / 100);
                takeProfitPrice = currentPrice * 97 / 100;

                messageAdd($"Instant price= {currentPrice}");
                messageAdd($"Stop loss= {stopLostPrice}");
                messageAdd($"Take Profit= {takeProfitPrice}");
                return;
            }
            if (lastcandle.ClosePrice > lastcandle.OpenPrice)
            {
                messageAdd("Back from Major Low");
                scolpState = ScolpState.backFromMajorLow;
                signalState = SignalState.BuyPosition;
                stopLostPrice = (majorLowPrice.Value > (currentPrice * 99 / 100)) ? majorLowPrice.Value : (currentPrice * 99 / 100);
                takeProfitPrice = currentPrice * 103 / 100;
                messageAdd($"Instant price= {currentPrice}");
                messageAdd($"Stop loss= {stopLostPrice}");
                messageAdd($"Take Profit= {takeProfitPrice}");
                return;
            }
        }

        internal void DataReady(BinanceModel binanceModel)
        {
            samples = null;
            switch (interval)
            {
                case KlineInterval.OneMinute:
                    samples = binanceModel.Candels_1min;
                    break;
                case KlineInterval.FiveMinutes:
                    samples = binanceModel.Candels_5min;
                    break;
                case KlineInterval.FifteenMinutes:
                    samples = binanceModel.Candels_15min;
                    break;
            }
            if (samples == null || samples.Count()==0)
                return;
            macdResult = BinanceHelper.MacdCalculation(binanceModel.Candels_15min
                , macdParams, out WeightedValue highPoint, out WeightedValue lowPoint);
            macdResult= macdResult.Skip(macdResult.Count() - 100).Take(100).ToList();
            if (highPoint != null && lowPoint != null)
            {
                majorHighPrice = highPoint;
                majorLowPrice = lowPoint;
            }
            if(interval == KlineInterval.FifteenMinutes)
            {
                if (highPoint != null && lowPoint != null)
                {
                    lowPrice = lowPoint;
                    highPrice = highPoint;
                }
            }
            else
            {
                macdResult =BinanceHelper.MacdCalculation(samples,macdParams, out  highPoint, out  lowPoint);
                macdResult = macdResult.Skip(macdResult.Count() - 100).Take(100).ToList();
                if (highPoint != null && lowPoint != null)
                {
                    lowPrice = lowPoint;
                    highPrice = highPoint;
                }
            }
            

            if (binanceModel.PriceList == null || binanceModel.PriceList.Count == 0) return;
            currentPrice = binanceModel.PriceList.Last().Value;
            lastcandle = samples.Last();
            
            
            //var history = samples.Select(s => new Quote { Close = s.Close, Date = s.CloseTime, High = s.High, Low = s.Low, Open = s.Open, Volume = s.QuoteVolume });
            //var macdResult = history.GetMacd(12, 26, 9);
            //macdResult1 = macdResult.SkipWhile(r => r.Macd == null || r.Signal == null).ToList();
            //bool sign = macdResult.Last().Histogram > 0 ? true : false;
            //List<DateTime> ZeroCrossHistogram = new List<DateTime>();
            //for (int i = macdResult1.Count() - 2; i > 0; i--)
            //{
            //    if (macdResult1[i].Histogram <= 0 && sign == true)
            //    {
            //        ZeroCrossHistogram.Add(macdResult1[i].Date);
            //        sign = !sign;
            //    }
            //    else if (macdResult1[i].Histogram >= 0 && sign == false)
            //    {
            //        ZeroCrossHistogram.Add(macdResult1[i].Date);
            //        sign = !sign;
            //    }
            //    if (ZeroCrossHistogram.Count() >= 3)
            //        break;
            //}
            //var observeRange = macdResult.Where(r => r.Date > ZeroCrossHistogram.Last() && r.Date <= ZeroCrossHistogram.First()).OrderBy(s => s.Histogram);
            //majorHighDate   = observeRange.Last().Date;
            //majorLowDate    = observeRange.First().Date;
            //majorHighPrice = history.FirstOrDefault(h => h.Date == majorHighDate).Close;
            //majorLowPrice  = history.FirstOrDefault(h => h.Date == majorLowDate).Close;
            //List<Quote> orderedQuote = new List<Quote>();
            //if (majorHighDate > majorLowDate)
            //{
            //    orderedQuote = history.Where(r => r.Date >= majorLowDate && r.Date <= majorHighDate).OrderBy(h => h.Close).ToList();
            //}
            //else
            //{
            //    orderedQuote = history.Where(r => r.Date >= majorHighDate && r.Date <= majorLowDate).OrderBy(h => h.Close).ToList();
            //}
            //Quote lowQuote = orderedQuote.First();
            //Quote highQuote = orderedQuote.Last();

            //var tempPrice = history.Where(r => r.Date > majorHighDate);
            //highPrice = tempPrice.Max(m => m.Close);
            //var tempPrice1 = history.Where(r => r.Date > majorLowDate);
            //lowPrice = tempPrice1.Min(m => m.Close);


            StartDate = macdResult.First().Date;
            EndDate = macdResult.Last().Date;

            switch (scolpState)
            {
                case ScolpState.initial:
                    initialScolpState();
                    break;
                case ScolpState.overMajorHigh:
                    overMajorHighPosition();
                    break;
                case ScolpState.backFromMajorHigh:
                    backFromMajorHighPosition();
                    break;
                case ScolpState.UnderMajorLow:
                    underMajorLowPosition();
                    break;
                case ScolpState.backFromMajorLow:
                    backFromMajorLowPosition();
                    break;
                case ScolpState.StopScolp:
                    break;
                case ScolpState.near2MajorHigh:
                    nearToMajorHigh();
                    break;
                case ScolpState.near2MajorLow:
                    nearToMajorLow();
                    break;
            }
        }
        private void nearToMajorHigh()
        {
            scolpState = ScolpState.near2MajorHigh;
            if (currentPrice > majorHighPrice.Value)
            {
                messageAdd("Over Major High");
                scolpState = ScolpState.overMajorHigh;
                signalState = SignalState.BuyPosition;
                stopLostPrice = (majorHighPrice.Value > (currentPrice * 99 / 100)) ? majorHighPrice.Value : (currentPrice * 99 / 100);
                takeProfitPrice = currentPrice * 103 / 100;
                messageAdd($"Instant price= {currentPrice}");
                messageAdd($"Stop loss= {stopLostPrice}");
                messageAdd($"Take Profit= {takeProfitPrice}");
                return;
            }
            if (lastcandle.ClosePrice < lastcandle.OpenPrice)
            {
                messageAdd("Back from Major High");
                scolpState = ScolpState.backFromMajorHigh;
                signalState = SignalState.SellPosition;
                stopLostPrice = (majorHighPrice.Value < (currentPrice * 101 / 100)) ? majorHighPrice.Value : (currentPrice * 101 / 100);
                takeProfitPrice = currentPrice * 97 / 100;
                messageAdd($"Instant price= {currentPrice}");
                messageAdd($"Stop loss= {stopLostPrice}");
                messageAdd($"Take Profit= {takeProfitPrice}");
                return;
            }
        }
        private void backFromMajorLowPosition()
        {
            var oldStopPrice = stopLostPrice;
            stopLostPrice = (currentPrice > (majorLowPrice.Value + 1 * (majorHighPrice.Value - majorLowPrice.Value) / 4)) ?
                majorLowPrice.Value + 1 * (majorHighPrice.Value - majorLowPrice.Value) / 4 : stopLostPrice;
            stopLostPrice = (currentPrice > (majorLowPrice.Value + 2 * (majorHighPrice.Value - majorLowPrice.Value) / 4)) ?
                majorLowPrice.Value + 2 * (majorHighPrice.Value - majorLowPrice.Value) / 4 : stopLostPrice;
            stopLostPrice = (currentPrice > (majorLowPrice.Value + 3 * (majorHighPrice.Value - majorLowPrice.Value) / 4)) ?
                majorLowPrice.Value + 3 * (majorHighPrice.Value - majorLowPrice.Value) / 4 : stopLostPrice;

            if (oldStopPrice != stopLostPrice)
            {
                SystemSounds.Beep.Play();
                messageAdd("Stop loss price changed!");
                messageAdd($"Stop loss= {stopLostPrice}");
            }
            if (currentPrice >= takeProfitPrice)
            {
                signalState = SignalState.SellPosition;
                scolpState = ScolpState.StopScolp;
                messageAdd("End of Scolp");
                generalState = "Position Closed!";
                return;
            }
            if (currentPrice <= stopLostPrice)
            {
                signalState = SignalState.SellPosition;
                scolpState = ScolpState.StopScolp;
                messageAdd("End of Scolp");
                generalState = "Position Closed!";
                return;
            }
        }

        private void underMajorLowPosition()
        {
            if (currentPrice <= takeProfitPrice)
            {
                signalState = SignalState.BuyPosition;
                scolpState = ScolpState.StopScolp;
                messageAdd("End of Scolp");
                return;
            }
            if (currentPrice >= stopLostPrice)
            {
                signalState = SignalState.BuyPosition;
                scolpState = ScolpState.StopScolp;
                messageAdd("End of Scolp");
                return;
            }
        }

        private void backFromMajorHighPosition()
        {
            var oldStopPrice = stopLostPrice;
            stopLostPrice = (currentPrice < (majorHighPrice.Value - 1 * (majorHighPrice.Value - majorLowPrice.Value) / 4)) ?
                majorHighPrice.Value - 1 * (majorHighPrice.Value - majorLowPrice.Value) / 4 : stopLostPrice;
            stopLostPrice = (currentPrice < (majorHighPrice.Value - 2 * (majorHighPrice.Value - majorLowPrice.Value) / 4)) ?
                majorHighPrice.Value - 2 * (majorHighPrice.Value - majorLowPrice.Value) / 4 : stopLostPrice;
            stopLostPrice = (currentPrice < (majorHighPrice.Value - 3 * (majorHighPrice.Value - majorLowPrice.Value) / 4)) ?
                majorHighPrice.Value - 3 * (majorHighPrice.Value - majorLowPrice.Value) / 4 : stopLostPrice;

            if(oldStopPrice != stopLostPrice)
            {
                SystemSounds.Beep.Play();
                messageAdd("Stop loss price changed!");
                messageAdd($"Stop loss= {stopLostPrice}");
            }    

            if (currentPrice >= stopLostPrice)
            {
                signalState = SignalState.BuyPosition;
                scolpState = ScolpState.StopScolp;
                messageAdd("End of Scolp");
                generalState = "Position Closed!";
                return;
            }
            if (currentPrice <= takeProfitPrice)
            {
                signalState = SignalState.BuyPosition;
                scolpState = ScolpState.StopScolp;
                messageAdd("End of Scolp");
                generalState = "Position Closed!";
                return;
            }
        }

        private void overMajorHighPosition()
        {
            //Check the stop lost
            if (currentPrice <= stopLostPrice)
            {
                signalState = SignalState.SellPosition;
                scolpState = ScolpState.StopScolp;
                messageAdd("End of Scolp");
                return;
            }
            stopLostPrice = majorHighPrice.Value;
            //check the take profit
            if (currentPrice >= takeProfitPrice)
            {
                signalState = SignalState.SellPosition;
                scolpState = ScolpState.StopScolp;
                messageAdd("End of Scolp");
                return;
            }

            //check the low and high
        }

        private void messageAdd(string strMessage)
        {
            if (messageList.Count == 0)
            {
                messageList.Add(strMessage);
                return;
            }
            if (messageList.Last() != strMessage)
            {
                messageList.Add(strMessage);
            }
        }
    }
}
