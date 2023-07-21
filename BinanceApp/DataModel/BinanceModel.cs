using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    public class BinanceModel
    { 
        
        public string coinName { get; set; }
        private List<TradeModel> tradeList;
        public List<CritcalPoint> CritcalPoints { get; set; }

        public List<EmaResult>[] MarginDatas { get; set; }
        public List<TradeModel> TradeList
        {
            get { return tradeList; }
            set
            {
                
                tradeList = value;
                //Trades.AddRange(tradeList);
            }
        }
        private List<MarketModel> buyerList;
        
        public List<MarketModel> BuyerList {
            get { return buyerList; }
            set{
                if (value == null) return;
                buyerList = value;
                if ( value.Count() >= 5)
                {
                    var val = CalculateWeightedValue(value.GetRange(0, 5));
                    if (val != null)
                        BidsWightedPrice5.Add(val);
                }
                if (value.Count() >= 10)
                {
                    var val = CalculateWeightedValue(value.GetRange(0, 10));
                    if (val != null)
                        BidsWightedPrice10.Add(val);
                }
                if (value.Count() >= 20)
                {
                    var val = CalculateWeightedValue(value.GetRange(0, 20));
                    if (val != null)
                        BidsWightedPrice20.Add(val);
                }
                if (value.Count() >= 50)
                {
                    var val = CalculateWeightedValue(value.GetRange(0, 50));
                    if (val != null)
                        BidsWightedPrice50.Add(val);
                }
            }
        }

        private List<MarketModel> sellerList;
        public List<MarketModel> SellerList
        {
            get { return sellerList; }
            set
            {
                if (value == null) return;
                sellerList = value;
                if (value.Count() >= 5)
                {
                    var val = CalculateWeightedValue(value.GetRange(0, 5));
                    if (val != null)
                        AskWightedPrice5.Add(val);
                }
                if (value.Count() >= 10)
                {
                    var val = CalculateWeightedValue(value.GetRange(0, 10));
                    if (val != null)
                        AskWightedPrice10.Add(val);
                }
                if (value.Count() >= 20)
                {
                    var val = CalculateWeightedValue(value.GetRange(0, 20));
                    if (val != null)
                        AskWightedPrice20.Add(val);
                }
                if (value.Count() >= 50)
                {
                    var val = CalculateWeightedValue(value.GetRange(0, 50));
                    if (val != null)
                        AskWightedPrice50.Add(val);
                }
            }
        }
        //public List<TradeModel> Trades { get; set; }

        public List<IBinanceKline> Candels_1min { get; set; }
        
        public List<IBinanceKline> Candels_5min { get; set; }
        
        public List<IBinanceKline> Candels_15min { get; set; }
        public List<IBinanceKline> Candels_30min { get; set; }
        public List<IBinanceKline> Candels_60min { get; set; }

        public List<IBinanceKline> Candels_2hour { get; set; }
        public List<IBinanceKline> Candels_4hour { get; set; }
        public List<IBinanceKline> Candels_oneDay { get; set; }

        public List<IBinanceKline> Candels_oneWeek { get; set; }

        public List<MacdResult> MacdResult1Min { get; set; }

        public List<WeightedValue> MajorHighPrice1Min { get; set; }
        public List<WeightedValue> MajorHighPrice5Min { get; set; }
        public List<WeightedValue> MajorHighPrice15Min { get; set; }
        public List<WeightedValue> MajorHighPrice30Min { get; set; }
        public List<WeightedValue> MajorHighPrice60Min { get; set; }
        public List<WeightedValue> MajorHighPrice2hour { get; set; }
        public List<WeightedValue> MajorHighPrice4hour { get; set; }
        public List<WeightedValue> MajorHighPrice1Day { get; set; }
        public List<WeightedValue> MajorHighPrice1Week { get; set; }

        public List<WeightedValue> MajorLowPrice1Min { get; set; }
        public List<WeightedValue> MajorLowPrice5Min { get; set; }
        public List<WeightedValue> MajorLowPrice15Min { get; set; }
        public List<WeightedValue> MajorLowPrice30Min { get; set; }
        public List<WeightedValue> MajorLowPrice60Min { get; set; }
        public List<WeightedValue> MajorLowPrice2hour { get; set; }
        public List<WeightedValue> MajorLowPrice4hour { get; set; }
        public List<WeightedValue> MajorLowPrice1Day { get; set; }
        public List<WeightedValue> MajorLowPrice1Week { get; set; }

        public List<LocalMinMax> MacdLocalMin1Min { get; set; }
        public List<LocalMinMax> MacdLocalMin5Min { get; set; }
        public List<LocalMinMax> MacdLocalMin15Min { get; set; }
        public List<LocalMinMax> MacdLocalMin30Min { get; set; }
        public List<LocalMinMax> MacdLocalMin60Min { get; set; }
        public List<LocalMinMax> MacdLocalMin2hour { get; set; }
        public List<LocalMinMax> MacdLocalMin4hour { get; set; }
        public List<LocalMinMax> MacdLocalMin1Day { get; set; }
        public List<LocalMinMax> MacdLocalMin1Week { get; set; }

        public List<LocalMinMax> MacdLocalMax1Min { get; set; }
        public List<LocalMinMax> MacdLocalMax5Min { get; set; }
        public List<LocalMinMax> MacdLocalMax15Min { get; set; }
        public List<LocalMinMax> MacdLocalMax30Min { get; set; }
        public List<LocalMinMax> MacdLocalMax60Min { get; set; }
        public List<LocalMinMax> MacdLocalMax2hour { get; set; }
        public List<LocalMinMax> MacdLocalMax4hour { get; set; }
        public List<LocalMinMax> MacdLocalMax1Day { get; set; }
        public List<LocalMinMax> MacdLocalMax1Week { get; set; }

        public List<Quote> Tbqv_1min { get; set; }

        public List<Quote> Tbqv_5min { get; set; }

        public List<Quote> Tbqv_15min { get; set; }
        public List<Quote> Tbqv_4H { get; set; }

        public List<WeightedValue> AskWightedPrice5 { get; set; }
        public List<WeightedValue> AskWightedPrice10 { get; set; }
        public List<WeightedValue> AskWightedPrice20 { get; set; }
        public List<WeightedValue> AskWightedPrice50 { get; set; }

        public List<WeightedValue> BidsWightedPrice5 { get; set; }
        public List<WeightedValue> BidsWightedPrice10 { get; set; }
        public List<WeightedValue> BidsWightedPrice20 { get; set; }
        public List<WeightedValue> BidsWightedPrice50 { get; set; }

        public List<WeightedValue> MP50 { get; set; }

        public List<WeightedValue> PriceList { get; set; }

        public Dictionary<KlineInterval, decimal> DMLDic { get; set; }
        public Dictionary<KlineInterval, decimal> DMHDic { get; set; }

        public decimal CurrentPrice { 
            get 
            {
                if (PriceList != null && PriceList.Any())
                    return PriceList.Last().Value;
                else
                    return 0;
            } 
        }
        public BinanceModel(string _coinName)
        {
            coinName = _coinName;
            tradeList = new List<TradeModel>();
            buyerList = new List<MarketModel>();
            sellerList = new List<MarketModel>();
            Candels_1min = new List<IBinanceKline>();
            Candels_5min = new List<IBinanceKline>();
            Candels_15min = new List<IBinanceKline>();
            Candels_30min = new List<IBinanceKline>();
            Candels_60min = new List<IBinanceKline>();
            Candels_2hour = new List<IBinanceKline>();
            Candels_4hour = new List<IBinanceKline>();
            Candels_oneDay = new List<IBinanceKline>();
            Candels_oneWeek = new List<IBinanceKline>();

            DMHDic = new Dictionary<KlineInterval, decimal>();
            DMLDic = new Dictionary<KlineInterval, decimal>();
            CritcalPoints = new List<CritcalPoint>();
            //Tbqv_1min = new List<Quote>();
            //Tbqv_5min = new List<Quote>();
            //Tbqv_15min = new List<Quote>();

            //Trades = new List<TradeModel>();

            PriceList = new List<WeightedValue>();
            AskWightedPrice5 = new List<WeightedValue>();
            AskWightedPrice10 = new List<WeightedValue>();
            AskWightedPrice20 = new List<WeightedValue>();
            AskWightedPrice50 = new List<WeightedValue>();

            BidsWightedPrice5 = new List<WeightedValue>();
            BidsWightedPrice10 = new List<WeightedValue>();
            BidsWightedPrice20 = new List<WeightedValue>();
            BidsWightedPrice50 = new List<WeightedValue>();
            MP50 = new List<WeightedValue>();
            MarginDatas = new List<EmaResult>[7];
            for (int i = 0; i < 7; i++)
                MarginDatas[i] = new List<EmaResult>();
        }

        public WeightedValue CalculateWeightedValue(List<MarketModel> samples)
        {
            var quantitySum = samples.Sum(q => q.Quantity);
            var WeightedSum = samples.Sum(v => v.Quantity * v.Price);
            if (quantitySum > 0)
                return new WeightedValue { Time = PriceList.Last().Time, Value = WeightedSum / quantitySum };
            else
                return null;
        }
        public void WightedPrice(int n, List<TradeModel> n_preLastSamples, List<TradeModel> newSamples, List<WeightedValue>wightedValues)
        {
            if (n_preLastSamples == null)
            {
                n_preLastSamples = new List<TradeModel>();
                for(int i=0; i<n-1; i++) 
                {
                    n_preLastSamples.Add(new TradeModel { Price = 0 , Quantity=0}); 
                }
            }

            var targetValues = n_preLastSamples;
            targetValues.AddRange(newSamples);
            for(int i=n;i<targetValues.Count; i++ )
            {
                var range = targetValues.GetRange(i - n, n);
                var quantitySum = range.Sum(q => q.Quantity);
                var WeightedSum = range.Sum(v => v.Quantity * v.Price);
                if (quantitySum > 0)
                    wightedValues.Add(new WeightedValue { Value = WeightedSum / quantitySum, Time = range.Last().Time });
            }
        }
        
    }
}
