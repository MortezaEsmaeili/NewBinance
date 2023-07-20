using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.DataModel;
using Skender.Stock.Indicators;
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
    public static class CriticalPointCollector
    {
        public static ConcurrentDictionary<string, string> FilePathDic { get; set; }
        public static string FilePath{get; set;}
        public static void Initialize()
        {
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;

            FilePathDic = new ConcurrentDictionary<string, string>();
            FilePath=ConfigurationManager.AppSettings["FilePath"];// FilePath = "C:\\BinanceLog\\";//
            try
            {
                if (Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
            }
            catch(Exception ex)
            {
            }
        }

       
        private static void OnCandleReadyEvent(KlineInterval interval, string coin)
        {
            try
            {
                if (FilePathDic.ContainsKey(coin) == false)
                {
                    var fileName = $"{coin}_{DateTime.Now.ToString("yyyyMMddhh")}.csv";
                    var filePath = Path.Combine(FilePath, fileName);
                    FilePathDic.TryAdd(coin, filePath);
                    string header = "Date,Price,EMA5,EMA25,EMA75,EMA150,EMA300,DEMA5,DEMA25,DEMA75,DEMA150,DEMA300," +
                        "MH1,MH5,MH15,MH30,MH60,ML1,ML5,ML15,ML30,ML60,Desc";
                    using (var fs = new StreamWriter(filePath, false))
                    {
                        fs.WriteLine(header);
                    }
                }

                var bmodel = BinanceDataCollector.Instance.GetBinance(coin);
                if (bmodel == null) return;
                bool calculatrAll = false;
                if (bmodel.CritcalPoints.Any()==false)
                    calculatrAll = true;
                List<CritcalPoint> newCriticalPoints = new List<CritcalPoint>();
                if (interval == KlineInterval.OneMinute || interval == KlineInterval.FiveMinutes)
                {
                    var crPoint = DetermineCriticalPoint(bmodel.MajorHighPrice5Min,
                        bmodel.MajorHighPrice1Min,bmodel.Candels_1min, "MH", calculatrAll);
                    if (crPoint.Any())
                        newCriticalPoints.AddRange(crPoint);
                    crPoint = DetermineCriticalPoint(bmodel.MajorLowPrice5Min,
                       bmodel.MajorLowPrice1Min, bmodel.Candels_1min, "ML", calculatrAll);
                    if (crPoint.Any())
                        newCriticalPoints.AddRange(crPoint);
                }
                if (interval != KlineInterval.OneMinute)
                {
                    //MH
                    var crPoint = DetermineCriticalPoint(bmodel.MajorHighPrice5Min,
                        bmodel.MajorHighPrice15Min, bmodel.Candels_5min, "MH", calculatrAll);
                    if (crPoint.Any())
                        newCriticalPoints.AddRange(crPoint);
                    crPoint = DetermineCriticalPoint(bmodel.MajorHighPrice5Min,
                        bmodel.MajorHighPrice30Min, bmodel.Candels_5min, "MH", calculatrAll);
                    if (crPoint.Any())
                        newCriticalPoints.AddRange(crPoint);
                    crPoint = DetermineCriticalPoint(bmodel.MajorHighPrice5Min,
                        bmodel.MajorHighPrice60Min, bmodel.Candels_5min, "MH", calculatrAll);
                    if (crPoint.Any())
                        newCriticalPoints.AddRange(crPoint);
                    //ML
                    crPoint = DetermineCriticalPoint(bmodel.MajorLowPrice5Min,
                       bmodel.MajorLowPrice15Min, bmodel.Candels_5min, "ML", calculatrAll);
                    if (crPoint.Any())
                        newCriticalPoints.AddRange(crPoint);
                    crPoint = DetermineCriticalPoint(bmodel.MajorLowPrice5Min,
                        bmodel.MajorLowPrice30Min, bmodel.Candels_5min, "ML", calculatrAll);
                    if (crPoint.Any())
                        newCriticalPoints.AddRange(crPoint);
                    crPoint = DetermineCriticalPoint(bmodel.MajorLowPrice5Min,
                        bmodel.MajorLowPrice60Min, bmodel.Candels_5min, "ML", calculatrAll);
                    if (crPoint.Any())
                        newCriticalPoints.AddRange(crPoint);
                }
                if (newCriticalPoints.Any())
                {

                    if (bmodel.CritcalPoints.Any() == true)
                    {
                        var lastCriticalPoint = bmodel.CritcalPoints.Last();
                        newCriticalPoints = newCriticalPoints.Where(x => x.Date > lastCriticalPoint.Date).ToList();
                    }
                    if (newCriticalPoints.Any() == true)
                    {
                        List<CritcalPoint> resultedPoints = new List<CritcalPoint>();
                        var candles = bmodel.Candels_1min;//.Skip(bmodel.Candels_1min.Count() - 650).Take(650).ToList();
                        var qutoes = BinanceHelper.GetQuotes(candles);
                        var minDate = qutoes.OrderBy(x => x.Date).First().Date;
                        var Ema5 = BinanceHelper.EMA(qutoes, 5);
                        var Ema25 = BinanceHelper.EMA(qutoes, 25);
                        var Ema75 = BinanceHelper.EMA(qutoes, 75);
                        var Ema150 = BinanceHelper.EMA(qutoes, 150);
                        var Ema300 = BinanceHelper.EMA(qutoes, 300);
                        
                        foreach (var crPoint in newCriticalPoints.Where(x=>x.Date>=minDate))
                        {
                            crPoint.ML1 = DetermineValueAtDate(bmodel.MajorLowPrice1Min, crPoint.Date);
                            crPoint.ML5 = DetermineValueAtDate(bmodel.MajorLowPrice5Min, crPoint.Date);
                            crPoint.ML15 = DetermineValueAtDate(bmodel.MajorLowPrice15Min, crPoint.Date);
                            crPoint.ML30 = DetermineValueAtDate(bmodel.MajorLowPrice30Min, crPoint.Date);
                            crPoint.ML60 = DetermineValueAtDate(bmodel.MajorLowPrice60Min, crPoint.Date);

                            crPoint.MH1 = DetermineValueAtDate(bmodel.MajorHighPrice1Min, crPoint.Date);
                            crPoint.MH5 = DetermineValueAtDate(bmodel.MajorHighPrice5Min, crPoint.Date);
                            crPoint.MH15 = DetermineValueAtDate(bmodel.MajorHighPrice15Min, crPoint.Date);
                            crPoint.MH30 = DetermineValueAtDate(bmodel.MajorHighPrice30Min, crPoint.Date);
                            crPoint.MH60 = DetermineValueAtDate(bmodel.MajorHighPrice60Min, crPoint.Date);
                            
                            crPoint.EMA5 = DetermineValueAtDate(Ema5, crPoint.Date);
                            crPoint.EMA25 = DetermineValueAtDate(Ema25, crPoint.Date);
                            crPoint.EMA75 = DetermineValueAtDate(Ema75, crPoint.Date);
                            crPoint.EMA150 = DetermineValueAtDate(Ema150, crPoint.Date);
                            crPoint.EMA300 = DetermineValueAtDate(Ema300, crPoint.Date);

                            crPoint.DEMA5 = BinanceHelper.CalulateDerivativeAtDate(Ema5, crPoint.Date);
                            crPoint.DEMA25 = BinanceHelper.CalulateDerivativeAtDate(Ema25, crPoint.Date);
                            crPoint.DEMA75 = BinanceHelper.CalulateDerivativeAtDate(Ema75, crPoint.Date);
                            crPoint.DEMA150 = BinanceHelper.CalulateDerivativeAtDate(Ema150, crPoint.Date);
                            crPoint.DEMA300 = BinanceHelper.CalulateDerivativeAtDate(Ema300, crPoint.Date);

                            resultedPoints.Add(crPoint);
                        }
                        if (resultedPoints.Any())
                        {
                            resultedPoints = resultedPoints.OrderBy(x => x.Date).ToList();
                            BinanceDataCollector.Instance.AddCriticalPoints(coin, resultedPoints);
                            SavePointTF1(coin, resultedPoints);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            //finally
            //{
            //    underProcess = false;
            //}
        }

        private static decimal DetermineValueAtDate(List<WeightedValue> listA, DateTime date)
        {
            var val = listA.Where(x => x.Time >= date).OrderBy(x => date).FirstOrDefault();
            if (val != null)
                return val.Value;
            else
            {
                val = listA.Where(x => x.Time < date).OrderByDescending(x => date).FirstOrDefault();
                if (val != null)
                    return val.Value;
            }
            return 0;
        }

        private static decimal DetermineValueAtDate(List<EmaResult> listA, DateTime date)
        {
            var val = listA.Where(x => x.Date >= date).OrderBy(x => date).FirstOrDefault();
            if (val != null && val.Ema.HasValue)
                return val.Ema.Value;
            else
                return 0;
        }
        public static void SavePointTF1(string coin, List<CritcalPoint> criticalPoints)
        {
            var filePath = FilePathDic[coin];
            using (var fs = new StreamWriter(filePath, true))
            {
                foreach (var crp in criticalPoints)
                {
                    var line = $"{crp.Date},{crp.Price},{crp.EMA5},{crp.EMA25},{crp.EMA75},{crp.EMA150},{crp.EMA300}," +
                      $"{crp.DEMA5},{crp.DEMA25},{crp.DEMA75},{crp.DEMA150},{crp.DEMA300},{crp.MH1},{crp.MH5}," +
                      $"{crp.MH15},{crp.MH30},{crp.MH60},{crp.ML1},{crp.ML5},{crp.ML15},{crp.ML30},{crp.ML60},{crp.Description}";

                    fs.WriteLine(line);

                }
            }

        }

        private static List<CritcalPoint> DetermineCriticalPoint(List<WeightedValue> listA,
            List<WeightedValue> listB, List<IBinanceKline> candles, string desc, bool calculateForAll = false)
        {
            List<CritcalPoint> newCriticalPoints = new List<CritcalPoint>();
            var minTimeA = listA.Min(t => t.Time);
            var minTimeB = listB.Min(t => t.Time);
            var minTime = minTimeA;
            if (minTimeB > minTime)
                minTime = minTimeB;
            try
            {
                
                var A_B = BinanceHelper.Subtract_Limits(listA, listB).Where(x=>x.Time> minTime).ToList();
                if (calculateForAll)
                {
                    foreach (var m in A_B)
                    {
                        var candleAtCriticalPoint = candles.Where(p => p.CloseTime >= m.Time).OrderBy(p => p.CloseTime).FirstOrDefault();
                        decimal price = 0;
                        if (desc == "ML") 
                            price = candleAtCriticalPoint.LowPrice;
                        else
                            price = candleAtCriticalPoint.HighPrice;
                        var a = Math.Abs(0.001 * (double)price);
                        if (candleAtCriticalPoint != null && (double)Math.Abs(m.Value) < Math.Abs(0.001 * (double)price))
                        {
                            newCriticalPoints.Add(new CritcalPoint { Date = m.Time, Price = price, Description = desc });
                        }
                    }
                }
                else
                {
                    var lastvalue = A_B.LastOrDefault();
                    if (lastvalue != null)
                    {
                        var candleAtCriticalPoint = candles.Where(p => p.CloseTime >= lastvalue.Time).OrderBy(p => p.CloseTime).FirstOrDefault();
                        if (candleAtCriticalPoint != null )
                        {
                            decimal price = 0;
                            if (desc == "ML")
                                price = candleAtCriticalPoint.LowPrice;
                            else
                                price = candleAtCriticalPoint.HighPrice;
                            var a = Math.Abs(0.001 * (double)price);
                            if (candleAtCriticalPoint != null && (double)Math.Abs(lastvalue.Value) < Math.Abs(0.001 * (double)price))
                            {
                                newCriticalPoints.Add(new CritcalPoint { Date = lastvalue.Time, Price = price, Description = desc });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return newCriticalPoints;

        }
    }
}
