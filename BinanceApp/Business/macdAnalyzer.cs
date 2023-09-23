using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.Business;
using BinanceApp.DataModel;
using MathNet.Numerics;
using Skender.Stock.Indicators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace BinanceApp.Busines
{
    public class MacdAnalyclass
    {
        public delegate void UpdateMacdStateDelegate(string coinName, KlineInterval interval, string state);
        public static UpdateMacdStateDelegate UpdateMacdEvent;
        public static void OnUpdateMacdState(string coinName, KlineInterval interval, string state)
        {
            if(UpdateMacdEvent != null) UpdateMacdEvent(coinName, interval, state);
        }
        private static MacdAnalyclass instance = null;
        public static MacdAnalyclass Instance
        {
            get
            {
                if (instance == null)
                    instance = new MacdAnalyclass();
                return instance;
            }
        }
        
        public static void DoAnalyze(KlineInterval interval, string coinName)
        {
            try
            {
                var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                List<IBinanceKline> candles = null;
                if (coinInfo == null) { return; }
                string state = "?";
                switch (interval)
                {

                    case KlineInterval.OneMinute:
                        candles = coinInfo.Candels_1min.Skip(coinInfo.Candels_1min.Count() - 200).Take(200).ToList();
                        break;
                    case KlineInterval.FiveMinutes:
                        candles = coinInfo.Candels_5min.Skip(coinInfo.Candels_5min.Count() - 200).Take(200).ToList();
                        break;
                    case KlineInterval.FifteenMinutes:
                        candles = coinInfo.Candels_15min.Skip(coinInfo.Candels_15min.Count() - 200).Take(200).ToList();
                        break;
                    case KlineInterval.ThirtyMinutes:
                        candles = coinInfo.Candels_30min.Skip(coinInfo.Candels_30min.Count() - 200).Take(200).ToList();
                        break;
                    case KlineInterval.OneHour:
                        candles = coinInfo.Candels_60min.Skip(coinInfo.Candels_60min.Count() - 200).Take(200).ToList();
                        break;
                    case KlineInterval.TwoHour:
                        candles = coinInfo.Candels_2hour.Skip(coinInfo.Candels_2hour.Count() - 200).Take(200).ToList();
                        break;
                    case KlineInterval.FourHour:
                        candles = coinInfo.Candels_4hour.Skip(coinInfo.Candels_4hour.Count() - 200).Take(200).ToList();
                        break;
                    case KlineInterval.OneDay:
                        candles = coinInfo.Candels_oneDay.Skip(coinInfo.Candels_oneDay.Count() - 200).Take(200).ToList();
                        break;
                    default: break;
                }
                if (candles != null)
                {
                    state = GetState(candles);
                   
                    OnUpdateMacdState(coinName, interval, state);
                }
            }catch(Exception ex) { }

        }
    
        
        private static string GetState( List<IBinanceKline> candels)
        {
            IndicatorBase indicator = CreateMACDIndicator(candels);
            List<double> macdValues = new List<double>();
            List<WeightedValue> macd = new List<WeightedValue>();
            for (int i = 0; i < indicator.DataPoints.Count; i++)
            {
                var value = indicator.GetProcessedValue(i);
                macdValues.Add(value);
                macd.Add(new WeightedValue { Time = candels[i].CloseTime, Value = (decimal)value });
            }
            var localMins = new List<LocalMinMax>();
            var localMaxes = new List<LocalMinMax>();
            BinanceHelper.CalculateMacdLocalMinMax(ref localMaxes, ref localMins, macd);

            return GetStateFromLocalMinMax(localMaxes, localMins);

        }

        private static String GetStateFromLocalMinMax(List<LocalMinMax> localMaxes, List<LocalMinMax> localMins)
        {
            int idxMax=-1,idxMin=-1;
            var lastPositive = localMaxes.LastOrDefault(x => x.Macd > 0);
            var lastNegative = localMaxes.LastOrDefault(x => x.index < lastPositive.index && x.Macd < 0);
            if (lastPositive != null && lastNegative != null)
            {
                var points = localMaxes.Where(x => x.index > lastNegative.index && x.index <= lastPositive.index).ToList();
                var maxPoint = points.Where(p => p.Macd == points.Max(x => x.Macd)).First();
                points = points.Where(x => x.index >= maxPoint.index).ToList();
                if (points.Count > 2)
                {
                    double[] xdata = points.Select(x => (double)x.index).ToArray();
                    double[] ydata = points.Select(x => (double)x.Macd).ToArray();
                    Tuple<double, double> p = Fit.Line(xdata, ydata);
                    if (p.Item2 < 0)
                        idxMax = lastPositive.index;
                }
            }

            lastNegative = localMins.LastOrDefault(x => x.Macd < 0);
            lastPositive = localMins.LastOrDefault(x => x.index < lastNegative.index && x.Macd > 0);
            if (lastPositive != null && lastNegative != null)
            {
                var points = localMins.Where(x => x.index > lastPositive.index && x.index <= lastNegative.index).ToList();
                var minPoint = points.Where(p => p.Macd == points.Min(x => x.Macd)).First();
                points = points.Where(x => x.index >= minPoint.index).ToList();
                if (points.Count > 2)
                {
                    double[] xdata = points.Select(x => (double)x.index).ToArray();
                    double[] ydata = points.Select(x => (double)x.Macd).ToArray();
                    Tuple<double, double> p = Fit.Line(xdata, ydata);
                    if (p.Item2 > 0)
                        idxMin = lastNegative.index;
                }
            }
            if (idxMax > idxMin )
                return "-";
            else if( idxMax < idxMin)
                return "+";
            else return "?";
        }

        private static IndicatorBase CreateMACDIndicator(List<IBinanceKline> candels)
        {
            MacdIndicator indicator = new MacdIndicator();
            indicator.ShortPeriod = 9;
            indicator.LongPeriod = 12;
            indicator.SignalPeriod = 6;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "ClosePrice";
            indicator.DataSource = candels;

            return indicator;
        }

        internal static List<WeightedValue> FitLine(List<SmaResult> points)
        {
            List<WeightedValue> line = new List<WeightedValue>();
            try
            {
                if (points.Count() > 2)
                {
                    double[] xdata = new double[points.Count()];
                    for (int i = 0; i < points.Count(); i++) { xdata[i] = i; }
                    double[] ydata = points.Select(x => (double)x.Sma).ToArray();
                    Tuple<double, double> p = Fit.Line(xdata, ydata);
                    for (int i = 0; i < points.Count(); i++)
                    {
                        line.Add(new WeightedValue { Time = points[i].Date, Value = (decimal)(p.Item1 + p.Item2 * i) });
                    }
                }
            }catch(Exception ex) { }
            return line;
        }
    }
}