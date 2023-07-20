using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.Business;
using BinanceApp.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace BinanceApp
{
    public partial class BuyForm : Telerik.WinControls.UI.RadForm
    {
        private delegate void UpdateFormDelegate(BinanceModel binanceModel);
        private delegate void CandleReadyDelegate(KlineInterval interval1, string coin);

        private string coinName = string.Empty;
        private BinanceModel binanceModel;
        private KlineInterval interval = KlineInterval.OneMinute;
        List<string> Intervals = new List<string> { 
            KlineInterval.OneMinute.ToString(), 
            KlineInterval.FiveMinutes.ToString(), 
            KlineInterval.FifteenMinutes.ToString(),
            KlineInterval.ThirtyMinutes.ToString(),
            KlineInterval.OneHour.ToString(),
            KlineInterval.FourHour.ToString(),
            KlineInterval.OneDay.ToString()
        };

        private List<Color> ButtonColor = new List<Color> { Color.LightGray, Color.Pink, Color.LightGreen };
        private List<TextBox> SxTextBoxes;
        private decimal majorHigh = 0;
        private decimal majorLow = 0;
        public BuyForm()
        {
            InitializeComponent();
        }
        
        private void BuyForm_Load(object sender, EventArgs e)
        {
            SxTextBoxes = new List<TextBox> { tb_S1, tb_S2, tb_S3, tb_S4, tb_S5, tb_S6};
            WireEvents();
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;
            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;
            this.radDropDownList4.DataSource = Intervals;
        }
        
        private void BuyForm_Closed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;
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
            if (InvokeRequired)
            {
                var d = new UpdateFormDelegate(UpdateData);
                this.Invoke(d, binanceModel);
            }
            else
                UpdateData(binanceModel);
        }

        protected void WireEvents()
        {
            this.radDropDownList1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList1_SelectedIndexChanged);
            this.radDropDownList4.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList4_SelectedIndexChanged);

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
                CandleReadyEvent(KlineInterval.OneMinute, coinName);
            }
        }
        void radDropDownList4_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (this.radDropDownList1.SelectedItem != null)
            {
                coinName = this.radDropDownList1.SelectedItem.Text;
                Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);
                var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                if (coinInfo == null) return;
                binanceModel = coinInfo;               
                UpdateData(binanceModel);
                CandleReadyEvent(KlineInterval.OneMinute, coinName);
            }
        }
        private void UpdateData(BinanceModel binanceModel)
        {
            try
            {
                if (binanceModel == null) return;
                price_LB.Text = binanceModel.CurrentPrice.ToString();
                var ml1min =(binanceModel.MajorLowPrice1Min!=null)? binanceModel.MajorLowPrice1Min.FirstOrDefault(): null;
                var ml5min =(binanceModel.MajorLowPrice5Min!=null)? binanceModel.MajorLowPrice5Min.FirstOrDefault():null;
                var ml15min = (binanceModel.MajorLowPrice15Min!=null)? binanceModel.MajorLowPrice15Min.FirstOrDefault(): null;
                var ml30min = (binanceModel.MajorLowPrice30Min!=null)?binanceModel.MajorLowPrice30Min.FirstOrDefault(): null;
                var ml1hour = (binanceModel.MajorLowPrice60Min!=null)?binanceModel.MajorLowPrice60Min.FirstOrDefault(): null;
                var ml4hour = (binanceModel.MajorLowPrice4hour!=null)?binanceModel.MajorLowPrice4hour.FirstOrDefault(): null;
                var ml1Day = (binanceModel.MajorLowPrice1Day!=null)?binanceModel.MajorLowPrice1Day.FirstOrDefault(): null;

                tb_1mMl.Text = ml1min == null ? string.Empty : ml1min.Value.ToString("N4");
                tb_5mMl.Text = ml5min == null ? string.Empty : ml5min.Value.ToString("N4");
                tb_15mMl.Text = ml5min == null ? string.Empty : ml15min.Value.ToString("N4");
                tb_30mMl.Text = ml30min == null ? string.Empty : ml30min.Value.ToString("N4");
                tb_1hMl.Text = ml1hour == null ? string.Empty : ml1hour.Value.ToString("N4");
                tb_4hMl.Text = ml4hour == null ? string.Empty : ml4hour.Value.ToString("N4");
                tb_1dMl.Text = ml1Day == null ? string.Empty : ml1Day.Value.ToString("N4");
                switch(interval)
                {
                    case KlineInterval.OneMinute:
                        var mh1min = (binanceModel.MajorHighPrice1Min != null)?binanceModel.MajorHighPrice1Min.FirstOrDefault(): null;
                        GetML_ML(binanceModel, ml1min, mh1min);
                        break;
                    case KlineInterval.FiveMinutes:
                        var mh5min =(binanceModel.MajorHighPrice5Min != null)? binanceModel.MajorHighPrice5Min.FirstOrDefault(): null;
                        GetML_ML(binanceModel, ml5min, mh5min);
                        break;
                    case KlineInterval.FifteenMinutes:
                        var mh15min = (binanceModel.MajorHighPrice15Min!=null)?binanceModel.MajorHighPrice15Min.FirstOrDefault(): null;
                        GetML_ML(binanceModel, ml15min, mh15min);
                        break;
                    case KlineInterval.ThirtyMinutes:
                        var mh30min = (binanceModel.MajorHighPrice30Min!=null)?binanceModel.MajorHighPrice30Min.FirstOrDefault(): null;
                        GetML_ML(binanceModel, ml30min, mh30min);
                        break;
                    case KlineInterval.OneHour:
                        var mh60min = (binanceModel.MajorHighPrice60Min!=null)? binanceModel.MajorHighPrice60Min.FirstOrDefault(): null;
                        GetML_ML(binanceModel, ml1hour, mh60min);
                        break;
                    case KlineInterval.FourHour:
                        var mh4hour = (binanceModel.MajorHighPrice4hour!=null) ?binanceModel.MajorHighPrice4hour.FirstOrDefault(): null;
                        GetML_ML(binanceModel, ml4hour, mh4hour);
                        break;
                    case KlineInterval.OneDay:
                        var mh1day = (binanceModel.MajorHighPrice1Day!=null)? binanceModel.MajorHighPrice1Day.FirstOrDefault(): null;
                        GetML_ML(binanceModel, ml1Day, mh1day);
                        break;
                }
                if (decimal.TryParse(tb_buyPrice.Text, out decimal buyPrice))
                    CalcBuyInfo(buyPrice);
                if (string.IsNullOrWhiteSpace(tb_ema5.Text))
                    CandleReadyEvent(KlineInterval.OneMinute, coinName);
            }
            catch(Exception ex)
            { }
        }

        private void GetML_ML(BinanceModel binanceModel, WeightedValue ml, WeightedValue mh)
        {
            if (ml != null && mh!=null)
            {
                majorLow = ml.Value;
                majorHigh = mh.Value;
            }
        }
        private void OnCandleReadyEvent(KlineInterval interval1, string coin)
        {
            if (InvokeRequired)
            {
                var d = new CandleReadyDelegate(CandleReadyEvent);
                this.Invoke(d, interval1, coin);
            }
            else
                CandleReadyEvent(interval1, coin);
        }
        private void CandleReadyEvent(KlineInterval interval1, string coin)
        {
            try
            {
                if (coin != coinName) return;
                if (binanceModel == null) return;
                if (interval1 != KlineInterval.OneMinute) return;

                List<IBinanceKline> candels = new List<IBinanceKline>();
                
                switch (interval)
                {
                    case KlineInterval.OneMinute:
                        if(binanceModel.Candels_1min!=null && binanceModel.Candels_1min.Any())
                            candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 200).Take(200).ToList();
                       
                        break;
                    case KlineInterval.FiveMinutes:
                        if (binanceModel.Candels_5min != null && binanceModel.Candels_5min.Any())
                            candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 200).Take(200).ToList();
                        
                        break;
                    case KlineInterval.FifteenMinutes:
                        if (binanceModel.Candels_15min != null && binanceModel.Candels_15min.Any())
                            candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 200).Take(200).ToList();
                       
                        break;
                    case KlineInterval.ThirtyMinutes:
                        if (binanceModel.Candels_30min != null && binanceModel.Candels_30min.Any())
                            candels = binanceModel.Candels_30min.Skip(binanceModel.Candels_30min.Count() - 200).Take(200).ToList();
                       
                        break;
                    case KlineInterval.OneHour:
                        if (binanceModel.Candels_60min != null && binanceModel.Candels_60min.Any())
                            candels = binanceModel.Candels_60min.Skip(binanceModel.Candels_60min.Count() - 200).Take(200).ToList();
                       
                        break;
                    case KlineInterval.FourHour:
                        if (binanceModel.Candels_4hour != null && binanceModel.Candels_4hour.Any())
                            candels = binanceModel.Candels_4hour.Skip(binanceModel.Candels_4hour.Count() - 200).Take(200).ToList();
                        
                        break;
                    case KlineInterval.OneDay:
                        if (binanceModel.Candels_oneDay != null && binanceModel.Candels_oneDay.Any())
                            candels = binanceModel.Candels_oneDay.Skip(binanceModel.Candels_oneDay.Count() - 200).Take(200).ToList();
                        break;
                }
                if (candels == null || candels.Count == 0) return;
                var sortTerm_Sf = BinanceHelper.LastShortTermFC(candels, binanceModel.CurrentPrice);
                var longTerm_SF = BinanceHelper.LastLongTermFC(candels, binanceModel.CurrentPrice);
                var qutoes = BinanceHelper.GetQuotes(candels);
                var ema5 = BinanceHelper.EMA(qutoes, 5);
                var ema7 = BinanceHelper.EMA(qutoes, 7);
                var ema20 = BinanceHelper.EMA(qutoes, 20);
                var ema50 = BinanceHelper.EMA(qutoes, 50);
                if (ema5 != null)
                    tb_ema5.Text = ema5.LastOrDefault() == null ? string.Empty : ema5.LastOrDefault().Ema?.ToString("N4");
                if (ema7 != null)
                    tb_ema7.Text = ema7.LastOrDefault() == null ? string.Empty : ema7.LastOrDefault().Ema?.ToString("N4");
                if (ema20 != null)
                    tb_ema20.Text = ema20.LastOrDefault() == null ? string.Empty : ema20.LastOrDefault().Ema?.ToString("N4");
                if (ema50 != null)
                    tb_ema50.Text = ema50.LastOrDefault() == null ? string.Empty : ema50.LastOrDefault().Ema?.ToString("N4");

                lb_ST.BackColor = (sortTerm_Sf.Value < 1) ? Color.Pink : Color.LightGray;
                lb_LT.BackColor = (longTerm_SF.Value < 1) ? Color.Pink : Color.LightGray;
            }
            catch (Exception ex)
            { }
        }

        private void bt_Click(object sender, EventArgs e)
        {
            var currentColor = ((Button)sender).BackColor;
            var index = ButtonColor.IndexOf(currentColor);
            if (index < 0) index = 0;
            index = (index + 1) % 3;
            ((Button)sender).BackColor = ButtonColor[index];
        }

        private void tb_Sx_Click(object sender, EventArgs e)
        {
            var selectedSx = ((TextBox)sender);
            int index = SxTextBoxes.FindIndex(x=>x.Name==selectedSx.Name);
            if (index < 0) index = 0;
            if (index == 5) index = 4;
            tb_support1.Text = SxTextBoxes[index].Text;
            tb_support2.Text = SxTextBoxes[index + 1].Text;
            if(chk_Auto.Checked)
                tb_buyPrice.Text = tb_support1.Text;
        }

        private void chk_Auto_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_Auto.Checked)
            {
                tb_buyPrice.ReadOnly = true;
                tb_buyPrice.Text = tb_support1.Text;
            }
            else
            {
                tb_buyPrice.ReadOnly = false;
            }
            if (decimal.TryParse(tb_buyPrice.Text, out decimal buyPrice))
                CalcBuyInfo(buyPrice);
        }
        private void CalcBuyInfo(decimal buyPrice)
        {
            var priceDiff = ((buyPrice - binanceModel.CurrentPrice) * 100) / binanceModel.CurrentPrice;
            tb_priceDiff.Text = priceDiff.ToString("N4") + " %";
            if(decimal.TryParse(tb_support2.Text, out decimal secondSupport))
            {
                tb_stopLoss.Text = (secondSupport -(decimal) 0.1).ToString("N4");
                //(MH - ML)÷(ML - (0.9 * SecondSupport))
                tb_risk.Text = ((majorHigh - majorLow) / (majorLow - (decimal)(0.9) * secondSupport)).ToString("N4");
            }           
        }

        private void btn_trend_Click(object sender, EventArgs e)
        {
            if(btn_trend.Text.StartsWith("Up"))
            {
                btn_trend.Text = "Downward Trend";
                btn_trend.BackColor = Color.Pink;
            }
            else
            {
                btn_trend.Text = "Upward Trend";
                btn_trend.BackColor = Color.LightGreen;
            }
        }

        
    }
}
