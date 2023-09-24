using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.Business;
using BinanceApp.DataModel;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class TradeBotForm : Telerik.WinControls.UI.RadForm
    {
        public TradeBotForm()
        {
            InitializeComponent();
        }

        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis2 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

        CandlestickSeries candleSery = new CandlestickSeries();

        private List<string> movingAverageIndicators, financialIndicators;
        
        private string coinName = string.Empty;

        private KlineInterval interval = KlineInterval.OneMinute;

        private BinanceModel binance;

        private List<IBinanceKline> candels;

        private void Form1_Load(object sender, EventArgs e)
        {

            initAxis();
            InitSeries();
            InitializeData();
            WireEvents();

            this.radDropDownList3.DataSource = BinanceDataCollector.Instance.CoinNames;
            List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(),
                KlineInterval.FiveMinutes.ToString(), KlineInterval.FifteenMinutes.ToString() };
 
            radChartView2.BackColor = Color.Gray;

            SetCartesianGrid(this.radChartView2);
            SetTrackBall();

            coinName = this.radDropDownList3.SelectedItem.Text;

            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
        }
        private  delegate  void UpdateFormDelegate(BinanceModel binanceModel);
        private void OnDataReadyEvent()
        {
            try
            {
                if (binance == null)
                {
                    if (!string.IsNullOrWhiteSpace(coinName))
                    {
                        var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                        if (coinInfo == null) return;
                        binance = coinInfo;
                    }
                    else
                        return;
                }
                if (InvokeRequired)
                {
                    var d = new UpdateFormDelegate(UpdateData);
                    this.Invoke(d, binance);
                }
                else
                    UpdateData(binance);
                
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        private void UpdateData(BinanceModel binanceModel)
        {
            try
            {
                UpdateStockSeries();
            }
            catch { }
        }
       
        private void InitializeData()
        {
            movingAverageIndicators = new List<string>()
            {
                "MA (5)",
                "Exponential MA (5)",
                "Modified MA (5)",
                "Modified Exponential MA (5)",
                "Weighted MA (5)",
                "Adaptive MA Kaufman (10,4,2)",
                "Bollinger Bands (5,2)"
            };

            financialIndicators = new List<string>()
            {
                "Average True Range (5)",
                "Commodity Channel Index (5)",
                "MACD (12, 9, 6)",
                "Momentum (5)",
                "Oscillator (8, 4)",
                "RAVI (8, 4)",
                "Rate Of Change (8)",
                "Relative Momentum Index (8)",
                "Relative Strength Index (8)",
                "Stochastic Fast (14, 3)",
                "Stochastic Slow (14, 3, 3)",
                "TRIX (8)",
                "True Range",
                "Ultimate Oscillator (6, 9, 12)"
            };
        }

        private void initAxis()
        {
            LassoZoomController lassoZoomController = new LassoZoomController();

            dateTimeCategoricalAxis2.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis2.IsPrimary = true;
            dateTimeCategoricalAxis2.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis2.LabelFormat = "{0:M/d HH:mm:ss}";
            dateTimeCategoricalAxis2.MajorTickInterval = 5;

            linearAxis2.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Right;
            linearAxis2.IsPrimary = true;
            linearAxis2.MajorStep = 5D;
            linearAxis2.Maximum = 80D;
            linearAxis2.Minimum = 50D;
            linearAxis2.TickOrigin = null;

            radChartView2.Area.View.Palette = KnownPalette.Material;
            this.radChartView2.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis2,
            linearAxis2});

            LassoZoomController lassoZoomController1 = new LassoZoomController();
            radChartView2.Controllers.Add(lassoZoomController1);
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
        private void SetTrackBall()
        {
            ChartTrackballController trackBallElement = new ChartTrackballController();

            trackBallElement.Element.TextAlignment = ContentAlignment.MiddleLeft;
            trackBallElement.Element.BorderColor = Color.Black;
            trackBallElement.Element.BackColor = Color.White;
            trackBallElement.Element.BorderGradientStyle = Telerik.WinControls.GradientStyles.Solid;
            trackBallElement.Element.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
            trackBallElement.Element.Padding = new Padding(3, 0, 3, 3);

            this.radChartView2.Controllers.Add(trackBallElement);
        }

        private void radDropDownList3_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (this.radDropDownList3.SelectedItem != null)
            {
                coinName = this.radDropDownList3.SelectedItem.Text;
                var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                if (coinInfo == null) return;
                binance = coinInfo;
                UpdateStockSeries();
            }
        }

        private void InitSeries()
        {
            candleSery.BorderColor = Color.FromArgb(102, 102, 102);
            candleSery.BackColor = Color.FromArgb(102, 102, 102);
            candleSery.HorizontalAxis = dateTimeCategoricalAxis2;
            candleSery.VerticalAxis = linearAxis2;
            candleSery.OpenValueMember = "OpenPrice";
            candleSery.HighValueMember = "HighPrice";
            candleSery.LowValueMember = "LowPrice";
            candleSery.CloseValueMember = "ClosePrice";
            candleSery.CategoryMember = "CloseTime";
        }
        private void UpdateStockSeries()
        {
            try
            {
                if (binance == null ) return;

                this.radChartView2.Series.Clear();
                if (binance.Tbqv_15min.Any() == false)
                    binance = BinanceDataCollector.Instance.GetBinance(coinName);
                candels = binance.Candels_4hour.Skip(binance.Candels_4hour.Count() - 100).Take(100).ToList();

                if (candels == null || candels.Any() == false) return;
                candleSery.DataSource = candels;

                var max = candels.Max(v => v.HighPrice);
                var min = candels.Min(v => v.LowPrice);
                if(max-min<3)
                {
                    linearAxis2.Maximum =(double) max;
                    linearAxis2.Minimum =(double) min;
                    linearAxis2.MajorStep = (double)((max-min) / 10);
                }
                else
                {
                    linearAxis2.Maximum = (double)Math.Ceiling(max);
                    linearAxis2.Minimum = (double)Math.Floor(min);
                    linearAxis2.MajorStep = Math.Ceiling((linearAxis2.Maximum - linearAxis2.Minimum) / 10);
                }
                this.radChartView2.Series.Add(candleSery);
            }
            catch (Exception ex)
            { }
        }

        protected  void WireEvents()
        {
            this.radDropDownList3.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList3_SelectedIndexChanged);
        }

        private void radChartView2_DoubleClick(object sender, EventArgs e)
        {
            this.radChartView2.Zoom(1, 1);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;

        }
    }
}
