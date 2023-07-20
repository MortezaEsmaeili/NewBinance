using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.DataModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.Business
{
    public delegate void AlertSignal(string coinName, KlineInterval interval, AlertInfo alertInfo);
    public static class StrategySFBusiness
    {
        public static ConcurrentDictionary<string, SignalState> SignalStateDic { get; set; }
        public static string FilePath { get; set; }
        public static event AlertSignal AlertEvent;
        public static void OnAlertEvent(string coinName, KlineInterval interval, AlertInfo alertInfo)
        {
            if (AlertEvent != null)
                AlertEvent(coinName, interval, alertInfo);
        }
        static StrategySFBusiness()
        {
            SignalStateDic = new ConcurrentDictionary<string, SignalState>();

            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;

            FilePath = ConfigurationManager.AppSettings["FilePath"];// FilePath = "C:\\BinanceLog\\";//
            try
            {
                foreach (var coin in BinanceDataCollector.Instance.CoinNames)
                {
                    var path = Path.Combine(FilePath, coin);
                    if (Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(path);
                        foreach(var interval in BinanceDataCollector.Instance.Intervals)
                        {
                            SignalStateDic.TryAdd($"{coin}_{interval}", SignalState.wait);
                            string header = "LocalTime, CloseTime, Price, ST_SF, LT_SF, DML, DMH, MH, ML, Signal";
                            var filePath = Path.Combine(FilePath, coin, $"{coin}_{interval}.csv");
                            if (File.Exists(filePath) == false)
                            {
                                using (var fs = new StreamWriter(filePath, true))
                                {
                                    fs.WriteLine(header);
                                }
                            }
                        }
                    }
                }
                AlertEvent += WriteAlertToFile;
            }
            catch (Exception ex)
            {
            }
        }

        private static void WriteAlertToFile(string coin, KlineInterval interval, AlertInfo alert)
        {
            var filePath = Path.Combine(FilePath, coin, $"{coin}_{interval}.csv");
            using (var fs = new StreamWriter(filePath, true))
            {
                string line = $"{alert.LocalTime}, {alert.CloseTime}, {alert.Price}, {alert.ST_SF},{alert.LT_SF}" +
                    $", {alert.DML}, {alert.DMH}, {alert.MH}, {alert.ML}, {alert.Signal}";
                fs.WriteLine(line);
            }

        }

        private static void OnCandleReadyEvent(KlineInterval interval, string coinName)
        {
            var binanceModel = BinanceDataCollector.Instance.GetBinance(coinName);
            List<IBinanceKline> candels = new List<IBinanceKline>();
            decimal majorHigh = 0;
            decimal majorLow = 0;
            switch (interval)
            {
                case KlineInterval.OneMinute:
                    candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 50).Take(50).ToList();
                    majorHigh = (binanceModel.MajorHighPrice1Min != null && binanceModel.MajorHighPrice1Min.Any()) ?
                        binanceModel.MajorHighPrice1Min.First().Value : 0;
                    majorLow = (binanceModel.MajorLowPrice1Min != null && binanceModel.MajorLowPrice1Min.Any()) ?
                        binanceModel.MajorLowPrice1Min.First().Value : 0;

                    break;
                case KlineInterval.FiveMinutes:
                    candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 50).Take(50).ToList();
                    majorHigh = (binanceModel.MajorHighPrice5Min != null && binanceModel.MajorHighPrice5Min.Any()) ?
                        binanceModel.MajorHighPrice5Min.First().Value : 0;
                    majorLow = (binanceModel.MajorLowPrice5Min != null && binanceModel.MajorLowPrice5Min.Any()) ?
                        binanceModel.MajorLowPrice5Min.First().Value : 0;

                    break;
                case KlineInterval.FifteenMinutes:
                    candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 50).Take(50).ToList();
                    majorHigh = (binanceModel.MajorHighPrice15Min != null && binanceModel.MajorHighPrice15Min.Any()) ?
                        binanceModel.MajorHighPrice15Min.First().Value : 0;
                    majorLow = (binanceModel.MajorLowPrice15Min != null && binanceModel.MajorLowPrice15Min.Any()) ?
                        binanceModel.MajorLowPrice15Min.First().Value : 0;

                    break;
                case KlineInterval.ThirtyMinutes:
                    candels = binanceModel.Candels_30min.Skip(binanceModel.Candels_30min.Count() - 50).Take(50).ToList();
                    majorHigh = (binanceModel.MajorHighPrice30Min != null && binanceModel.MajorHighPrice30Min.Any()) ?
                        binanceModel.MajorHighPrice30Min.First().Value : 0;
                    majorLow = (binanceModel.MajorLowPrice30Min != null && binanceModel.MajorLowPrice30Min.Any()) ?
                        binanceModel.MajorLowPrice30Min.First().Value : 0;

                    break;
                case KlineInterval.OneHour:
                    candels = binanceModel.Candels_60min.Skip(binanceModel.Candels_60min.Count() - 50).Take(50).ToList();
                    majorHigh = (binanceModel.MajorHighPrice60Min != null && binanceModel.MajorHighPrice60Min.Any()) ?
                        binanceModel.MajorHighPrice60Min.First().Value : 0;
                    majorLow = (binanceModel.MajorLowPrice60Min != null && binanceModel.MajorLowPrice60Min.Any()) ?
                        binanceModel.MajorLowPrice60Min.First().Value : 0;

                    break;
                case KlineInterval.FourHour:
                    candels = binanceModel.Candels_4hour.Skip(binanceModel.Candels_4hour.Count() - 50).Take(50).ToList();
                    majorHigh = (binanceModel.MajorHighPrice4hour != null && binanceModel.MajorHighPrice4hour.Any()) ?
                        binanceModel.MajorHighPrice4hour.First().Value : 0;
                    majorLow = (binanceModel.MajorLowPrice4hour != null && binanceModel.MajorLowPrice4hour.Any()) ?
                        binanceModel.MajorLowPrice4hour.First().Value : 0;

                    break;
                case KlineInterval.OneDay:
                    candels = binanceModel.Candels_oneDay.Skip(binanceModel.Candels_oneDay.Count() - 50).Take(50).ToList();
                    majorHigh = (binanceModel.MajorHighPrice1Day != null && binanceModel.MajorHighPrice1Day.Any()) ?
                        binanceModel.MajorHighPrice1Day.First().Value : 0;
                    majorLow = (binanceModel.MajorLowPrice1Day != null && binanceModel.MajorLowPrice1Day.Any()) ?
                        binanceModel.MajorLowPrice1Day.First().Value : 0;

                    break;
            }
            if (candels == null || candels.Count == 0) return;
            var sortTerm_Sf = BinanceHelper.LastShortTermFC(candels, binanceModel.CurrentPrice);
            var longTerm_SF = BinanceHelper.LastLongTermFC(candels, binanceModel.CurrentPrice);
            
            SignalState signalState = SignalState.wait;
            if (sortTerm_Sf.Value>1 && longTerm_SF.Value>=sortTerm_Sf.Value)
            {
                //create buy signal
                signalState = SignalState.BuyPosition;
            }
            else if(longTerm_SF.Value<=1 && sortTerm_Sf.Value<=1)
            {
                //create sell signal
                signalState = SignalState.SellPosition;
            }

            string key = $"{coinName}_{interval}";

            if (SignalStateDic[key] != signalState)
            {
                SignalStateDic[key] = signalState;
                var localTime = DateTime.Now;
                var dmh = binanceModel.DMHDic.ContainsKey(interval) ? binanceModel.DMHDic[interval] : 0;
                var dml = binanceModel.DMLDic.ContainsKey(interval) ? binanceModel.DMLDic[interval] : 0;
                var alert = new AlertInfo
                {
                    CloseTime = sortTerm_Sf.Time,
                    LocalTime = localTime,
                    DMH = dmh,
                    DML = dml,
                    LT_SF = Math.Round( longTerm_SF.Value*100)/100,
                    ST_SF = Math.Round(sortTerm_Sf.Value*100)/100,
                    Price = binanceModel.CurrentPrice,
                    Signal = signalState,
                    MH = majorHigh,
                    ML = majorLow
                };
                OnAlertEvent(coinName, interval, alert);
            }
        }
    }
}
