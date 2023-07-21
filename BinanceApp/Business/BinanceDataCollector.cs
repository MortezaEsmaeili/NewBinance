using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.DataModel;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Skender.Stock.Indicators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinanceApp.Business
{
    public delegate void DataReadyDlg();
    public delegate void CandleReadyDlg(KlineInterval interval, string coinName);
    public class BinanceDataCollector
    {
        private static BinanceDataCollector instance = null;
        private ConcurrentDictionary<string,BinanceModel> CoinsDictionary;
        public List<string> CoinNames;
        public List<KlineInterval> Intervals;
        private Timer DataGatherTimer;
        private Timer CheckDataConnectionTimer;
        private int dataConnectionCounter = 0;
        public event DataReadyDlg DataReadyEvent;
        public event CandleReadyDlg CandleReadyEvent;
        private MarketType marketType = MarketType.SPOT;
        private BinanceSocketClient socketClient = null;
        private List<BinanceSocketClient> socketClientsList = new List<BinanceSocketClient>();
        public static BinanceDataCollector Instance
        {
            get
            {
                if (instance == null)
                    instance = new BinanceDataCollector();
                return instance;
            }
        }
        public void OnDataReady()
        {
            if (DataReadyEvent != null)
                DataReadyEvent();
        }
        public void OnCandleReady(KlineInterval interval, string coinName)
        {
            if (CandleReadyEvent != null)
            {
                CandleReadyEvent(interval, coinName);
            }
        }
        private BinanceDataCollector()
        {
            LogHelper.SendInfo(this, "Data Collector, is initializing ...");

            CoinNames = new List<string> { "BTCUSDT" , "ALGOUSDT" ,  "SOLUSDT", "DOGEUSDT", "ADAUSDT",
                "MATICUSDT","KAVAUSDT","LTCUSDT","BNBUSDT" };
           // CoinNames = new List<string> { "BTCUSDT"};
            Intervals = new List<KlineInterval> { KlineInterval.OneMinute, KlineInterval.FiveMinutes, KlineInterval.FifteenMinutes, KlineInterval.ThirtyMinutes,
                KlineInterval.OneHour, KlineInterval.TwoHour, KlineInterval.FourHour, KlineInterval.OneDay, KlineInterval.OneWeek};
            BinanceRestClient.SetDefaultOptions(options =>
            {
                options.ApiCredentials = new ApiCredentials("78KoVDEzrJM58vGxuz1nG6MZV3HnXRAHlQsfH9POskT5FrZa7204Oaa92jfK34ZL",
                "4jtBPUQyMloPBVLFGDljW90SboVIp4LziUEki6ycaV6dXQFomsSalpjAn36KHwGT");

                /*ApiCredentials = new ApiCredentials("SY61riBzzSBnq6tpRnS33mKYxmhvV6KOn79YqQI9A3Zj8HUjbvyTdnvc4PBx2StM",
                    "mPC57oyfUCt1F582cjENjINU3kn5QspmnXZKhVPUFMjM5wssUBlWVDY10T5evUqp"),*/
            });
            BinanceSocketClient.SetDefaultOptions(options =>
            {
                options.ApiCredentials = new ApiCredentials("78KoVDEzrJM58vGxuz1nG6MZV3HnXRAHlQsfH9POskT5FrZa7204Oaa92jfK34ZL",
                "4jtBPUQyMloPBVLFGDljW90SboVIp4LziUEki6ycaV6dXQFomsSalpjAn36KHwGT");
                /*ApiCredentials = new ApiCredentials("SY61riBzzSBnq6tpRnS33mKYxmhvV6KOn79YqQI9A3Zj8HUjbvyTdnvc4PBx2StM",
                    "mPC57oyfUCt1F582cjENjINU3kn5QspmnXZKhVPUFMjM5wssUBlWVDY10T5evUqp"),*/
            });

            CoinsDictionary = new ConcurrentDictionary<string, BinanceModel>();

            foreach (var coinName in CoinNames)
            {
                CoinsDictionary.TryAdd(coinName, new BinanceModel(coinName));
            }

            DataGatherTimer = new Timer(OnTimerTick, null, 1000, 5000);
            //DataGatherTimer = new Timer(OnTimerTick, null, 1000, 1000);
            CheckDataConnectionTimer = new Timer(OnConnectionTimerTick, null, 10000, 1000);
            dataConnectionCounter = 0;
        }

        public void InitializeModels(MarketType _marketType)
        {
            marketType = _marketType;
            LogHelper.SendInfo(this, "Data Collector, Getting candle history.");

            CoinNames.AsParallel().ForAll(async coinName =>
            {
                while (CoinsDictionary[coinName].Candels_1min == null || CoinsDictionary[coinName].Candels_1min.Count <= 0)
                {
                    CoinsDictionary[coinName].Candels_1min = await GetKLinesAsync(KlineInterval.OneMinute, coinName);
                }
                CalcMajorHighMajorLow(coinName, KlineInterval.OneMinute);
                CalculateTbqv_1min(coinName);
            });
            
            CoinNames.AsParallel().ForAll(async coinName =>
            {
                while (CoinsDictionary[coinName].Candels_5min == null || CoinsDictionary[coinName].Candels_5min.Count <= 0)
                {
                    CoinsDictionary[coinName].Candels_5min = await GetKLinesAsync(KlineInterval.FiveMinutes, coinName);
                }
                CalcMajorHighMajorLow(coinName, KlineInterval.FiveMinutes);
                CalculateTbqv_5min(coinName);
            });
            CoinNames.AsParallel().ForAll(async coinName =>
            { 
                while (CoinsDictionary[coinName].Candels_15min == null || CoinsDictionary[coinName].Candels_15min.Count <= 0)
                {
                    CoinsDictionary[coinName].Candels_15min = await GetKLinesAsync(KlineInterval.FifteenMinutes, coinName);
                }
                CalcMajorHighMajorLow(coinName, KlineInterval.FifteenMinutes);

                CalculateTbqv_15min(coinName);
            });
            CoinNames.AsParallel().ForAll(async coinName =>
            {
                while (CoinsDictionary[coinName].Candels_30min == null || CoinsDictionary[coinName].Candels_30min.Count <= 0)
                {
                    CoinsDictionary[coinName].Candels_30min = await GetKLinesAsync(KlineInterval.ThirtyMinutes, coinName);
                }
                CalcMajorHighMajorLow(coinName, KlineInterval.ThirtyMinutes);
            });

            CoinNames.AsParallel().ForAll(async coinName =>
            {
                while (CoinsDictionary[coinName].Candels_60min == null || CoinsDictionary[coinName].Candels_60min.Count <= 0)
                {
                    CoinsDictionary[coinName].Candels_60min = await GetKLinesAsync(KlineInterval.OneHour, coinName);
                }
                CalcMajorHighMajorLow(coinName, KlineInterval.OneHour);
            });
            CoinNames.AsParallel().ForAll(async coinName =>
            {
                while (CoinsDictionary[coinName].Candels_4hour == null || CoinsDictionary[coinName].Candels_4hour.Count <= 0)
                {
                    CoinsDictionary[coinName].Candels_4hour = await GetKLinesAsync(KlineInterval.FourHour, coinName);
                }
                CalcMajorHighMajorLow(coinName, KlineInterval.FourHour);
                CalculateTbqv_4H(coinName);
            });
            CoinNames.AsParallel().ForAll(async coinName =>
            {
                while (CoinsDictionary[coinName].Candels_2hour == null || CoinsDictionary[coinName].Candels_2hour.Count <= 0)
                {
                    CoinsDictionary[coinName].Candels_2hour = await GetKLinesAsync(KlineInterval.TwoHour, coinName);
                }
                CalcMajorHighMajorLow(coinName, KlineInterval.TwoHour);
            });
            CoinNames.AsParallel().ForAll(async coinName =>
            {
                while (CoinsDictionary[coinName].Candels_oneDay == null || CoinsDictionary[coinName].Candels_oneDay.Count <= 0)
                {
                    CoinsDictionary[coinName].Candels_oneDay = await GetKLinesAsync(KlineInterval.OneDay, coinName);
                }
                CalcMajorHighMajorLow(coinName, KlineInterval.OneDay);
            });
            CoinNames.AsParallel().ForAll(async coinName =>
            {
                while (CoinsDictionary[coinName].Candels_oneWeek == null || CoinsDictionary[coinName].Candels_oneWeek.Count <= 0)
                {
                    CoinsDictionary[coinName].Candels_oneWeek = await GetKLinesAsync(KlineInterval.OneWeek, coinName);
                }
                CalcMajorHighMajorLow(coinName, KlineInterval.OneWeek);
            });
        }
        private void CalculateTbqv_1min(string coinName)
        {
            CoinsDictionary[coinName].Tbqv_1min = new List<Quote>();
            
            var refCandles = CoinsDictionary[coinName].Candels_1min.Skip(CoinsDictionary[coinName].Candels_1min.Count() - 100).Take(100).ToList();
            foreach (var x in refCandles)
            {
                if (x.QuoteVolume != 0)
                {
                    var value = x.TakerBuyQuoteVolume / x.QuoteVolume;
                    CoinsDictionary[coinName].Tbqv_1min.Add(new Quote { Date = x.CloseTime, Close = value, High = value, Low = value, Open = value, Volume = x.QuoteVolume });
                }
            }
            
        }
        private void CalculateTbqv_5min(string coinName)
        {
            CoinsDictionary[coinName].Tbqv_5min = new List<Quote>();

            var refCandles = CoinsDictionary[coinName].Candels_5min.Skip(CoinsDictionary[coinName].Candels_5min.Count() - 100).Take(100).ToList();
            foreach (var x in refCandles)
            {
                if (x.QuoteVolume != 0)
                {
                    var value = x.TakerBuyQuoteVolume / x.QuoteVolume;
                    CoinsDictionary[coinName].Tbqv_5min.Add(new Quote { Date = x.CloseTime, Close = value, High = value, Low = value, Open = value, Volume = x.QuoteVolume });
                }
            }
        }

        private void CalculateTbqv_15min(string coinName)
        {
            CoinsDictionary[coinName].Tbqv_15min = new List<Quote>();
            var refCandles = CoinsDictionary[coinName].Candels_15min.Skip(CoinsDictionary[coinName].Candels_15min.Count() - 100).Take(100).ToList();
            foreach (var x in refCandles)
            {
                if (x.QuoteVolume != 0)
                {
                    var value = x.TakerBuyQuoteVolume / x.QuoteVolume;
                    CoinsDictionary[coinName].Tbqv_15min.Add(new Quote { Date = x.CloseTime, Close = value, High = value, Low = value, Open = value, Volume = x.QuoteVolume });
                }
            }
        }
        private void CalculateTbqv_4H(string coinName)
        {
            CoinsDictionary[coinName].Tbqv_4H = new List<Quote>();
            var refCandles = CoinsDictionary[coinName].Candels_4hour.Skip(CoinsDictionary[coinName].Candels_4hour.Count() - 100).Take(100).ToList();
            foreach (var x in refCandles)
            {
                if (x.QuoteVolume != 0)
                {
                    var value = x.TakerBuyQuoteVolume / x.QuoteVolume;
                    CoinsDictionary[coinName].Tbqv_4H.Add(new Quote { Date = x.CloseTime, Close = value, High = value, Low = value, Open = value, Volume = x.QuoteVolume });
                }
            }
        }
        internal void AddCriticalPoints(string coinName, List<CritcalPoint> critcalPoints)
        {
            try
            {
                CoinsDictionary[coinName].CritcalPoints.AddRange(critcalPoints);
                if (CoinsDictionary[coinName].CritcalPoints.Count > 20)
                    CoinsDictionary[coinName].CritcalPoints.RemoveRange(0, 5);
            }
            catch(Exception ex)
            {
                LogHelper.SendError(this, ex.Message, ex, null);
                Console.WriteLine($"ex 215: {ex.ToString()}");
            }
        }

        private void OnUpdateCandle(IBinanceStreamKlineData data)
        {
            try
            {
                dataConnectionCounter = 0;

                LogHelper.SendInfo(this, "Data Collector, Updating candle information");
                if (!CoinNames.Contains(data.Symbol) || data.Data==null) return;
                switch(data.Data.Interval)
                {
                    case KlineInterval.OneMinute:
                        UpdateCandle(data.Data.Interval, data.Symbol, CoinsDictionary[data.Symbol].Candels_1min, CoinsDictionary[data.Symbol].Tbqv_1min, data.Data);
                        break;
                    case KlineInterval.FiveMinutes:
                        UpdateCandle(data.Data.Interval, data.Symbol, CoinsDictionary[data.Symbol].Candels_5min, CoinsDictionary[data.Symbol].Tbqv_5min, data.Data);
                        break;
                    case KlineInterval.FifteenMinutes:
                        UpdateCandle(data.Data.Interval, data.Symbol, CoinsDictionary[data.Symbol].Candels_15min, CoinsDictionary[data.Symbol].Tbqv_15min, data.Data);
                        break;
                    case KlineInterval.ThirtyMinutes:
                        UpdateCandle(data.Data.Interval, data.Symbol, CoinsDictionary[data.Symbol].Candels_30min, null, data.Data);
                        break;
                    case KlineInterval.OneHour:
                        UpdateCandle(data.Data.Interval, data.Symbol, CoinsDictionary[data.Symbol].Candels_60min, null, data.Data);
                        break;
                    case KlineInterval.TwoHour:
                        UpdateCandle(data.Data.Interval, data.Symbol, CoinsDictionary[data.Symbol].Candels_2hour, null, data.Data);
                        break;
                    case KlineInterval.FourHour:
                        UpdateCandle(data.Data.Interval, data.Symbol, CoinsDictionary[data.Symbol].Candels_4hour, null, data.Data);
                        break;
                    case KlineInterval.OneDay:
                        UpdateCandle(data.Data.Interval, data.Symbol, CoinsDictionary[data.Symbol].Candels_oneDay, null, data.Data);
                        break;
                    case KlineInterval.OneWeek:
                        UpdateCandle(data.Data.Interval, data.Symbol, CoinsDictionary[data.Symbol].Candels_oneWeek, null, data.Data);
                        break;
                }
                
            }
            catch (Exception ex)
            {
                LogHelper.SendError(this, $"Data Collector, Exception.",ex);
                Console.WriteLine("12:" + ex.ToString());
            }
        }

        private void UpdateCandle(KlineInterval interval, string coin, List<IBinanceKline> klines, List<Quote> tbqv, IBinanceStreamKline data)
        {
            try
            {
                if (klines == null || klines.Any() == false)
                    return;
                if (data.Final)
                {
                    klines.RemoveAt(0);
                    if (tbqv != null && tbqv.Any())
                        tbqv.RemoveAt(0);

                }
                else
                {
                    int CandleCount = 500;
                    if (interval == KlineInterval.OneWeek)
                        CandleCount = 121;
                    if (interval<KlineInterval.ThirtyMinutes)
                        CandleCount = 1498;
                    if (klines.Count < CandleCount)
                    {
                        if(klines.Last().CloseTime<data.CloseTime)
                            klines.Add(data);

                        if (data.QuoteVolume == 0)
                            return;
                        var value = data.TakerBuyQuoteVolume / data.QuoteVolume;
                        if (tbqv != null && tbqv.Any())
                            tbqv.Add(new Quote { Open = value, High = value, Low = value, Date = data.CloseTime });
                    }
                }
                klines[klines.Count() - 1] = data;

                if (data.Final)
                {
                    Task.Run(() => { CalcMajorHighMajorLow(coin, interval); }).Wait();
                    if (coin == "BTCUSDT" && (interval == KlineInterval.OneMinute ||
                        interval == KlineInterval.FiveMinutes ||
                        interval == KlineInterval.FifteenMinutes))
                        Console.WriteLine($"final candle time={data.CloseTime} , intrerval ={interval}");
                    OnCandleReady(interval, coin);
                }
                if (tbqv == null)
                    return;
                int idx = tbqv.Count() - 1;
                if (idx > 0)
                {
                    if (tbqv[idx] == null)
                        return;
                    tbqv[idx].Close = data.TakerBuyQuoteVolume / data.QuoteVolume;
                    tbqv[idx].Date = data.CloseTime;
                    tbqv[idx].High = Math.Max(tbqv[idx].Close, tbqv[idx].High);
                    tbqv[idx].Low = Math.Min(tbqv[idx].Close, tbqv[idx].Low);
                    tbqv[idx].Volume = data.QuoteVolume;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("13: " + e.ToString());
            }

        }
        
        private void OnTimerTick(object state)
        {
            _ = GetDataAsync();
            LogHelper.SendInfo(this, "Data Collector, Timer Tick .");
        }

        private void OnConnectionTimerTick(object state)
        {
            dataConnectionCounter++;
            if (dataConnectionCounter < 61)
                return;

            dataConnectionCounter = 0;
            if (socketClientsList != null && socketClientsList.Any())
            {
                try
                {
                    socketClientsList.ForEach(x =>
                    {
                        try { x.UnsubscribeAllAsync(); x.Dispose(); }
                        catch { }
                    });
                }
                catch { }
            }
            socketClientsList.Clear();

            Console.WriteLine("Morteza: Reset connection!!!");

            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Esmaeili\\Markets");
            var _market = key.GetValue("Market");
            key.Close();

            if (_market.ToString() == "FUTURE")
            {
                foreach (var coinName in CoinNames)
                {
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, KlineInterval.OneMinute, OnNewUpdateCandle);

                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, KlineInterval.FiveMinutes, OnNewUpdateCandle);

                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, KlineInterval.FifteenMinutes, OnNewUpdateCandle);

                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, KlineInterval.ThirtyMinutes, OnNewUpdateCandle);

                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, KlineInterval.OneHour, OnNewUpdateCandle);

                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, KlineInterval.TwoHour, OnNewUpdateCandle);
                                        
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, KlineInterval.FourHour, OnNewUpdateCandle);

                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, KlineInterval.OneDay, OnNewUpdateCandle);

                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, KlineInterval.OneWeek, OnNewUpdateCandle);
                }
            }
            else
            {
                socketClient = new BinanceSocketClient();
                socketClientsList.Add(socketClient);
                foreach (var coinName in CoinNames)
                {
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName,
                        KlineInterval.OneMinute, OnNewUpdateCandle);
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName,
                        KlineInterval.FiveMinutes, OnNewUpdateCandle);
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName,
                        KlineInterval.FifteenMinutes, OnNewUpdateCandle);
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName,
                        KlineInterval.ThirtyMinutes, OnNewUpdateCandle);
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName,
                        KlineInterval.OneHour, OnNewUpdateCandle);
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName,
                        KlineInterval.TwoHour, OnNewUpdateCandle);
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName,
                        KlineInterval.FourHour, OnNewUpdateCandle);
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName,
                        KlineInterval.OneDay, OnNewUpdateCandle);
                    socketClient = new BinanceSocketClient();
                    socketClientsList.Add(socketClient);
                    socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName,
                        KlineInterval.OneWeek, OnNewUpdateCandle);
                }
            }
        }

        private void OnNewUpdateCandle(DataEvent<IBinanceStreamKlineData> data)
        {
                try
                {
                    dataConnectionCounter = 0;

                    LogHelper.SendInfo(this, "Data Collector, Updating candle information");
                    if (!CoinNames.Contains(data.Data.Symbol) || data.Data == null) return;
                    switch (data.Data.Data.Interval)
                    {
                        case KlineInterval.OneMinute:
                            UpdateCandle(data.Data.Data.Interval, data.Data.Symbol,CoinsDictionary[data.Data.Symbol].Candels_1min,
                                CoinsDictionary[data.Data.Symbol].Tbqv_1min, data.Data.Data);
                            break;
                        case KlineInterval.FiveMinutes:
                            UpdateCandle(data.Data.Data.Interval, data.Data.Symbol, CoinsDictionary[data.Data.Symbol].Candels_5min,
                                CoinsDictionary[data.Data.Symbol].Tbqv_5min, data.Data.Data);
                            break;
                        case KlineInterval.FifteenMinutes:
                            UpdateCandle(data.Data.Data.Interval, data.Data.Symbol, CoinsDictionary[data.Data.Symbol].Candels_15min,
                                CoinsDictionary[data.Data.Symbol].Tbqv_15min, data.Data.Data);
                            break;
                        case KlineInterval.ThirtyMinutes:
                            UpdateCandle(data.Data.Data.Interval, data.Data.Symbol, CoinsDictionary[data.Data.Symbol].Candels_30min,
                                null, data.Data.Data);
                            break;
                        case KlineInterval.OneHour:
                            UpdateCandle(data.Data.Data.Interval, data.Data.Symbol, CoinsDictionary[data.Data.Symbol].Candels_60min,
                                null, data.Data.Data);
                            break;
                        case KlineInterval.TwoHour:
                            UpdateCandle(data.Data.Data.Interval, data.Data.Symbol, CoinsDictionary[data.Data.Symbol].Candels_2hour,
                                null, data.Data.Data);
                            break;
                        case KlineInterval.FourHour:
                            UpdateCandle(data.Data.Data.Interval, data.Data.Symbol, CoinsDictionary[data.Data.Symbol].Candels_4hour,
                                null, data.Data.Data);
                            break;
                        case KlineInterval.OneDay:
                            UpdateCandle(data.Data.Data.Interval, data.Data.Symbol, CoinsDictionary[data.Data.Symbol].Candels_oneDay,
                                null, data.Data.Data);
                            break;
                        case KlineInterval.OneWeek:
                            UpdateCandle(data.Data.Data.Interval, data.Data.Symbol, CoinsDictionary[data.Data.Symbol].Candels_oneWeek,
                                null, data.Data.Data);
                            break;
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.SendError(this, $"Data Collector, Exception.", ex);
                    Console.WriteLine("12:" + ex.ToString());
                }
            
        }

        private void CalcMajorHighMajorLow(string coinName, KlineInterval interval)
        {
            var macdParams = new MacdParams(12, 26, 9);
            if (CoinsDictionary.Keys.Contains(coinName))
            {
                List<WeightedValue> majorHighs = new List<WeightedValue>();
                List<WeightedValue> majorLows = new List<WeightedValue>();
                List<LocalMinMax> localMins = new List<LocalMinMax>();
                List<LocalMinMax> localMaxes = new List<LocalMinMax>();
                switch (interval)
                {
                    case KlineInterval.OneMinute:
                        {
                            var macdResult = BinanceHelper.MacdCalculationForStepDiagram(CoinsDictionary[coinName].Candels_1min, macdParams,
                              ref majorHighs, ref majorLows);
                            if (macdResult != null)
                            {
                                CoinsDictionary[coinName].MacdResult1Min = macdResult;
                                CoinsDictionary[coinName].MajorHighPrice1Min = majorHighs;
                                CoinsDictionary[coinName].MajorLowPrice1Min = majorLows;
                                CoinsDictionary[coinName].MacdLocalMin1Min = localMins;
                                CoinsDictionary[coinName].MacdLocalMax1Min = localMaxes;
                            }
                        }
                        break;
                    case KlineInterval.FiveMinutes:
                        {
                            var macdResult = BinanceHelper.MacdCalculationForStepDiagram(CoinsDictionary[coinName].Candels_5min, macdParams,
                              ref majorHighs, ref majorLows);
                            if (macdResult != null)
                            {
                                CoinsDictionary[coinName].MajorHighPrice5Min = majorHighs;
                                CoinsDictionary[coinName].MajorLowPrice5Min = majorLows;
                                CoinsDictionary[coinName].MacdLocalMin5Min = localMins;
                                CoinsDictionary[coinName].MacdLocalMax5Min = localMaxes;
                            }
                        }
                        break;
                    case KlineInterval.FifteenMinutes:
                        {
                            var macdResult = BinanceHelper.MacdCalculationForStepDiagram(CoinsDictionary[coinName].Candels_15min, macdParams,
                              ref majorHighs, ref majorLows);
                            if (macdResult != null)
                            {
                                CoinsDictionary[coinName].MajorHighPrice15Min = majorHighs;
                                CoinsDictionary[coinName].MajorLowPrice15Min = majorLows;
                                CoinsDictionary[coinName].MacdLocalMin15Min = localMins;
                                CoinsDictionary[coinName].MacdLocalMax15Min = localMaxes;

                            }
                        }
                        break;
                    case KlineInterval.ThirtyMinutes:
                        {
                            var macdResult = BinanceHelper.MacdCalculationForStepDiagram(CoinsDictionary[coinName].Candels_30min, macdParams,
                              ref majorHighs, ref majorLows);
                            if (macdResult != null)
                            {
                                CoinsDictionary[coinName].MajorHighPrice30Min = majorHighs;
                                CoinsDictionary[coinName].MajorLowPrice30Min = majorLows;
                                CoinsDictionary[coinName].MacdLocalMin30Min = localMins;
                                CoinsDictionary[coinName].MacdLocalMax30Min = localMaxes;

                            }
                        }
                        break;
                    case KlineInterval.OneHour:
                        {
                            var macdResult = BinanceHelper.MacdCalculationForStepDiagram(CoinsDictionary[coinName].Candels_60min, macdParams,
                              ref majorHighs, ref majorLows);
                            if (macdResult != null)
                            {
                                CoinsDictionary[coinName].MajorHighPrice60Min = majorHighs;
                                CoinsDictionary[coinName].MajorLowPrice60Min = majorLows;
                                CoinsDictionary[coinName].MacdLocalMin60Min = localMins;
                                CoinsDictionary[coinName].MacdLocalMax60Min = localMaxes;

                            }
                        }
                        break;
                    case KlineInterval.TwoHour:
                        {
                            var macdResult = BinanceHelper.MacdCalculationForStepDiagram(CoinsDictionary[coinName].Candels_2hour, macdParams,
                              ref majorHighs, ref majorLows);
                            if (macdResult != null)
                            {
                                CoinsDictionary[coinName].MajorHighPrice2hour = majorHighs;
                                CoinsDictionary[coinName].MajorLowPrice2hour = majorLows;
                                CoinsDictionary[coinName].MacdLocalMin2hour = localMins;
                                CoinsDictionary[coinName].MacdLocalMax2hour = localMaxes;

                            }
                        }
                        break;
                    case KlineInterval.FourHour:
                        {
                            var macdResult = BinanceHelper.MacdCalculationForStepDiagram(CoinsDictionary[coinName].Candels_4hour, macdParams,
                              ref majorHighs, ref majorLows);
                            if (macdResult != null)
                            {
                                CoinsDictionary[coinName].MajorHighPrice4hour = majorHighs;
                                CoinsDictionary[coinName].MajorLowPrice4hour = majorLows;
                                CoinsDictionary[coinName].MacdLocalMin4hour = localMins;
                                CoinsDictionary[coinName].MacdLocalMax4hour = localMaxes;

                            }
                        }
                        break;
                    case KlineInterval.OneDay:
                        {
                            var macdResult = BinanceHelper.MacdCalculationForStepDiagram(CoinsDictionary[coinName].Candels_oneDay, macdParams,
                              ref majorHighs, ref majorLows);
                            if (macdResult != null)
                            {
                                CoinsDictionary[coinName].MajorHighPrice1Day = majorHighs;
                                CoinsDictionary[coinName].MajorLowPrice1Day = majorLows;
                                CoinsDictionary[coinName].MacdLocalMin1Day = localMins;
                                CoinsDictionary[coinName].MacdLocalMax1Day = localMaxes;
                            }
                        }
                        break;
                    case KlineInterval.OneWeek:
                        {
                            var macdResult = BinanceHelper.MacdCalculationForStepDiagram(CoinsDictionary[coinName].Candels_oneWeek, macdParams,
                              ref majorHighs, ref majorLows);
                            if (macdResult != null)
                            {
                                CoinsDictionary[coinName].MajorHighPrice1Week = majorHighs;
                                CoinsDictionary[coinName].MajorLowPrice1Week = majorLows;
                                CoinsDictionary[coinName].MacdLocalMin1Week = localMins;
                                CoinsDictionary[coinName].MacdLocalMax1Week = localMaxes;
                            }
                        }
                        break;
                }
            }
        }

        public async Task<List<IBinanceKline>> GetKLinesAsync(KlineInterval klineInterval, string coinName)
        {
            try
            {
                if (marketType == MarketType.SPOT)
                {
                    using (var client = new BinanceRestClient())
                    {
                        var kandle1 = await client.SpotApi.ExchangeData.GetKlinesAsync(coinName, klineInterval);
                        if (kandle1.Data == null)
                            return null;
                        if (klineInterval < KlineInterval.ThirtyMinutes )
                        {
                            DateTime startTime = kandle1.Data.First().CloseTime;
                            DateTime endTime = kandle1.Data.Last().CloseTime;
                            var interval = endTime - startTime;
                            DateTime newEndTime = startTime;
                            DateTime newStartTime = newEndTime - interval;

                            var kandle2 = await client.SpotApi.ExchangeData.GetKlinesAsync(coinName, klineInterval, newStartTime, newEndTime);
                            if (kandle2.Data == null)
                                return null;
                            newEndTime = newStartTime;
                            newStartTime = newEndTime - interval;
                            List<IBinanceKline> candles = new List<IBinanceKline>();
                            
                            var kandle3 = await client.SpotApi.ExchangeData.GetKlinesAsync(coinName, klineInterval, newStartTime, newEndTime);
                            if (kandle3.Data == null)
                                return null;
                                
                            candles.AddRange( kandle3.Data.ToList());
                            
                            candles.AddRange(kandle2.Data.ToList());
                            candles.AddRange(kandle1.Data.ToList());
                            socketClient = new BinanceSocketClient();
                            socketClientsList.Add(socketClient);
                            _ = socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName, klineInterval, OnNewUpdateCandle);
                            return candles;
                        }
                        else
                        {
                            List<IBinanceKline> candles = kandle1.Data.ToList();
                            socketClient = new BinanceSocketClient();
                            socketClientsList.Add(socketClient);
                            _ = socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(coinName, klineInterval, OnNewUpdateCandle);
                            return candles;
                        }

                    }
                }
                else
                {
                    using (var client = new BinanceRestClient())
                    {
                        var kandle1 = await client.UsdFuturesApi.ExchangeData.GetKlinesAsync(coinName, klineInterval);
                        if (kandle1.Data == null)
                            return null;
                        if (klineInterval < KlineInterval.ThirtyMinutes )
                        {
                            DateTime startTime = kandle1.Data.First().CloseTime;
                            DateTime endTime = kandle1.Data.Last().CloseTime;
                            var interval = endTime - startTime;
                            DateTime newEndTime = startTime;
                            DateTime newStartTime = newEndTime - interval;
                            
                            var kandle2 = await client.UsdFuturesApi.ExchangeData.GetKlinesAsync(coinName, klineInterval, newStartTime, newEndTime);
                            if (kandle2.Data == null)
                                return null;
                            newEndTime = newStartTime;
                            newStartTime = newEndTime - interval;
                            List<IBinanceKline> candles = new List<IBinanceKline>();
                            
                            var kandle3 = await client.UsdFuturesApi.ExchangeData.GetKlinesAsync(coinName, klineInterval, newStartTime, newEndTime);
                            if (kandle3.Data == null)
                                return null;
                            candles.AddRange(kandle3.Data.ToList());
                            
                            candles.AddRange(kandle2.Data.ToList());
                            candles.AddRange(kandle1.Data.ToList());
                            socketClient = new BinanceSocketClient();
                            socketClientsList.Add(socketClient);
                            _ = socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, klineInterval, OnNewUpdateCandle);
                            return candles;
                        }
                        else
                        {
                            List<IBinanceKline> candles = kandle1.Data.ToList();
                            socketClient = new BinanceSocketClient();
                            socketClientsList.Add(socketClient);
                            _ = socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(coinName, klineInterval, OnNewUpdateCandle);
                            return candles;
                        }
                        //return kandle.Data.Skip(kandle.Data.Count() - 150).ToList();

                    }
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
                Console.WriteLine($"ex 581: {ex.ToString()}");
                return null;
            }
        }        

        private async Task GetDataAsync()
        {
            try
            {
                if (marketType == MarketType.SPOT)
                {
                    List<Task> taskList = new List<Task>();
                    foreach (var coinName in CoinNames)
                    {
                        taskList.Add(Task.Factory.StartNew(async () =>
                        {
                            try
                            {
                                using (var client = new BinanceRestClient())
                                {
                                    var tmpBinanceModel = CoinsDictionary[coinName];
                                    var systemTime = await client.SpotApi.ExchangeData.GetServerTimeAsync();
                                    var currentPrice = await client.SpotApi.ExchangeData.GetPriceAsync(coinName);
                                    if (currentPrice.Data == null)
                                        return;
                                    var price = new WeightedValue { Value = currentPrice.Data.Price, Time = systemTime.Data };
                                    tmpBinanceModel.PriceList.Add(price);
                                    WebCallResult<Binance.Net.Objects.Models.Spot.BinanceOrderBook> data3 =
                                        await client.SpotApi.ExchangeData.GetOrderBookAsync(coinName);
                                    if (data3.ResponseStatusCode == System.Net.HttpStatusCode.OK && data3.Data != null)
                                    {
                                            //Buy
                                            tmpBinanceModel.BuyerList = data3.Data.Asks.Select(d => new MarketModel { Price = d.Price, Quantity = d.Quantity }).ToList();
                                            //sell

                                            tmpBinanceModel.SellerList = data3.Data.Bids.Select(d => new MarketModel { Price = d.Price, Quantity = d.Quantity }).ToList();

                                        decimal y50 = 0;
                                        if (price.Value > 0)
                                            y50 = -((((tmpBinanceModel.AskWightedPrice50.Last().Value + tmpBinanceModel.BidsWightedPrice50.Last().Value) / 2) - price.Value) / price.Value);
                                        if (tmpBinanceModel.MP50.Any())
                                        {
                                            tmpBinanceModel.MP50.Add(new WeightedValue { Value = y50 + tmpBinanceModel.MP50.Last().Value, Time = systemTime.Data });
                                        }
                                        else
                                            tmpBinanceModel.MP50.Add(new WeightedValue { Value = y50, Time = systemTime.Data });
                                    }


                                    WebCallResult<IEnumerable<Binance.Net.Interfaces.IBinanceRecentTrade>> data4 = await client.SpotApi.ExchangeData.GetRecentTradesAsync(coinName);
                                    if (data4.ResponseStatusCode == System.Net.HttpStatusCode.OK && data4.Data != null)
                                    {

                                        var tradeList = data4.Data.Select(d => new TradeModel { Price = d.Price, Time = d.TradeTime, Quantity = d.BaseQuantity }).ToList();
                                        tmpBinanceModel.TradeList = tradeList;

                                    }
                                    CalculateDMH_DML(tmpBinanceModel);

                                    CoinsDictionary.AddOrUpdate(coinName, tmpBinanceModel, (k, v) => tmpBinanceModel);
                                    client.Dispose();
                                }
                            }
                            catch (Exception ex) { }
                        }));
                    }
                    Task.WaitAll(taskList.ToArray());
                }
                else
                {
                    List<Task> taskList = new List<Task>();
                    foreach (var coinName in CoinNames)
                    {
                        taskList.Add(Task.Factory.StartNew(async () =>
                        {
                            try
                            {
                                using (var client = new BinanceRestClient())
                                {
                                    var tmpBinanceModel = CoinsDictionary[coinName];
                                    var systemTime = await client.UsdFuturesApi.ExchangeData.GetServerTimeAsync();

                                    //var currentPrice = await client.Spot.Market.GetCurrentAvgPriceAsync(coinName);
                                    var currentPrice = await client.UsdFuturesApi.ExchangeData.GetPriceAsync(coinName);
                                    if (currentPrice.Data == null)
                                        return;
                                    var price = new WeightedValue { Value = currentPrice.Data.Price, Time = systemTime.Data };
                                    tmpBinanceModel.PriceList.Add(price);
                                    WebCallResult<Binance.Net.Objects.Models.Futures.BinanceFuturesOrderBook> data3 =
                                       await client.UsdFuturesApi.ExchangeData.GetOrderBookAsync(coinName);
                                    if (data3.ResponseStatusCode == System.Net.HttpStatusCode.OK && data3.Data != null)
                                    {
                                        //Buy
                                        tmpBinanceModel.BuyerList = data3.Data.Asks.Select(d => new MarketModel { Price = d.Price, Quantity = d.Quantity }).ToList();
                                        //sell

                                        tmpBinanceModel.SellerList = data3.Data.Bids.Select(d => new MarketModel { Price = d.Price, Quantity = d.Quantity }).ToList();

                                        decimal y50 = 0;
                                        if (price.Value > 0)
                                            y50 = -((((tmpBinanceModel.AskWightedPrice50.Last().Value + tmpBinanceModel.BidsWightedPrice50.Last().Value) / 2) - price.Value) / price.Value);
                                        if (tmpBinanceModel.MP50.Any())
                                        {
                                            tmpBinanceModel.MP50.Add(new WeightedValue { Value = y50 + tmpBinanceModel.MP50.Last().Value, Time = systemTime.Data });
                                        }
                                        else
                                            tmpBinanceModel.MP50.Add(new WeightedValue { Value = y50, Time = systemTime.Data });
                                    }

                                    WebCallResult<IEnumerable<IBinanceRecentTrade>> data4 =
                                        await client.UsdFuturesApi.ExchangeData.GetRecentTradesAsync(coinName);
                                    if (data4.ResponseStatusCode == System.Net.HttpStatusCode.OK && data4.Data != null)
                                    {

                                        var tradeList = data4.Data.Select(d => new TradeModel { Price = d.Price, Time = d.TradeTime, Quantity = d.BaseQuantity }).ToList();
                                        tmpBinanceModel.TradeList = tradeList;

                                    }

                                    CoinsDictionary.AddOrUpdate(coinName, tmpBinanceModel, (k, v) => tmpBinanceModel);
                                    client.Dispose();
                                }
                            }
                            catch (Exception ex) { }
                        }));
                    }
                    Task.WaitAll(taskList.ToArray());
                }
                OnDataReady();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ex 698: {ex.ToString()}");
                ex.ToString();
            }
        }

        private void CalculateDMH_DML(BinanceModel binanceModel)
        {
            UpdateCoinDMH_DML(binanceModel, KlineInterval.OneMinute, binanceModel.MajorHighPrice1Min, binanceModel.MajorLowPrice1Min);
            UpdateCoinDMH_DML(binanceModel, KlineInterval.FiveMinutes, binanceModel.MajorHighPrice5Min, binanceModel.MajorLowPrice5Min);
            UpdateCoinDMH_DML(binanceModel, KlineInterval.FifteenMinutes, binanceModel.MajorHighPrice15Min, binanceModel.MajorLowPrice15Min);
            UpdateCoinDMH_DML(binanceModel, KlineInterval.ThirtyMinutes, binanceModel.MajorHighPrice30Min, binanceModel.MajorLowPrice30Min);
            UpdateCoinDMH_DML(binanceModel, KlineInterval.OneHour, binanceModel.MajorHighPrice60Min, binanceModel.MajorLowPrice60Min);
            UpdateCoinDMH_DML(binanceModel, KlineInterval.TwoHour, binanceModel.MajorHighPrice2hour, binanceModel.MajorLowPrice2hour);
            UpdateCoinDMH_DML(binanceModel, KlineInterval.FourHour, binanceModel.MajorHighPrice4hour, binanceModel.MajorLowPrice4hour);
            UpdateCoinDMH_DML(binanceModel, KlineInterval.OneDay, binanceModel.MajorHighPrice1Day, binanceModel.MajorLowPrice1Day);
            UpdateCoinDMH_DML(binanceModel, KlineInterval.OneWeek, binanceModel.MajorHighPrice1Day, binanceModel.MajorLowPrice1Week);
        }

        private void UpdateCoinDMH_DML(BinanceModel binanceModel, KlineInterval interval,List<WeightedValue> majorHighPrice, List<WeightedValue> majorLowPrice)
        {
            try
            {
                // decimal price = binanceModel.CurrentPrice;
                if (binanceModel.Candels_1min == null || binanceModel.Candels_1min.Any() == false)
                    return;
                decimal price = binanceModel.Candels_1min.Last().ClosePrice;

                if (majorHighPrice != null && majorHighPrice.Any())
                {
                    var mh = majorHighPrice.First().Value;
                    if (binanceModel.DMHDic.ContainsKey(interval))
                        binanceModel.DMHDic[interval] = decimal.Round(( price - mh) *10000/ mh)/100;
                    else
                        binanceModel.DMHDic.Add(interval, decimal.Round(( price - mh)*10000 / mh)/100);
                }
                else
                {
                    if (binanceModel.DMHDic.ContainsKey(interval)==false)
                       binanceModel.DMHDic.Add(interval, 0);
                }
                if (majorLowPrice != null && majorLowPrice.Any())
                {
                    var ml = majorLowPrice.First().Value;
                    if (binanceModel.DMLDic.ContainsKey(interval))
                        binanceModel.DMLDic[interval] = decimal.Round(( price - ml)*10000 / ml)/100;
                    else
                        binanceModel.DMLDic.Add(interval, decimal.Round(( price - ml)*10000 / ml)/100);
                }
                else
                {
                    if (binanceModel.DMLDic.ContainsKey(interval) == false)
                        binanceModel.DMLDic.Add(interval, 0);
                }
            }
            catch(Exception ex) {
                Console.WriteLine($"ex 751: {ex.ToString()}");
            }
        }

        public BinanceModel GetBinance(string coinName)
        {
            if (CoinsDictionary.Keys.Contains(coinName))
                return CoinsDictionary[coinName];
            else
                return null;
        }
    }
}
