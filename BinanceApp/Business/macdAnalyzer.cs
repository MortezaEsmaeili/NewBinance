using Binance.Net.Interfaces;
using BinanceApp.Business;
using BinanceApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace BinanceApp.Busines
{
    public class MacdAnalyclass
    {
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
        public Dictionary<String, (Timeframe, string)> MacdStateDic = new Dictionary<string, (Timeframe, string)>();

        internal void DoAnalysis()
        {
            try
            {
                MacdStateDic.Clear();
                foreach (string coinName in BinanceDataCollector.Instance.CoinNames)
                {
                    var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                    if (coinInfo == null) { continue; }
                    String State = GetState(coinInfo,
                        coinInfo.Candels_1min.Skip(coinInfo.Candels_1min.Count() - 200).Take(200).ToList());
                    MacdStateDic.Add(coinName, (Timeframe.tf_1min,State));
                    State = GetState(coinInfo,
                        coinInfo.Candels_5min.Skip(coinInfo.Candels_5min.Count() - 200).Take(200).ToList());
                    MacdStateDic.Add(coinName, (Timeframe.tf_5min, State));
                    State = GetState(coinInfo,
                        coinInfo.Candels_15min.Skip(coinInfo.Candels_15min.Count() - 200).Take(200).ToList());
                    MacdStateDic.Add(coinName, (Timeframe.tf_15min, State));
                    State = GetState(coinInfo,
                        coinInfo.Candels_30min.Skip(coinInfo.Candels_30min.Count() - 200).Take(200).ToList());
                    MacdStateDic.Add(coinName, (Timeframe.tf_30min, State));
                    State = GetState(coinInfo,
                        coinInfo.Candels_60min.Skip(coinInfo.Candels_60min.Count() - 200).Take(200).ToList());
                    MacdStateDic.Add(coinName, (Timeframe.tf_1h, State));
                    State = GetState(coinInfo,
                        coinInfo.Candels_2hour.Skip(coinInfo.Candels_2hour.Count() - 200).Take(200).ToList());
                    MacdStateDic.Add(coinName, (Timeframe.tf_2h, State));
                    State = GetState(coinInfo,
                        coinInfo.Candels_4hour.Skip(coinInfo.Candels_4hour.Count() - 200).Take(200).ToList());
                    MacdStateDic.Add(coinName, (Timeframe.tf_4h, State));
                    State = GetState(coinInfo,
                        coinInfo.Candels_oneDay.Skip(coinInfo.Candels_oneDay.Count() - 200).Take(200).ToList());
                    MacdStateDic.Add(coinName, (Timeframe.tf_1d, State));
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private string GetState(BinanceModel coinInfo, List<IBinanceKline> candels)
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


            return "?";
        }

        private IndicatorBase CreateMACDIndicator(List<IBinanceKline> candels)
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
    }
}