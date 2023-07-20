using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.Business;
using BinanceApp.DataModel;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Skender.Stock.Indicators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace BinanceApp
{
    public partial class strategiesLongSfForm : Telerik.WinControls.UI.RadForm
    {
       
        private string coinName = string.Empty;
        private BinanceModel binanceModel;
        private KlineInterval interval = KlineInterval.OneMinute;

        private List<IBinanceKline> candels;

        public strategiesLongSfForm()
        {
            InitializeComponent();
        }
        //Telerik.WinControls.UI.CategoricalAxis baseAxis = new Telerik.WinControls.UI.CategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();

        // LineSeries price = new LineSeries();
        LineSeries longTerm1m = new LineSeries();
        LineSeries longTerm5m = new LineSeries();
        LineSeries longTerm15m = new LineSeries();
        LineSeries longTerm30m = new LineSeries();
        LineSeries longTerm1H = new LineSeries();
        LineSeries longTerm4H = new LineSeries();
        LineSeries longTerm1D = new LineSeries();
        LineSeries OneLine = new LineSeries();

        bool DrawWasDone = false;

        private Dictionary<string, LineSeries> CoinLongTermSFDic = new Dictionary<string, LineSeries>();
        private bool initialized = false;
        List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(),
            KlineInterval.FiveMinutes.ToString(),
            KlineInterval.FifteenMinutes.ToString(),
            KlineInterval.ThirtyMinutes.ToString(),
            KlineInterval.OneHour.ToString(),
            KlineInterval.FourHour.ToString(),
            KlineInterval.OneDay.ToString()
        };


        private delegate void UpdateFormDelegate();

        private void strategiesLongSfForm_Load(object sender, EventArgs e)
        {
            
            
            this.radDropDownList4.DataSource = Intervals;
            foreach (var coin in BinanceDataCollector.Instance.CoinNames)
            {
                chCoinList.Items.Add(coin, true);
                CoinLongTermSFDic.Add(coin, new LineSeries());
            }
            initAxis();
            InitSeries();
            WireEvents();
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);

            radChartView1.BackColor = Color.AliceBlue;
            radChartView1.GetArea<CartesianArea>().ShowGrid = true;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall();

            foreach (var coin in chCoinList.CheckedItems)
            {
                OnCandleReadyEvent(interval, coin.ToString());
            }
            BinanceDataCollector.Instance.DataReadyEvent += OnDateReadyEvent;
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;
            initialized = true;
        }

        private void OnDateReadyEvent()
        {
            if (DrawWasDone == false)
            {
                foreach (var coin in chCoinList.CheckedItems)
                {
                    OnCandleReadyEvent(interval, coin.ToString());
                }
            }
            else
                BinanceDataCollector.Instance.DataReadyEvent -= OnDateReadyEvent;
        }
        private delegate void UpdateSfFormDelegate(string coin);
        private void OnCandleReadyEvent(KlineInterval interval1, string coin)
        {
            try
            {
                if(coin == "BTCUSDT")
                {
                    UpdateReferenceLongTermSF(interval1, coin);
                }
                if ( interval1!= interval) return;
                for (int k = 0; k < chCoinList.CheckedItems.Count; k++)
                {
                    if (chCoinList.Items[k].ToString() == coin)
                    {
                        if (InvokeRequired)
                        {
                            UpdateSfFormDelegate d = new UpdateSfFormDelegate(UpdateDataSF);
                            this.Invoke(d, coin);
                        }
                        else
                            UpdateDataSF(coin);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        
        private void initAxis()
        {
            dateTimeCategoricalAxis1.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis1.IsPrimary = true;
            dateTimeCategoricalAxis1.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis1.LabelFormat = "{0:HH:mm:ss}";
            dateTimeCategoricalAxis1.MajorTickInterval = 50;

           
            linearAxis2.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis2.IsPrimary = true;
            linearAxis2.TickOrigin = null;

            this.radChartView1.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1, linearAxis2});

            LassoZoomController lassoZoomController = new LassoZoomController();
            radChartView1.Controllers.Add(lassoZoomController);
           // radChartView1.Area.View.Palette = KnownPalette.Material;
            radChartView1.ShowLegend = true;
            radChartView1.ShowGrid = true;
        }

        private void InitSeries()
        {
            foreach (var item in CoinLongTermSFDic)
            {
                item.Value.HorizontalAxis = dateTimeCategoricalAxis1;
                item.Value.VerticalAxis = linearAxis2;
                item.Value.LegendTitle = "LT_DSF_" + item.Key ;     
                radChartView1.Series.Add(item.Value);
            }
            
        }

        private void SetCartesianGrid(RadChartView chart)
        {
            CartesianArea area = chart.GetArea<CartesianArea>();
            area.ShowGrid = true;
            CartesianGrid grid = area.GetGrid<CartesianGrid>();
            grid.DrawHorizontalFills = false;
            grid.DrawVerticalFills = false;
            grid.DrawHorizontalStripes = true;
            grid.DrawVerticalStripes = true;
            grid.ForeColor = Color.LightGray;
            grid.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        }
        private void cb_Selection_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Selection.Checked)
            {
                for (int k = 0; k < chCoinList.Items.Count; k++)
                    if (chCoinList.GetItemChecked(k) == false)
                        chCoinList.SetItemChecked(k, true);

            }
            else
            {
                for (int k = 0; k < chCoinList.Items.Count; k++)
                    if (chCoinList.GetItemChecked(k) == true)
                        chCoinList.SetItemChecked(k, false);
            }
        }
        private void SetTrackBall()
        {
            ChartTrackballController trackBallElement = new ChartTrackballController();

            trackBallElement.Element.TextAlignment = ContentAlignment.MiddleLeft;
            trackBallElement.Element.BorderColor = Color.Black;
            trackBallElement.Element.BackColor = Color.White;
            trackBallElement.Element.BorderGradientStyle = Telerik.WinControls.GradientStyles.Solid;
            trackBallElement.Element.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
            trackBallElement.Element.Padding = new Padding(3, 0, 3, 3);

            this.radChartView1.Controllers.Add(trackBallElement);
        }

         
        protected  void WireEvents()
        {
            this.radDropDownList4.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList4_SelectedIndexChanged);
        }
        void radDropDownList4_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval))
            {
                var reflongTerm = BTCUSDT_SF.GetLongTermSF(interval);
                if (reflongTerm==null || reflongTerm.Any()==false)
                    UpdateReferenceLongTermSF(interval, "BTCUSDT");

                UpdateData();
            }
        }
        public void UpdateData()
        {
            foreach (var item in chCoinList.CheckedItems)
                UpdateDataSF(item.ToString());
        }
        private void radChartView1_DoubleClick(object sender, EventArgs e)
        {
            this.radChartView1.Zoom(1, 1);

        }
        private void UpdateReferenceLongTermSF(KlineInterval interval, string coin)
        {
            try
            {
                var binanceModel = BinanceDataCollector.Instance.GetBinance(coin);
                if (binanceModel == null) return;
                if (binanceModel.PriceList == null || binanceModel.PriceList.Count == 0)
                    return;
                decimal currentPrice = binanceModel.PriceList.Last().Value;
                switch (interval)
                {
                    case KlineInterval.OneMinute:
                        if (binanceModel.Candels_1min == null) return;
                        candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FiveMinutes:
                        if (binanceModel.Candels_5min == null) return;
                        candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FifteenMinutes:
                        if (binanceModel.Candels_15min == null) return;
                        candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.ThirtyMinutes:
                        if (binanceModel.Candels_30min == null) return;
                        candels = binanceModel.Candels_30min.Skip(binanceModel.Candels_30min.Count() - 500).Take(500).ToList(); ;
                        break;
                    case KlineInterval.OneHour:
                        if (binanceModel.Candels_60min == null) return;
                        candels = binanceModel.Candels_60min.Skip(binanceModel.Candels_60min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FourHour:
                        if (binanceModel.Candels_4hour == null) return;
                        candels = binanceModel.Candels_4hour.Skip(binanceModel.Candels_4hour.Count() - 500).Take(500).ToList(); ;
                        break;
                    case KlineInterval.OneDay:
                        if (binanceModel.Candels_oneDay == null) return;
                        candels = binanceModel.Candels_oneDay.Skip(binanceModel.Candels_oneDay.Count() - 500).Take(500).ToList(); ;
                        break;
                }
                if (candels == null || candels.Count == 0) return;
                var longTerm_SF = BinanceHelper.LongTermFC(candels, currentPrice).Where(x => x.Value != 0);

                if (longTerm_SF != null)
                {
                    BTCUSDT_SF.UpdateLongTerm(interval, longTerm_SF.ToList());
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void UpdateDataSF(string coin)
        {
            try
            {
                var binanceModel = BinanceDataCollector.Instance.GetBinance(coin);
                if (binanceModel == null) return;
                if (binanceModel.PriceList == null || binanceModel.PriceList.Count == 0)
                    return;
                decimal currentPrice = binanceModel.PriceList.Last().Value;
                switch (interval)
                {
                    case KlineInterval.OneMinute:
                        if (binanceModel.Candels_1min == null) return;
                        candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FiveMinutes:
                        if (binanceModel.Candels_5min == null) return;
                        candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FifteenMinutes:
                        if (binanceModel.Candels_15min == null) return;
                        candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.ThirtyMinutes:
                        if (binanceModel.Candels_30min == null) return;
                        candels = binanceModel.Candels_30min.Skip(binanceModel.Candels_30min.Count() - 500).Take(500).ToList(); ;
                        break;
                    case KlineInterval.OneHour:
                        if (binanceModel.Candels_60min == null) return;
                        candels = binanceModel.Candels_60min.Skip(binanceModel.Candels_60min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FourHour:
                        if (binanceModel.Candels_4hour == null) return;
                        candels = binanceModel.Candels_4hour.Skip(binanceModel.Candels_4hour.Count() - 500).Take(500).ToList(); ;
                        break;
                    case KlineInterval.OneDay:
                        if (binanceModel.Candels_oneDay == null) return;
                        candels = binanceModel.Candels_oneDay.Skip(binanceModel.Candels_oneDay.Count() - 500).Take(500).ToList(); ;
                        break;
                }
                if (candels == null || candels.Count == 0) return;
                var longTerm_SF = BinanceHelper.LongTermFC(candels, currentPrice).Where(x => x.Value != 0);
                var refLongTerm_SF = BTCUSDT_SF.GetLongTermSF(interval);

                if (longTerm_SF != null && refLongTerm_SF!=null && refLongTerm_SF.Any())
                {
                    if (CoinLongTermSFDic[coin].DataPoints != null)
                        CoinLongTermSFDic[coin].DataPoints.Clear();
                    var result = longTerm_SF.Join(refLongTerm_SF.Where(x => x.Value != 0), a => a.Time, b => b.Time,
                        (a, b) => new CategoricalDataPoint((double)((a.Value / b.Value) - 1), a.Time));
                    CoinLongTermSFDic[coin].DataPoints.AddRange(result);
                }
                DrawWasDone = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void strategiesLongSfForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;
        }

        private void chCoinList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (initialized == false) return;
                var coin = chCoinList.Items[e.Index].ToString();
                if (chCoinList.GetItemChecked(e.Index) == false)
                {
                    radChartView1.Series.Add(CoinLongTermSFDic[coin]);
                }
                else
                {
                    if (radChartView1.Series.Count == 0)
                        return;
                    radChartView1.Series.Remove(CoinLongTermSFDic[coin]);
                   
                }
            }
            catch { }
        }
    }
}
