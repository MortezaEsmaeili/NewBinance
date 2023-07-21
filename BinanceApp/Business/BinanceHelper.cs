using Binance.Net.Interfaces;
using BinanceApp.DataModel;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Interpolation;
using System.Collections.Concurrent;

namespace BinanceApp.Business
{
    public static class BinanceHelper
    {
        public static List<Quote> GetQuotes(List<IBinanceKline> refSamples)
        {
            var history = refSamples.Select(s => new Quote { Close = s.ClosePrice, Date = s.CloseTime, High = s.HighPrice, Low = s.LowPrice, Open = s.OpenPrice, Volume = s.QuoteVolume });
            return history.ToList();
        }

        public static List<MacdResult> MacdCalculationForStepDiagram(List<IBinanceKline> refSamples, MacdParams macdParam,
            ref List<WeightedValue> majorHighList, ref List<WeightedValue> majorLowList)
            
        {

            List<MacdResult> macdResult = new List<MacdResult>();
            if (refSamples == null || refSamples.Any() == false) return null;
            var history = GetQuotes(refSamples);

            var macd = history.GetMacd(macdParam.FastPeriod, macdParam.SlowPeriod, macdParam.SignalPeriod);
            macdResult = macd.SkipWhile(r => r.Macd == null || r.Signal == null).ToList();
            try
            {
                bool sign = macd.Last().Histogram > 0 ? true : false;
                List<Tuple<DateTime, bool>> ZeroCrossHistogram = new List<Tuple<DateTime, bool>>();
                for (int i = macdResult.Count() - 2; i > 0; i--)
                {
                    if (macdResult[i].Histogram <= 0 && sign == true)
                    {
                        ZeroCrossHistogram.Add(new Tuple<DateTime, bool>(macdResult[i].Date, sign));
                        sign = !sign;
                    }
                    else if (macdResult[i].Histogram >= 0 && sign == false)
                    {
                        ZeroCrossHistogram.Add(new Tuple<DateTime, bool>(macdResult[i].Date, sign));
                        sign = !sign;
                    }

                }
                var lastDate = macdResult.Last().Date;
                if (ZeroCrossHistogram == null || ZeroCrossHistogram.Any() == false)
                    return null;
                var lastRefDate = ZeroCrossHistogram.First().Item1;


                var tmpMajorHighList = new List<WeightedValue>();
                var tmpMajorLowList = new List<WeightedValue>();

                // int lastSet = 0;
                foreach (var item in ZeroCrossHistogram.Skip(1))
                {
                    if (item.Item2 == true)
                    {
                        var refPrices = history.Where(h => h.Date < lastRefDate && h.Date >= item.Item1).OrderByDescending(h => h.High);
                        var maxprice = refPrices.FirstOrDefault();
                        if (maxprice != null)
                        {
                            tmpMajorHighList.Add(new WeightedValue { Time = maxprice.Date, Value = maxprice.High });//item.item1
                            lastRefDate = item.Item1;
                        }

                    }
                    else
                    {
                        var refPrices = history.Where(h => h.Date < lastRefDate && h.Date >= item.Item1).OrderBy(h => h.Low);

                        var minPrice = refPrices.FirstOrDefault();
                        if (minPrice != null)
                        {
                            tmpMajorLowList.Add(new WeightedValue { Time = minPrice.Date, Value = minPrice.Low });
                            lastRefDate = item.Item1;
                        }
                    }
                }
                if (tmpMajorHighList.Any())
                    majorHighList = tmpMajorHighList;
                if (tmpMajorLowList.Any())
                    majorLowList = tmpMajorLowList;
            }
            catch (Exception ex)
            {
                majorHighList = null;
                majorLowList = null;
            }

            return macdResult;
        }

        private static void CalculateMacdLocalMinMax(ref List<LocalMinMax> localMaxes, ref List<LocalMinMax> localMins, List<MacdResult> macdResult)
        {
            try
            {
                var tmpLocalMins = new List<LocalMinMax>();
                var tmpLocalMaxes = new List<LocalMinMax>();
                int i = 1;
                while (i < macdResult.Count)
                {
                    if (macdResult[i].Macd > macdResult[i - 1].Macd)
                    {
                        while (macdResult[i].Macd > macdResult[i - 1].Macd)
                        {
                            i++;
                            if (i >= macdResult.Count)
                                break;
                        }
                        if (i < macdResult.Count)
                            tmpLocalMaxes.Add(new LocalMinMax { Date = macdResult[i - 1].Date, Macd = macdResult[i - 1].Macd.Value, index = i - 1 });

                    }
                    else
                    {
                        while (macdResult[i].Macd < macdResult[i - 1].Macd)
                        {
                            i++;
                            if (i >= macdResult.Count)
                                break;
                        }
                        if (i < macdResult.Count)
                            tmpLocalMins.Add(new LocalMinMax { Date = macdResult[i - 1].Date, Macd = macdResult[i - 1].Macd.Value, index = i - 1 });
                    }

                }
                for (int k = 0; k < tmpLocalMaxes.Count; k++)
                {
                    var localMax = tmpLocalMaxes[k];
                    localMax.LeftDelta = 0;
                    if (tmpLocalMins.Any(x => x.index < localMax.index))
                    {
                        var leftMin = tmpLocalMins.Where(x => x.index < localMax.index).Last();
                        localMax.LeftDelta = localMax.Macd - leftMin.Macd;
                    }
                    localMax.RightDelta = 0;
                    if (tmpLocalMins.Any(x => x.index > localMax.index))
                    {
                        var rightMin = tmpLocalMins.Where(x => x.index > localMax.index).First();
                        localMax.RightDelta = localMax.Macd - rightMin.Macd;
                    }
                    localMax.MinDelta = Math.Min(localMax.LeftDelta, localMax.RightDelta);
                    localMax.Sign = (k == 0) ? true : (localMax.Macd > tmpLocalMaxes[k - 1].Macd);
                }
                for (int k = 0; k < tmpLocalMins.Count; k++)
                {
                    var localMin = tmpLocalMins[k];
                    localMin.LeftDelta = 0;
                    if (tmpLocalMaxes.Any(x => x.index < localMin.index))
                    {
                        var leftMax = tmpLocalMaxes.Where(x => x.index < localMin.index).Last();
                        localMin.LeftDelta = leftMax.Macd - localMin.Macd;
                    }
                    localMin.RightDelta = 0;
                    if (tmpLocalMaxes.Any(x => x.index > localMin.index))
                    {
                        var rightMax = tmpLocalMaxes.Where(x => x.index > localMin.index).First();
                        localMin.RightDelta = rightMax.Macd - localMin.Macd;
                    }
                    localMin.MinDelta = Math.Min(localMin.LeftDelta, localMin.RightDelta);
                    localMin.Sign = (k == 0) ? true : (localMin.Macd > tmpLocalMins[k - 1].Macd);
                }
                if (tmpLocalMins.Any())
                    localMins = tmpLocalMins;
                if (tmpLocalMaxes.Any())
                    localMaxes = tmpLocalMaxes;
            }
            catch (Exception ex1)
            {
                localMaxes = null;
                localMins = null;
            }
        }

        public static void CalculateMacdLocalMinMax(ref List<LocalMinMax> localMaxes, ref List<LocalMinMax> localMins, List<WeightedValue> macdResult)
        {
            try
            {
                var tmpLocalMins = new List<LocalMinMax>();
                var tmpLocalMaxes = new List<LocalMinMax>();
                int i = 1;
                while (i < macdResult.Count)
                {
                    if (macdResult[i].Value >= macdResult[i - 1].Value)
                    {
                        while (macdResult[i].Value >= macdResult[i - 1].Value)
                        {
                            i++;
                            if (i >= macdResult.Count)
                                break;
                        }
                        if (i < macdResult.Count)
                            tmpLocalMaxes.Add(new LocalMinMax { Date = macdResult[i - 1].Time, Macd = macdResult[i - 1].Value, index = i - 1 });

                    }
                    else
                    {
                        while (macdResult[i].Value < macdResult[i - 1].Value)
                        {
                            i++;
                            if (i >= macdResult.Count)
                                break;
                        }
                        if (i < macdResult.Count)
                            tmpLocalMins.Add(new LocalMinMax { Date = macdResult[i - 1].Time, Macd = macdResult[i - 1].Value, index = i - 1 });
                    }

                }
                for (int k = 0; k < tmpLocalMaxes.Count; k++)
                {
                    var localMax = tmpLocalMaxes[k];
                    localMax.LeftDelta = 0;
                    if (tmpLocalMins.Any(x => x.index < localMax.index))
                    {
                        var leftMin = tmpLocalMins.Where(x => x.index < localMax.index).Last();
                        localMax.LeftDelta = localMax.Macd - leftMin.Macd;
                    }
                    localMax.RightDelta = 0;
                    if (tmpLocalMins.Any(x => x.index > localMax.index))
                    {
                        var rightMin = tmpLocalMins.Where(x => x.index > localMax.index).First();
                        localMax.RightDelta = localMax.Macd - rightMin.Macd;
                    }
                    localMax.MinDelta = Math.Min(localMax.LeftDelta, localMax.RightDelta);
                    localMax.Sign = (k == 0) ? true : (localMax.Macd > tmpLocalMaxes[k - 1].Macd);
                }
                for (int k = 0; k < tmpLocalMins.Count; k++)
                {
                    var localMin = tmpLocalMins[k];
                    localMin.LeftDelta = 0;
                    if (tmpLocalMaxes.Any(x => x.index < localMin.index))
                    {
                        var leftMax = tmpLocalMaxes.Where(x => x.index < localMin.index).Last();
                        localMin.LeftDelta = leftMax.Macd - localMin.Macd;
                    }
                    localMin.RightDelta = 0;
                    if (tmpLocalMaxes.Any(x => x.index > localMin.index))
                    {
                        var rightMax = tmpLocalMaxes.Where(x => x.index > localMin.index).First();
                        localMin.RightDelta = rightMax.Macd - localMin.Macd;
                    }
                    localMin.MinDelta = Math.Min(localMin.LeftDelta, localMin.RightDelta);
                    localMin.Sign = (k == 0) ? true : (localMin.Macd > tmpLocalMins[k - 1].Macd);
                }
                if (tmpLocalMins.Any())
                    localMins = tmpLocalMins;
                if (tmpLocalMaxes.Any())
                    localMaxes = tmpLocalMaxes;
            }
            catch (Exception ex1)
            {
                localMaxes = null;
                localMins = null;
            }
        }

        public static List<MacdResult> MacdCalculation(List<IBinanceKline> refSamples, MacdParams macdParam, out WeightedValue majorHigh,
            out WeightedValue majorLow)
        {
            List<MacdResult> macdResult = new List<MacdResult>();
            try
            {
                var history = GetQuotes(refSamples);

                var macd = history.GetMacd(macdParam.FastPeriod, macdParam.SlowPeriod, macdParam.SignalPeriod);
                macdResult = macd.SkipWhile(r => r.Macd == null || r.Signal == null).ToList();
                bool sign = macd.Last().Histogram > 0 ? true : false;
                List<DateTime> ZeroCrossHistogram = new List<DateTime>();
                for (int i = macdResult.Count() - 2; i > 0; i--)
                {
                    if (macdResult[i].Histogram <= 0 && sign == true)
                    {
                        ZeroCrossHistogram.Add(macdResult[i].Date);
                        sign = !sign;
                    }
                    else if (macdResult[i].Histogram >= 0 && sign == false)
                    {
                        ZeroCrossHistogram.Add(macdResult[i].Date);
                        sign = !sign;
                    }

                }
                var priceBetweenZeroCrocess = history.Where(h => h.Date > ZeroCrossHistogram.Last() &&
                h.Date <= ZeroCrossHistogram.First()).OrderBy(s => s.Close);
                var lowerPrice = priceBetweenZeroCrocess.First();
                var higherPrice = priceBetweenZeroCrocess.Last();
                majorLow = new WeightedValue { Time = lowerPrice.Date, Value = lowerPrice.Close };
                majorHigh = new WeightedValue { Time = higherPrice.Date, Value = higherPrice.Close };

            }
            catch (Exception ex)
            {
                majorHigh = null;
                majorLow = null;
            }
            return macdResult;
        }
        public static WeightedValue LastFC(List<IBinanceKline> refSamples, decimal currentPrice, int period)
        {
            try
            {
                if (refSamples == null || refSamples.Any() == false || refSamples.Count <= period || period <= 1)
                    return null;
                refSamples.Last().ClosePrice = currentPrice;
                decimal result = 1;
                int lastIndex = refSamples.Count - 1;
                for (int i = 0; i < period; i++)
                {
                    if (refSamples[lastIndex - i - 1].ClosePrice > 0)
                        result *= ((refSamples[lastIndex - i].ClosePrice - refSamples[lastIndex - i - 1].ClosePrice) / refSamples[lastIndex - i - 1].ClosePrice) + 1;
                }
                return new WeightedValue { Time = refSamples[lastIndex].CloseTime, Value = result };
            }
            catch (Exception ex)
            {
                LogHelper.SendError(typeof(BinanceHelper), ex.Message, ex, null);
                return null;
            }
        }
        public static List<WeightedValue> FC(List<IBinanceKline> refSamples, decimal currentPrice, int period)
        {
            try
            {
                if (refSamples == null || refSamples.Any() == false || refSamples.Count <= period)
                    return null;
                List<WeightedValue> Fc = new List<WeightedValue>();

                if (period <= 1)
                {
                    Fc = refSamples.Select(s => new WeightedValue { Time = s.CloseTime, Value = s.ClosePrice }).ToList();
                    Fc.Last().Value = currentPrice;
                    return Fc;
                }
                refSamples.Last().ClosePrice = currentPrice;
                for (int k = period; k < refSamples.Count; k++)
                {
                    decimal result = 1;
                    for (int i = 0; i < period; i++)
                    {
                        if (refSamples[k - i - 1].ClosePrice > 0)
                            result *= ((refSamples[k - i].ClosePrice - refSamples[k - i - 1].ClosePrice) / refSamples[k - i - 1].ClosePrice) + 1;
                    }
                    Fc.Add(new WeightedValue { Time = refSamples[k].CloseTime, Value = result });
                }
                return Fc;
            }
            catch (Exception ex)
            {
                LogHelper.SendError(typeof(BinanceHelper), ex.Message, ex, null);
                return null;
            }
        }
        public static List<WeightedValue> ShortTermFC(List<IBinanceKline> refSamples, decimal currentPrice)
        {
            try
            {
                if (refSamples == null || refSamples.Any() == false || refSamples.Count <= 8)
                    return null;
                var Fc3 = FC(refSamples, currentPrice, 3);
                var Fc5 = FC(refSamples, currentPrice, 5);
                var Fc8 = FC(refSamples, currentPrice, 8);
                if (Fc8 == null || Fc5 == null || Fc3 == null)
                    return null;
                var startTime = Fc8.First().Time;
                Fc3 = Fc3.SkipWhile(x => x.Time < startTime).ToList();
                Fc5 = Fc5.SkipWhile(x => x.Time < startTime).ToList();
                List<WeightedValue> FcShort = new List<WeightedValue>();
                for (int i = 0; i < Fc8.Count; i++)
                    FcShort.Add(new WeightedValue { Time = Fc8[i].Time, Value = Math.Round((Fc3[i].Value + Fc5[i].Value + Fc8[i].Value) * 10000 / 3) / 10000 });
                return FcShort;
            }
            catch (Exception ex)
            {
                LogHelper.SendError(typeof(BinanceHelper), ex.Message, ex, null);
                return null;
            }
        }

        public static List<WeightedValue> LongTermFC(List<IBinanceKline> refSamples, decimal currentPrice)
        {
            try
            {
                if (refSamples == null || refSamples.Any() == false || refSamples.Count <= 30)
                    return null;
                var Fc10 = FC(refSamples, currentPrice, 10);
                var Fc15 = FC(refSamples, currentPrice, 15);
                var Fc30 = FC(refSamples, currentPrice, 30);
                if (Fc30 == null || Fc15 == null || Fc10 == null)
                    return null;
                var startTime = Fc30.First().Time;
                Fc10 = Fc10.SkipWhile(x => x.Time < startTime).ToList();
                Fc15 = Fc15.SkipWhile(x => x.Time < startTime).ToList();
                List<WeightedValue> FcShort = new List<WeightedValue>();
                for (int i = 0; i < Fc30.Count; i++)
                    FcShort.Add(new WeightedValue { Time = Fc30[i].Time, Value = Math.Round((Fc10[i].Value + Fc15[i].Value + Fc30[i].Value) * 10000 / 3) / 10000 });
                return FcShort;
            }
            catch (Exception ex)
            {
                LogHelper.SendError(typeof(BinanceHelper), ex.Message, ex, null);
                return null;
            }
        }

        public static WeightedValue LastShortTermFC(List<IBinanceKline> refSamples, decimal currentPrice)
        {
            if (refSamples == null || refSamples.Any() == false || refSamples.Count <= 8)
                return null;
            var Fc3 = LastFC(refSamples, currentPrice, 3);
            var Fc5 = LastFC(refSamples, currentPrice, 5);
            var Fc8 = LastFC(refSamples, currentPrice, 8);
            if (Fc3 == null || Fc5 == null || Fc8 == null)
                return null;
            return new WeightedValue { Time = Fc8.Time, Value = Math.Round((Fc3.Value + Fc5.Value + Fc8.Value) * 10000 / 3) / 10000 };
        }
        public static WeightedValue LastLongTermFC(List<IBinanceKline> refSamples, decimal currentPrice)
        {
            if (refSamples == null || refSamples.Any() == false || refSamples.Count <= 30)
                return null;
            var Fc10 = LastFC(refSamples, currentPrice, 10);
            var Fc15 = LastFC(refSamples, currentPrice, 15);
            var Fc30 = LastFC(refSamples, currentPrice, 30);
            if (Fc10 == null || Fc15 == null || Fc30 == null)
                return null;
            return new WeightedValue { Time = Fc30.Time, Value = Math.Round(10000 * (Fc10.Value + Fc15.Value + Fc30.Value) / 3) / 10000 };
        }

        public static List<MacdResult> MacdCalculation(List<Quote> history, MacdParams macdParam, out WeightedValue majorHigh, out WeightedValue majorLow)
        {
            List<MacdResult> macdResult = new List<MacdResult>();
            try
            {
                var macd = history.GetMacd(macdParam.FastPeriod, macdParam.SlowPeriod, macdParam.SignalPeriod);
                macdResult = macd.SkipWhile(r => r.Macd == null || r.Signal == null).ToList();
                bool sign = macd.Last().Histogram > 0 ? true : false;
                List<DateTime> ZeroCrossHistogram = new List<DateTime>();
                for (int i = macdResult.Count() - 2; i > 0; i--)
                {
                    if (macdResult[i].Histogram <= 0 && sign == true)
                    {
                        ZeroCrossHistogram.Add(macdResult[i].Date);
                        sign = !sign;
                    }
                    else if (macdResult[i].Histogram >= 0 && sign == false)
                    {
                        ZeroCrossHistogram.Add(macdResult[i].Date);
                        sign = !sign;
                    }
                    if (ZeroCrossHistogram.Count() >= 3)
                        break;
                }
                var priceBetweenZeroCrocess = history.Where(h => h.Date > ZeroCrossHistogram.Last() &&
                h.Date <= ZeroCrossHistogram.First()).OrderBy(s => s.Close);
                var lowerPrice = priceBetweenZeroCrocess.First();
                var higherPrice = priceBetweenZeroCrocess.Last();
                majorLow = new WeightedValue { Time = lowerPrice.Date, Value = lowerPrice.Close };
                majorHigh = new WeightedValue { Time = higherPrice.Date, Value = higherPrice.Close };
                //var observeRange = macdResult.Where(r => r.Date > ZeroCrossHistogram.Last() &&
                //r.Date <= ZeroCrossHistogram.First()).OrderBy(s => s.Histogram);
                //var mhhDate = observeRange.Last().Date;
                //var mlDate = observeRange.First().Date;
                //var mhPrice = history.FirstOrDefault(h => h.Date == mhhDate).Close;
                //var mlPrice = history.FirstOrDefault(h => h.Date == mlDate).Close;
                //majorHigh = new WeightedValue { Time = mhhDate, Value = mhPrice };
                //majorLow = new WeightedValue { Time = mlDate, Value = mlPrice };
            }
            catch (Exception ex)
            {
                majorHigh = null;
                majorLow = null;
            }
            return macdResult;
        }

        public static List<SmaResult> SMA(List<Quote> history, int period)//simple moving avaerage
        {
            return history.GetSma(period).Where(x => x.Sma != null).ToList();
        }
        public static List<EmaResult> EMA(List<Quote> history, int period)//exponential moving avaerage
        {
            return history.GetEma(period).Where(x => x.Ema != null).ToList();
        }
        public static List<WeightedValue> CalulateDerivative(List<EmaResult> data)
        {
            try
            {
                var xvec = new DenseVector(data.Select(x => (double)x.Date.Ticks).ToArray());
                var yvec = new DenseVector(data.Select(x => (double)x.Ema).ToArray());
                var cs = CubicSpline.InterpolateNatural(xvec, yvec);
                var dydx = xvec.Select(x => new WeightedValue
                {
                    Time = new DateTime((long)x),
                    Value = (decimal)cs.Differentiate(x)
                }).ToList();
                return dydx;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static double CalulateDerivativeAtDate(List<EmaResult> refData, DateTime date)
        {
            try
            {
                var data = refData.OrderBy(x => x.Date).Take(200).ToList();
                data = refData.Where(x => Math.Abs((x.Date - date).TotalMinutes) < 30).OrderBy(x => x.Date).ToList();
                var firsDate = data.First().Date;

                var xvec = new DenseVector(data.Select(x => (x.Date - firsDate).TotalMinutes).ToArray());
                var yvec = new DenseVector(data.Select(x => (double)x.Ema).ToArray());
                var cs = CubicSpline.InterpolateNatural(xvec, yvec);
                var retval = cs.Differentiate((date - firsDate).TotalMinutes);
                return retval;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static List<WeightedValue> Subtract_Limits(List<WeightedValue> MH, List<WeightedValue> ML)
        {
            try
            {
                List<DateTime> timeArr = new List<DateTime>();
                timeArr.AddRange(MH.Select(item => item.Time));
                timeArr.AddRange(ML.Select(item => item.Time));
                timeArr.Sort();

                var test1 = MH.ToList().OrderBy(item => item.Time).ToList();
                var test2 = ML.ToList().OrderBy(item => item.Time).ToList();

                var temp = new List<WeightedValue>();

                foreach (DateTime dt in timeArr)
                {
                    decimal value = 0;
                    var cat1 = test1.Where(t => t.Time <= dt).LastOrDefault();
                    var cat2 = test2.Where(t => t.Time <= dt).LastOrDefault();
                    if (cat1 == null || cat2 == null)
                        value = 0;
                    else
                        value = (cat1.Value - cat2.Value);

                    temp.Add(new WeightedValue { Time = dt, Value = value });
                }

                return temp;
            }
            catch { return null; }
        }

    }
}
