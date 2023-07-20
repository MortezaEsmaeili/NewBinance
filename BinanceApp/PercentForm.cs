using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.Business;
using BinanceApp.DataModel;
using CryptoExchange.Net.CommonObjects;
using Skender.Stock.Indicators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;

namespace BinanceApp
{
    public partial class PercentForm : Telerik.WinControls.UI.RadForm
    {
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();

        LineSeries ZeroLine = new LineSeries();

        List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(),
            KlineInterval.FiveMinutes.ToString(), KlineInterval.FifteenMinutes.ToString(),
            KlineInterval.ThirtyMinutes.ToString(), KlineInterval.OneHour.ToString(),
            KlineInterval.FourHour.ToString(), KlineInterval.OneDay.ToString()};

        Dictionary<string,decimal> priceDic = new Dictionary<string,decimal>();
        Dictionary<string,decimal> AllCoinBuyRR = new Dictionary<string,decimal>();
        Dictionary<string,decimal> AllCoinSellRR= new Dictionary<string,decimal>();
        private BinanceModel binanceModel;

        private string coinName = string.Empty;

        private delegate void UpdateFormDelegate(BinanceModel binanceModel);
        private delegate void UpdateAllCoinListDelegate(string coinName);

        public PercentForm()
        {
            InitializeComponent();
        }

        private void PercentForm_Load(object sender, EventArgs e)
        {
            WireEvents();
            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;

            coinName = this.radDropDownList1.SelectedItem.Text;
            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;

            initAxis();
            InitSeries();
        }
        protected void WireEvents()
        {
            this.radDropDownList1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList1_SelectedIndexChanged);
        }

        void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (this.radDropDownList1.SelectedItem != null)
            {
                coinName = this.radDropDownList1.SelectedItem.Text;
                var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                if (coinInfo == null) return;
                binanceModel = coinInfo;
                UpdateData(binanceModel);
                //setToInitialState();
            }
        }

        private void PercentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;
        }
        private void OnDataReadyEvent()
        {
            if (binanceModel == null)
            {
                if (!string.IsNullOrWhiteSpace(coinName))
                {
                    var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                    if (coinInfo == null) return;
                    binanceModel = coinInfo;
                }
                else
                    return;
            }
            Action act = new Action(() => {
                if (binanceModel.PriceList.Any())
                {
                    price_LB.Text = binanceModel.CurrentPrice.ToString();

                    UpdateData(binanceModel);
                }
            });
            if (InvokeRequired)
            {
                this.Invoke(act);
            }
            else
            {
                act();
            }
        }
        private void OnCandleReadyEvent(KlineInterval interval, string coin)
        {
            try
            {
                if (InvokeRequired)
                {
                    var d = new UpdateAllCoinListDelegate(UpdateAllCoinList);
                    this.Invoke(d, coin);
                }
                else
                    UpdateAllCoinList(coin);

                if (coin != coinName) return;
                if (binanceModel == null)
                {
                    if (!string.IsNullOrWhiteSpace(coinName))
                    {
                        var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                        if (coinInfo == null) return;
                        binanceModel = coinInfo;

                    }
                    else
                        return;
                }

                if (InvokeRequired)
                {
                    var d = new UpdateFormDelegate(UpdateData);
                    this.Invoke(d, binanceModel);
                }
                else
                    UpdateData(binanceModel);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void UpdateData(BinanceModel binanceModel)
        {
            try
            {
                price_LB.Text = binanceModel.CurrentPrice.ToString();
                decimal price = binanceModel.CurrentPrice;

                var startTime = binanceModel.MacdResult1Min.First().Date;
                var endTime = binanceModel.MacdResult1Min.Last().Date;
                decimal mh1H = 0, ml1H = 0, mh2H = 0, ml2H = 0, mh4H = 0, ml4H = 0;
                decimal mh1D = 0, ml1D = 0, mh1W = 0, ml1W = 0;
                decimal ma1H = 0, ma2H = 0, ma4H = 0, ma1D = 0, ma1W = 0;

                _ = GetMarginPoints(binanceModel.MajorLowPrice60Min, startTime, endTime, out ml1H);
                _ = GetMarginPoints(binanceModel.MajorHighPrice60Min, startTime, endTime, out mh1H);
                ma1H = GetMovingAverage(binanceModel.Candels_60min);
                LB_ML1H.Text = $"ML 1H : {ml1H}";
                string temp = ((price - ml1H) * 100 / ml1H).ToString("#.##");
                LB_ML1HP.Text = $"ML 1H P : {temp} %";
                LB_MH1H.Text = $"MH 1H : {ml1H}";
                temp = ((price - mh1H) * 100 / mh1H).ToString("#.##");
                LB_MH1HP.Text = $"MH 1H P : {temp} %";
                if(ma1H > 0)
                {
                    LB_MA_1H.Text = $" MA 1H : {ma1H}";
                    temp = ((price - ma1H) * 100 / ma1H).ToString("#.##");
                    LB_MA_1HP.Text = $"MA 1H P : {temp} %";
                }

                _ = GetMarginPoints(binanceModel.MajorLowPrice2hour, startTime, endTime, out ml2H);
                _ = GetMarginPoints(binanceModel.MajorHighPrice60Min, startTime, endTime, out mh2H);
                ma2H = GetMovingAverage(binanceModel.Candels_2hour);
                LB_ML2H.Text = $"ML 2H : {ml2H}";
                temp = ((price - ml2H) * 100 / ml2H).ToString("#.##");
                LB_ML2HP.Text = $"ML 2H P : {temp} %";
                LB_MH2H.Text = $"MH 2H : {mh2H}";
                temp = ((price - mh2H) * 100 / mh2H).ToString("#.##");
                LB_MH2HP.Text = $"MH 2H P : {temp} %";
                if (ma2H > 0)
                {
                    LB_MA_2H.Text = $" MA 2H : {ma2H}";
                    temp = ((price - ma2H) * 100 / ma2H).ToString("#.##");
                    LB_MA_2HP.Text = $"MA 2H P : {temp} %";
                }

                _ = GetMarginPoints(binanceModel.MajorLowPrice4hour, startTime, endTime, out ml4H);
                _ = GetMarginPoints(binanceModel.MajorHighPrice4hour, startTime, endTime, out mh4H);
                ma4H = GetMovingAverage(binanceModel.Candels_4hour);
                LB_ML4H.Text = $"ML 4H : {ml4H}";
                temp = ((price - ml4H) * 100 / ml4H).ToString("#.##");
                LB_ML4HP.Text = $"ML 4H P : {temp} %";
                LB_MH4H.Text = $"MH 4H : {mh4H}";
                temp = ((price - mh4H) * 100 / mh4H).ToString("#.##");
                LB_MH4HP.Text = $"MH 4H P : {temp} %";
                if (ma4H > 0)
                {
                    LB_MA_4H.Text = $" MA 4H : {ma4H}";
                    temp = ((price - ma4H) * 100 / ma4H).ToString("#.##");
                    LB_MA_4HP.Text = $"MA 4H P : {temp} %";
                }

                _ = GetMarginPoints(binanceModel.MajorLowPrice1Day, startTime, endTime, out ml1D);
                _ = GetMarginPoints(binanceModel.MajorHighPrice1Day, startTime, endTime, out mh1D);
                ma1D = GetMovingAverage(binanceModel.Candels_oneDay);
                LB_ML1D.Text = $"ML 1D : {ml1D}";
                temp = ((price - ml1D) * 100 / ml1D).ToString("#.##");
                LB_ML1DP.Text = $"ML 1D P : {temp} %";
                LB_MH1D.Text = $"MH 1D : {mh1D}";
                temp = ((price - mh1D) * 100 / mh1D).ToString("#.##");
                LB_MH1DP.Text = $"MH 1D P : {temp} %";
                if (ma1D > 0)
                {
                    LB_MA_1D.Text = $" MA 1D : {ma1D}";
                    temp = ((price - ma1D) * 100 / ma1D).ToString("#.##");
                    LB_MA_1DP.Text = $"MA 1D P : {temp} %";
                }

                _ = GetMarginPoints(binanceModel.MajorLowPrice1Week, startTime, endTime, out ml1W);
                _ = GetMarginPoints(binanceModel.MajorHighPrice1Week, startTime, endTime, out mh1W);
                ma1W = GetMovingAverage(binanceModel.Candels_oneWeek);
                LB_ML1W.Text = $"ML 1W : {ml1W}";
                temp = ((price - ml1W) * 100 / ml1W).ToString("#.##");
                LB_ML1WP.Text = $"ML 1W P : {temp} %";
                LB_MH1W.Text = $"MH 1W : {mh1W}";
                temp = ((price - mh1W) * 100 / mh1W).ToString("#.##");
                LB_MH1WP.Text = $"MH 1W P : {temp} %";
                if (ma1W > 0)
                {
                    LB_MA_1W.Text = $" MA 1W : {ma1W}";
                    temp = ((price - ma1W) * 100 / ma1W).ToString("#.##");
                    LB_MA_1WP.Text = $"MA 1W P : {temp} %";
                }

                priceDic.Clear();
                priceDic.Add("PRICE",price);
                priceDic.Add("ML 1H",ml1H);
                priceDic.Add("MH 1H",mh1H);
                priceDic.Add("ML 2H",ml2H);
                priceDic.Add("MH 2H",mh2H);
                priceDic.Add("ML 4H",ml4H);
                priceDic.Add("MH 4H",mh4H);
                priceDic.Add("ML 1D",ml1D);
                priceDic.Add("MH 1D",mh1D);
                priceDic.Add("ML 1W",ml1W);
                priceDic.Add("MH 1W",mh1W);

                var sortedDic = from entry in priceDic orderby entry.Value descending select entry; //ascending

                LIST_BOX_SORTED.Items.Clear();

                int iResistanceIndex = 0, iSupportIndex = 0;
                for(int i=0; i<sortedDic.Count(); i++)
                {
                    LIST_BOX_SORTED.Items.Add($"{sortedDic.ElementAt(i).Key}:   {sortedDic.ElementAt(i).Value}");
                    if(sortedDic.ElementAt(i).Key == "PRICE")
                    {
                        iResistanceIndex = (i-1) < 0 ? 0 : i-1;
                        iSupportIndex = i == sortedDic.Count() - 1 ? i : i+1;
                    }
                }
                decimal supportPrice = sortedDic.ElementAt(iSupportIndex).Value;
                decimal resistancePrice = sortedDic.ElementAt(iResistanceIndex).Value;

                decimal positionMargin = decimal.Parse(TB_PositionMargin.Text);
                decimal stopLossMargin = decimal.Parse(TB_StopLossPercentage.Text);
                positionMargin = positionMargin / 100;
                stopLossMargin = stopLossMargin / 100;


                decimal buyOpenPosition = supportPrice * (1 + positionMargin);
                decimal sellOpenPosition = resistancePrice * (1 - positionMargin);

                TB_BuyPositionOpen.Text = buyOpenPosition.ToString();
                TB_SellPositionPrice.Text=sellOpenPosition.ToString();

                decimal buyStopLoss = supportPrice * (1 - stopLossMargin);
                decimal sellStopLoss= resistancePrice * (1 + stopLossMargin);

                TB_BuyStopLoss.Text = buyStopLoss.ToString();
                TB_SellStopLoss.Text = sellStopLoss.ToString();

                decimal buyGain = -1*((buyOpenPosition / sellOpenPosition) - 1 ) *100;
                decimal sellGain = ((sellOpenPosition / buyOpenPosition) - 1) *100;

                TB_BuyGain.Text = buyGain.ToString("#.##");
                TB_SellGain.Text = sellGain.ToString("#.##");

                decimal buyLoss = ((buyOpenPosition / buyStopLoss) - 1)*100;
                decimal sellLoss= ((sellStopLoss/sellOpenPosition) - 1 ) *100;

                TB_BuyLoss.Text = buyLoss.ToString("#.##");
                TB_SellLoss.Text = sellLoss.ToString("#.##");

                decimal buyRiskReward = buyGain / buyLoss;
                decimal sellRiskReward = sellGain / sellLoss;

                TB_BuyRR.Text = buyRiskReward.ToString("#.##");
                TB_SellRR.Text = sellRiskReward.ToString("#.##");

                UpdateFinancialIndicator(coinName);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void UpdateAllCoinList(string coinName)
        {
            try
            {
                if(!radDropDownList1.Items.Contains(coinName)) { return; }
                var binanceModel = BinanceDataCollector.Instance.GetBinance(coinName);

                //price_LB.Text = binanceModel.CurrentPrice.ToString();
                decimal price = binanceModel.CurrentPrice;

                var startTime = binanceModel.MacdResult1Min.First().Date;
                var endTime = binanceModel.MacdResult1Min.Last().Date;
                decimal mh1H = 0, ml1H = 0, mh2H = 0, ml2H = 0, mh4H = 0, ml4H = 0;
                decimal mh1D = 0, ml1D = 0, mh1W = 0, ml1W = 0;
                decimal ma1H = 0, ma2H = 0, ma4H = 0, ma1D = 0, ma1W = 0;

                _ = GetMarginPoints(binanceModel.MajorLowPrice60Min, startTime, endTime, out ml1H);
                _ = GetMarginPoints(binanceModel.MajorHighPrice60Min, startTime, endTime, out mh1H);
                ma1H = GetMovingAverage(binanceModel.Candels_60min);
                //LB_ML1H.Text = $"ML 1H : {ml1H}";
                string temp = ((price - ml1H) * 100 / ml1H).ToString("#.##");
               // LB_ML1HP.Text = $"ML 1H P : {temp} %";
                //LB_MH1H.Text = $"MH 1H : {ml1H}";
                temp = ((price - mh1H) * 100 / mh1H).ToString("#.##");
               // LB_MH1HP.Text = $"MH 1H P : {temp} %";
               /* if (ma1H > 0)
                {
                    LB_MA_1H.Text = $" MA 1H : {ma1H}";
                    temp = ((price - ma1H) * 100 / ma1H).ToString("#.##");
                    LB_MA_1HP.Text = $"MA 1H P : {temp} %";
                }*/

                _ = GetMarginPoints(binanceModel.MajorLowPrice2hour, startTime, endTime, out ml2H);
                _ = GetMarginPoints(binanceModel.MajorHighPrice60Min, startTime, endTime, out mh2H);
                ma2H = GetMovingAverage(binanceModel.Candels_2hour);
              //  LB_ML2H.Text = $"ML 2H : {ml2H}";
                temp = ((price - ml2H) * 100 / ml2H).ToString("#.##");
              //  LB_ML2HP.Text = $"ML 2H P : {temp} %";
              //  LB_MH2H.Text = $"MH 2H : {mh2H}";
                temp = ((price - mh2H) * 100 / mh2H).ToString("#.##");
             //   LB_MH2HP.Text = $"MH 2H P : {temp} %";
             /*   if (ma2H > 0)
                {
                    LB_MA_2H.Text = $" MA 2H : {ma2H}";
                    temp = ((price - ma2H) * 100 / ma2H).ToString("#.##");
                    LB_MA_2HP.Text = $"MA 2H P : {temp} %";
                }*/

                _ = GetMarginPoints(binanceModel.MajorLowPrice4hour, startTime, endTime, out ml4H);
                _ = GetMarginPoints(binanceModel.MajorHighPrice4hour, startTime, endTime, out mh4H);
                ma4H = GetMovingAverage(binanceModel.Candels_4hour);
             //   LB_ML4H.Text = $"ML 4H : {ml4H}";
                temp = ((price - ml4H) * 100 / ml4H).ToString("#.##");
             //   LB_ML4HP.Text = $"ML 4H P : {temp} %";
             //   LB_MH4H.Text = $"MH 4H : {mh4H}";
                temp = ((price - mh4H) * 100 / mh4H).ToString("#.##");
            //    LB_MH4HP.Text = $"MH 4H P : {temp} %";
            /*    if (ma4H > 0)
                {
                    LB_MA_4H.Text = $" MA 4H : {ma4H}";
                    temp = ((price - ma4H) * 100 / ma4H).ToString("#.##");
                    LB_MA_4HP.Text = $"MA 4H P : {temp} %";
                }*/

                _ = GetMarginPoints(binanceModel.MajorLowPrice1Day, startTime, endTime, out ml1D);
                _ = GetMarginPoints(binanceModel.MajorHighPrice1Day, startTime, endTime, out mh1D);
                ma1D = GetMovingAverage(binanceModel.Candels_oneDay);
            /*    LB_ML1D.Text = $"ML 1D : {ml1D}";
                temp = ((price - ml1D) * 100 / ml1D).ToString("#.##");
                LB_ML1DP.Text = $"ML 1D P : {temp} %";
                LB_MH1D.Text = $"MH 1D : {mh1D}";
                temp = ((price - mh1D) * 100 / mh1D).ToString("#.##");
                LB_MH1DP.Text = $"MH 1D P : {temp} %";
                if (ma1D > 0)
                {
                    LB_MA_1D.Text = $" MA 1D : {ma1D}";
                    temp = ((price - ma1D) * 100 / ma1D).ToString("#.##");
                    LB_MA_1DP.Text = $"MA 1D P : {temp} %";
                }*/

                _ = GetMarginPoints(binanceModel.MajorLowPrice1Week, startTime, endTime, out ml1W);
                _ = GetMarginPoints(binanceModel.MajorHighPrice1Week, startTime, endTime, out mh1W);
                ma1W = GetMovingAverage(binanceModel.Candels_oneWeek);
             /*   LB_ML1W.Text = $"ML 1W : {ml1W}";
                temp = ((price - ml1W) * 100 / ml1W).ToString("#.##");
                LB_ML1WP.Text = $"ML 1W P : {temp} %";
                LB_MH1W.Text = $"MH 1W : {mh1W}";
                temp = ((price - mh1W) * 100 / mh1W).ToString("#.##");
                LB_MH1WP.Text = $"MH 1W P : {temp} %";
                if (ma1W > 0)
                {
                    LB_MA_1W.Text = $" MA 1W : {ma1W}";
                    temp = ((price - ma1W) * 100 / ma1W).ToString("#.##");
                    LB_MA_1WP.Text = $"MA 1W P : {temp} %";
                }*/

                priceDic.Clear();
                priceDic.Add("PRICE", price);
                priceDic.Add("ML 1H", ml1H);
                priceDic.Add("MH 1H", mh1H);
                priceDic.Add("ML 2H", ml2H);
                priceDic.Add("MH 2H", mh2H);
                priceDic.Add("ML 4H", ml4H);
                priceDic.Add("MH 4H", mh4H);
                priceDic.Add("ML 1D", ml1D);
                priceDic.Add("MH 1D", mh1D);
                priceDic.Add("ML 1W", ml1W);
                priceDic.Add("MH 1W", mh1W);

                var sortedDic = from entry in priceDic orderby entry.Value descending select entry; //ascending

            //    LIST_BOX_SORTED.Items.Clear();

                int iResistanceIndex = 0, iSupportIndex = 0;
                for (int i = 0; i < sortedDic.Count(); i++)
                {
             //       LIST_BOX_SORTED.Items.Add($"{sortedDic.ElementAt(i).Key}:   {sortedDic.ElementAt(i).Value}");
                    if (sortedDic.ElementAt(i).Key == "PRICE")
                    {
                        iResistanceIndex = (i - 1) < 0 ? 0 : i - 1;
                        iSupportIndex = i == sortedDic.Count() - 1 ? i : i + 1;
                    }
                }
                decimal supportPrice = sortedDic.ElementAt(iSupportIndex).Value;
                decimal resistancePrice = sortedDic.ElementAt(iResistanceIndex).Value;

                decimal positionMargin = decimal.Parse(TB_PositionMargin.Text);
                decimal stopLossMargin = decimal.Parse(TB_StopLossPercentage.Text);
                positionMargin = positionMargin / 100;
                stopLossMargin = stopLossMargin / 100;


                decimal buyOpenPosition = supportPrice * (1 + positionMargin);
                decimal sellOpenPosition = resistancePrice * (1 - positionMargin);

         //       TB_BuyPositionOpen.Text = buyOpenPosition.ToString();
           //     TB_SellPositionPrice.Text = sellOpenPosition.ToString();

                decimal buyStopLoss = supportPrice * (1 - stopLossMargin);
                decimal sellStopLoss = resistancePrice * (1 + stopLossMargin);

         //       TB_BuyStopLoss.Text = buyStopLoss.ToString();
           //     TB_SellStopLoss.Text = sellStopLoss.ToString();

                decimal buyGain = -1 * ((buyOpenPosition / sellOpenPosition) - 1) * 100;
                decimal sellGain = ((sellOpenPosition / buyOpenPosition) - 1) * 100;

         //       TB_BuyGain.Text = buyGain.ToString("#.##");
           //     TB_SellGain.Text = sellGain.ToString("#.##");

                decimal buyLoss = ((buyOpenPosition / buyStopLoss) - 1) * 100;
                decimal sellLoss = ((sellStopLoss / sellOpenPosition) - 1) * 100;

          //      TB_BuyLoss.Text = buyLoss.ToString("#.##");
            //    TB_SellLoss.Text = sellLoss.ToString("#.##");

                decimal buyRiskReward = buyGain / buyLoss;
                decimal sellRiskReward = sellGain / sellLoss;

            //    if(AllCoinBuyRR.Keys.Contains(coinName))
                {
                    AllCoinBuyRR.Remove($"{coinName} Buy: ");
                }
                AllCoinBuyRR.Add($"{coinName} Buy: ", buyRiskReward);

         //       if (AllCoinSellRR.Keys.Contains(coinName))
                {
                    AllCoinSellRR.Remove($"{coinName} Sell: ");
                }
                AllCoinSellRR.Add($"{coinName} Sell: ", sellRiskReward);

                var sortedBuy = from entry in AllCoinBuyRR orderby entry.Value descending select entry; //ascending
                var sortedSell = from entry in AllCoinSellRR orderby entry.Value descending select entry; //ascending

                ListBox_Buy.Items.Clear();
                ListBox_Sell.Items.Clear();

                for (int i = 0; i < sortedBuy.Count(); i++)
                    ListBox_Buy.Items.Add($"{sortedBuy.ElementAt(i).Key} : {sortedBuy.ElementAt(i).Value}");
                for (int i = 0; i < sortedSell.Count(); i++)
                    ListBox_Sell.Items.Add($"{sortedSell.ElementAt(i).Key} : {sortedSell.ElementAt(i).Value}");


                //         TB_BuyRR.Text = buyRiskReward.ToString("#.##");
                //       TB_SellRR.Text = sellRiskReward.ToString("#.##");

                bool isExisted = false;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if(listView1.Items[i].SubItems[0].Text == coinName)
                    {
                        isExisted = true;
                        listView1.Items[i].SubItems[1].Text = mh4H.ToString();
                        listView1.Items[i].SubItems[2].Text = price.ToString();
                        listView1.Items[i].SubItems[3].Text = ml4H.ToString();
                        listView1.Items[i].SubItems[4].Text = "Macd State";
                        listView1.Items[i].SubItems[5].Text = "General Command";

                        break;
                    }
                }

                if(!isExisted)
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = coinName;
                    listItem.SubItems.Add(mh4H.ToString());
                    listItem.SubItems.Add(price.ToString());
                    listItem.SubItems.Add(ml4H.ToString());
                    listItem.SubItems.Add("Macd State");
                    listItem.SubItems.Add("General Command");
                    
                    listView1.Items.Add(listItem);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private decimal GetMovingAverage(List<IBinanceKline> candels)
        {
            if (candels == null) return -1;
            if (candels.Count < 50) return -1;
            decimal movingAverage = 0;
            List<IBinanceKline> temp = candels.Skip(Math.Max(0, candels.Count() - 100)).ToList();
            foreach(var item in temp)
            {
                movingAverage += item.ClosePrice;
            }
            movingAverage = movingAverage / temp.Count();
            return movingAverage;            
        }

        private ConcurrentBag<CategoricalDataPoint> GetMarginPoints(List<WeightedValue> valueList, DateTime startTime,
            DateTime endTime, out decimal lastvalue)
        {
            lastvalue = 0;
            var temp = new ConcurrentBag<CategoricalDataPoint>();
            try
            {
                if (valueList != null && valueList.Any())
                {
                    lastvalue = valueList.First().Value;

                    valueList.Where(s => s.Time >= startTime).AsParallel().ForAll(item =>
                    { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                    if (temp.Count == 0)
                    {
                        temp.Add(new CategoricalDataPoint((double)lastvalue, startTime));
                        temp.Add(new CategoricalDataPoint((double)lastvalue, endTime));
                        return temp;
                    }
                    if (temp.Count == 1)
                    {
                        temp.Add(new CategoricalDataPoint(temp.First().Value, startTime));
                    }
                    else
                    {
                        var firstPoint = valueList.Where(s => s.Time < startTime).OrderByDescending(s => s.Time).FirstOrDefault();
                        if (firstPoint != null)
                            temp.Add(new CategoricalDataPoint((double)firstPoint.Value, startTime));
                    }

                    temp.Add(new CategoricalDataPoint((double)lastvalue, endTime));
                }
            }
            catch (Exception ex)
            { }
            return temp;

        }

        private void ListBox_Buy_DoubleClick(object sender, EventArgs e)
        {
            string tempCoin = ListBox_Buy.GetItemText(ListBox_Buy.SelectedItem);
            var temptext = tempCoin.Split(' ');
            string coin = temptext[0];
            for(int i = 0; i < radDropDownList1.Items.Count; i++) 
            {
                if (radDropDownList1.Items[i].Text == coin)
                { radDropDownList1.SelectedIndex = i; break; }
            }
            coinName = coin;
            var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
            if (coinInfo == null) return;
            binanceModel = coinInfo;
            UpdateData(binanceModel);
        }

        private void ListBox_Sell_DoubleClick(object sender, EventArgs e)
        {
            string tempCoin = ListBox_Sell.GetItemText(ListBox_Sell.SelectedItem);
            var temptext = tempCoin.Split(' ');
            string coin = temptext[0];
            for (int i = 0; i < radDropDownList1.Items.Count; i++)
            {
                if (radDropDownList1.Items[i].Text == coin)
                { radDropDownList1.SelectedIndex = i; break; }
            }
            coinName = coin;
            var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
            if (coinInfo == null) return;
            binanceModel = coinInfo;
            UpdateData(binanceModel);
        }

        private void ConfigureAxis(double min, double max, double majorStep)
        {
            LinearAxis axis = this.radChartView1.Axes[1] as LinearAxis;
            if (axis == null)
                return;

            axis.Minimum = min;
            axis.Maximum = max;
            axis.MajorStep = majorStep;
        }

        private IndicatorBase CreateMACDIndicator(List<IBinanceKline> candels)
        {
            MacdIndicator indicator = new MacdIndicator();
            indicator.ShortPeriod = 9;
            indicator.LongPeriod = 12;
            indicator.SignalPeriod = 6;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "ClosePrice";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(-2, 2, 1);

            return indicator;
        }

        private void initAxis()
        {
            dateTimeCategoricalAxis1.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis1.IsPrimary = true;
            dateTimeCategoricalAxis1.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis1.LabelFormat = "{0:HH:mm:ss}";
            dateTimeCategoricalAxis1.MajorTickInterval = 5;

            linearAxis1.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis1.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Right;
            linearAxis1.IsPrimary = true;

            linearAxis1.TickOrigin = null;
            this.radChartView1.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1,
            linearAxis1});

            LassoZoomController lassoZoomController = new LassoZoomController();
            radChartView1.Controllers.Add(lassoZoomController);
            radChartView1.Area.View.Palette = KnownPalette.Material;

            radChartView1.BackColor = Color.AliceBlue;

        }

        private void InitSeries()
        {
            ZeroLine.HorizontalAxis = dateTimeCategoricalAxis1;
            ZeroLine.VerticalAxis = linearAxis1;
            ZeroLine.BackColor = Color.Blue;
            ZeroLine.BorderColor = Color.Blue;
        }

        private void UpdateFinancialIndicator(string coinName)
        {
            try
            {
                var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                if (coinInfo == null) return;

                //var candels = coinInfo.Candels_4hour.Skip(coinInfo.Candels_4hour.Count() - 200).Take(200).ToList();
                var candels = coinInfo.Candels_60min.Skip(coinInfo.Candels_60min.Count() - 200).Take(200).ToList();

                //if (viewModel.Data == null || !viewModel.Data.Any()) return;
                if (candels == null || !candels.Any()) return;
                DateTimeCategoricalAxis horizontalAxis = this.radChartView1.Axes[0] as DateTimeCategoricalAxis;
                LinearAxis verticalAxis = this.radChartView1.Axes[1] as LinearAxis;

                ZeroLine.DataPoints.Clear();
                this.radChartView1.Series.Clear();

                CategoricalDataPoint[] temp = { new CategoricalDataPoint(0, candels[0].CloseTime),
                        new CategoricalDataPoint(0, candels[199].CloseTime)};
                ZeroLine.DataPoints.AddRange(temp);

                IndicatorBase indicator = CreateMACDIndicator(candels);
                indicator.HorizontalAxis = horizontalAxis;
                indicator.VerticalAxis = verticalAxis;
                indicator.PointSize = SizeF.Empty;
                indicator.BorderColor = Color.Black;

                IParentIndicator parentIndicator = indicator as IParentIndicator;
                if (parentIndicator != null)
                {
                    parentIndicator.ChildIndicator.BorderColor = Color.Red;
                }
                //ConfigureAxis();
                List<double>macdValues = new List<double>();
                for (int i = 0; i < indicator.DataPoints.Count; i++)
                    macdValues.Add(indicator.GetProcessedValue(i));
                ConfigureAxis(macdValues.Min(), macdValues.Max() , (macdValues.Max() - macdValues.Min())/10);
                this.radChartView1.Series.Add(indicator);

                this.radChartView1.Series.Add(ZeroLine);

            }
            catch (Exception ex)
            {
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
           int index = listView1.SelectedIndices[0];
            coinName = listView1.Items[index].Text;
            var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
            if (coinInfo == null) return;
            binanceModel = coinInfo;
            UpdateData(binanceModel);
        }
    }
}
