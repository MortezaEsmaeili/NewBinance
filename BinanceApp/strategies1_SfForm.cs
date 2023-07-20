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
    public partial class strategies1_SfForm : Telerik.WinControls.UI.RadForm
    {
        List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(),
            KlineInterval.FiveMinutes.ToString(),
            KlineInterval.FifteenMinutes.ToString(),
            KlineInterval.ThirtyMinutes.ToString(),
            KlineInterval.OneHour.ToString(),
            KlineInterval.FourHour.ToString(),
            KlineInterval.OneDay.ToString()
        };
        
        private string coinName = string.Empty;
        private BinanceModel binanceModel;
        private KlineInterval interval = KlineInterval.OneMinute;

        private List<IBinanceKline> candels;

        public strategies1_SfForm()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

        LineSeries price = new LineSeries();
        LineSeries shortTermSf = new LineSeries();
        LineSeries longTermSf = new LineSeries();
        LineSeries LineOne = new LineSeries();
        

        private delegate void UpdateFormDelegate();

        private void strategies1_SfForm_Load(object sender, EventArgs e)
        {
            
            initAxis();
            InitSeries();
            WireEvents();


            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;
            this.radDropDownList4.DataSource = Intervals;

            radChartView1.BackColor = Color.FromArgb(250,210,210);
            radChartView1.GetArea<CartesianArea>().ShowGrid = true;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall();
            coinName = this.radDropDownList1.SelectedItem.Text;
            
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;
            //BinanceDataCollector.Instance.DataReadyEvent += OnDateReadyEvent;
            OnCandleReadyEvent(interval, coinName);
            sendLog("Initializing ...");
        }

        //private void OnDateReadyEvent()
        //{
        //    OnCandleReadyEvent(interval, coinName);
        //    if (binanceModel != null)
        //        BinanceDataCollector.Instance.DataReadyEvent -= OnDateReadyEvent;
        //}

        private void sendLog(string strLog)
        {

        }
                
        private void OnCandleReadyEvent(KlineInterval interval, string coin)
        {
            try
            {
                if (coin != coinName || interval!= KlineInterval.OneMinute) return;
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
                //if(strategy03 != null)
                //    strategy03.DataReady(binanceModel);
                if (InvokeRequired)
                {
                    var d = new UpdateFormDelegate(UpdateData);
                    this.Invoke(d);
                }
                else
                    UpdateData();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void UpdateData()
        {
            try
            {
                UpdateStockSeries();
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

            linearAxis1.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis1.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Right;
            linearAxis1.IsPrimary = true;
            linearAxis1.TickOrigin = null;

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

        }

        private void InitSeries()
        {
            shortTermSf.HorizontalAxis = dateTimeCategoricalAxis1;
            shortTermSf.VerticalAxis = linearAxis2;
            shortTermSf.BorderColor = Color.Red;
            shortTermSf.LegendTitle = "ShortTerm 1/SF";

            longTermSf.HorizontalAxis = dateTimeCategoricalAxis1;
            longTermSf.VerticalAxis = linearAxis2;
            longTermSf.BorderColor = Color.Green;
            longTermSf.LegendTitle = "LongTerm 1/SF";

            price.HorizontalAxis = dateTimeCategoricalAxis1;
            price.VerticalAxis = linearAxis1;
            price.BorderColor = Color.Blue;
            price.LegendTitle = "Price";

            LineOne.HorizontalAxis = dateTimeCategoricalAxis1;
            LineOne.VerticalAxis = linearAxis2;
            LineOne.BorderColor = Color.Black;
            LineOne.BackColor = Color.Black;
            LineOne.IsVisibleInLegend = false;
            LineOne.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;


            radChartView1.Series.Add(price);
            radChartView1.Series.Add(longTermSf);
            radChartView1.Series.Add(shortTermSf);
            radChartView1.Series.Add(LineOne);
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

            this.radChartView1.Controllers.Add(trackBallElement);
        }

        void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (this.radDropDownList1.SelectedItem != null)
            {
                coinName = this.radDropDownList1.SelectedItem.Text;
               // strategy03 = new Strategy03Business(coinName, interval);
                
                var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                if (coinInfo == null) return;
                binanceModel = coinInfo;
               // strategy03.DataReady(binanceModel);
                UpdateData();
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
                UpdateData();
            }
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

  
        protected  void WireEvents()
        {
            this.radDropDownList1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList1_SelectedIndexChanged);
            this.radDropDownList4.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList4_SelectedIndexChanged);

        }

        private void radChartView1_DoubleClick(object sender, EventArgs e)
        {
            this.radChartView1.Zoom(1, 1);

        }
                              
        private void UpdateStockSeries()
        {
            try
            {
                if (binanceModel == null) return;
                if (binanceModel.PriceList == null || binanceModel.PriceList.Count == 0)
                    return;
                decimal currentPrice = binanceModel.PriceList.Last().Value;
                price_LB.Text = currentPrice.ToString();

                
                switch (interval)
                {
                    case KlineInterval.OneMinute:
                        if (binanceModel.Candels_1min == null || binanceModel.Candels_1min.Any() == false) return;

                        candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FiveMinutes:
                        if (binanceModel.Candels_5min == null || binanceModel.Candels_5min.Any() == false) return;

                        candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FifteenMinutes:
                        if (binanceModel.Candels_15min == null || binanceModel.Candels_15min.Any() == false) return;

                        candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.ThirtyMinutes:
                        if (binanceModel.Candels_30min == null || binanceModel.Candels_30min.Any() == false) return;

                        candels = binanceModel.Candels_30min.Skip(binanceModel.Candels_30min.Count() - 500).Take(500).ToList(); ;
                        break;
                    case KlineInterval.OneHour:
                        if (binanceModel.Candels_60min == null || binanceModel.Candels_60min.Any() == false) return;

                        candels = binanceModel.Candels_60min.Skip(binanceModel.Candels_60min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FourHour:
                        if (binanceModel.Candels_4hour == null || binanceModel.Candels_4hour.Any() == false) return;

                        candels = binanceModel.Candels_4hour.Skip(binanceModel.Candels_4hour.Count() - 500).Take(500).ToList(); ;
                        break;
                    case KlineInterval.OneDay:
                        if (binanceModel.Candels_oneDay == null || binanceModel.Candels_oneDay.Any() == false) return;

                        candels = binanceModel.Candels_oneDay.Skip(binanceModel.Candels_oneDay.Count() - 500).Take(500).ToList(); ;
                        break;
                }
                if (candels == null || candels.Count == 0) return;
                var sortTerm_Sf = BinanceHelper.ShortTermFC(candels, currentPrice);
                var longTerm_SF = BinanceHelper.LongTermFC(candels, currentPrice);

                LineOne.DataPoints.Clear();
                LineOne.DataPoints.Add(new CategoricalDataPoint(1, sortTerm_Sf.First().Time));
                LineOne.DataPoints.Add(new CategoricalDataPoint(1, sortTerm_Sf.Last().Time));

                price.DataPoints.Clear();
                price.DataPoints.AddRange(candels.Select(c => new CategoricalDataPoint((double)c.ClosePrice, c.CloseTime)));
                if (sortTerm_Sf != null)
                {
                    shortTermSf.DataPoints.Clear();
                    shortTermSf.DataPoints.AddRange(sortTerm_Sf.Where(s=>s.Value!=0).
                        Select(s => new CategoricalDataPoint(1/(double)s.Value, s.Time)));
                }
                if(longTerm_SF != null)
                {
                    longTermSf.DataPoints.Clear();
                    longTermSf.DataPoints.AddRange(longTerm_SF.Where(s=>s.Value!=0).
                        Select(s => new CategoricalDataPoint(1/(double)s.Value, s.Time)));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           
        }

        private void strategiesSfForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;

        }
    }
}
