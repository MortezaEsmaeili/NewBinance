using Binance.Net.Enums;
using BinanceApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.Business
{
    public static class BTCUSDT_SF
    {
        public static Dictionary<KlineInterval, List<WeightedValue>> ShortTerm_SFDic;

        public static Dictionary<KlineInterval, List<WeightedValue>> LongTerm_SFDic;

        static BTCUSDT_SF()
        {
            ShortTerm_SFDic = new Dictionary<KlineInterval, List<WeightedValue>>();
            LongTerm_SFDic = new Dictionary<KlineInterval, List<WeightedValue>>();
            foreach (var interval in BinanceDataCollector.Instance.Intervals)
            {
                ShortTerm_SFDic.Add(interval, new List<WeightedValue>());
                LongTerm_SFDic.Add(interval, new List<WeightedValue>());
            }
        }
        public static void UpdateShortTerm(KlineInterval  interval, List<WeightedValue> sortTermSF)
        {
            ShortTerm_SFDic[interval] = sortTermSF;
        }
        public static void UpdateLongTerm(KlineInterval interval, List<WeightedValue> longTermSF)
        {
            LongTerm_SFDic[interval] = longTermSF;
        }
        public static List<WeightedValue> GetLongTermSF(KlineInterval interval)
        {
            return LongTerm_SFDic[interval];
        }
        public static List<WeightedValue> GetShortTermSF(KlineInterval interval)
        {
            return ShortTerm_SFDic[interval];
        }
    }
}
