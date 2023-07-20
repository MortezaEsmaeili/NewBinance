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
    public partial class strategiesShortSfForm_old : Telerik.WinControls.UI.RadForm
    {
       
        private string coinName = string.Empty;
        private BinanceModel binanceModel;
        private KlineInterval interval = KlineInterval.OneMinute;

        private List<IBinanceKline> candels;

        public strategiesShortSfForm_old()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.CategoricalAxis baseAxis = new Telerik.WinControls.UI.CategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

       // LineSeries price = new LineSeries();
        LineSeries shortTerm1m = new LineSeries();
        LineSeries shortTerm5m = new LineSeries();
        LineSeries shortTerm15m = new LineSeries();
        LineSeries shortTerm30m = new LineSeries();
        LineSeries shortTerm1H = new LineSeries();
        LineSeries shortTerm4H = new LineSeries();
        LineSeries shortTerm1D = new LineSeries();
        LineSeries OneLine = new LineSeries();
       

        private delegate void UpdateFormDelegate();

        private void strategiesShortSfForm_Load(object sender, EventArgs e)
        {
            
            initAxis();
            InitSeries();
            WireEvents();


            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;

            radChartView1.BackColor = Color.AliceBlue;
            radChartView1.GetArea<CartesianArea>().ShowGrid = true;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall();
            coinName = this.radDropDownList1.SelectedItem.Text;
            
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;
            OnCandleReadyEvent(interval, coinName);
            sendLog("Initializing ...");
        }

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
            //dateTimeCategoricalAxis1.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            //dateTimeCategoricalAxis1.IsPrimary = true;
            //dateTimeCategoricalAxis1.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            //dateTimeCategoricalAxis1.LabelFormat = "{0:HH:mm:ss}";
            //dateTimeCategoricalAxis1.MajorTickInterval = 50;

            /*linearAxis1.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis1.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Right;
            linearAxis1.IsPrimary = true;
            linearAxis1.TickOrigin = null;*/


            baseAxis.AxisType = Telerik.Charting.AxisType.First;
            baseAxis.LabelFitMode = AxisLabelFitMode.MultiLine;
            baseAxis.IsPrimary = true;
            baseAxis.MajorTickInterval = 20;

            linearAxis2.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis2.IsPrimary = true;
            linearAxis2.TickOrigin = null;

            this.radChartView1.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            baseAxis, linearAxis2});

            LassoZoomController lassoZoomController = new LassoZoomController();
            radChartView1.Controllers.Add(lassoZoomController);
           // radChartView1.Area.View.Palette = KnownPalette.Material;
            radChartView1.ShowLegend = true;

        }

        private void InitSeries()
        {
            shortTerm1m.HorizontalAxis = baseAxis;
            shortTerm1m.VerticalAxis = linearAxis2;
            shortTerm1m.BorderColor = Color.Red;
            shortTerm1m.LegendTitle = "ST01";

            shortTerm5m.HorizontalAxis = baseAxis;
            shortTerm5m.VerticalAxis = linearAxis2;
            shortTerm5m.BorderColor = Color.Green;
            shortTerm5m.LegendTitle = "ST05";

            shortTerm15m.HorizontalAxis = baseAxis;
            shortTerm15m.VerticalAxis = linearAxis2;
            shortTerm15m.BorderColor = Color.Blue;
            shortTerm15m.LegendTitle = "ST15";

            shortTerm30m.HorizontalAxis = baseAxis;
            shortTerm30m.VerticalAxis = linearAxis2;
            shortTerm30m.BorderColor = Color.DarkOrange;
            shortTerm30m.LegendTitle = "ST30";

            shortTerm1H.HorizontalAxis = baseAxis;
            shortTerm1H.VerticalAxis = linearAxis2;
            shortTerm1H.BorderColor = Color.Bisque;
            shortTerm1H.LegendTitle = "ST1H";

            shortTerm4H.HorizontalAxis = baseAxis;
            shortTerm4H.VerticalAxis = linearAxis2;
            shortTerm4H.BorderColor = Color.Brown;
            shortTerm4H.LegendTitle = "ST4H";

            shortTerm1D.HorizontalAxis = baseAxis;
            shortTerm1D.VerticalAxis = linearAxis2;
            shortTerm1D.BorderColor = Color.BurlyWood;
            shortTerm1D.LegendTitle = "ST1D";

            OneLine.HorizontalAxis = baseAxis;
            OneLine.VerticalAxis = linearAxis2;
            OneLine.BorderColor = Color.Black;
            OneLine.BackColor = Color.Black;
            OneLine.IsVisibleInLegend = false;
            OneLine.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            OneLine.DataPoints.Add(new CategoricalDataPoint(1, 0));
            OneLine.DataPoints.Add(new CategoricalDataPoint(1, 490));
            //price.HorizontalAxis = baseAxis;
            //price.VerticalAxis = linearAxis1;
            //price.BorderColor = Color.Black;
            //price.LegendTitle = "Price";

            //radChartView1.Series.Add(price);
            radChartView1.Series.Add(shortTerm1m);
            radChartView1.Series.Add(shortTerm5m);
            radChartView1.Series.Add(shortTerm15m);
            radChartView1.Series.Add(shortTerm30m);
            radChartView1.Series.Add(shortTerm4H);
            radChartView1.Series.Add(shortTerm1D);
            radChartView1.Series.Add(OneLine);
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
                //decimal minValue = 0;
                //decimal maxValue = 0;
                if (binanceModel.Candels_1min.Any() != false)
                {
                    candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 500).Take(500).ToList();
                    var sortTerm1m = BinanceHelper.ShortTermFC(candels, currentPrice);
                    shortTerm1m.DataPoints.Clear();
                     int k = 0;
                    shortTerm1m.DataPoints.AddRange(
                        sortTerm1m.Where(s => s.Value != 0).Select(s => new CategoricalDataPoint((double)s.Value, k++)));
                    //minValue = sortTerm1m.Min(s => s.Value);
                    //maxValue = sortTerm1m.Max(s => s.Value);
                }
                if (binanceModel.Candels_5min.Any() != false)
                {
                    int k = 0;
                    candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 500).Take(500).ToList();
                    var sortTerm5m = BinanceHelper.ShortTermFC(candels, currentPrice);
                    shortTerm5m.DataPoints.Clear();
                    shortTerm5m.DataPoints.AddRange(
                        sortTerm5m.Where(s => s.Value != 0).Select(s => new CategoricalDataPoint((double)s.Value, k++)));
                    //minValue = Math.Min(minValue, sortTerm5m.Min(s => s.Value));
                    //maxValue = Math.Max(maxValue, sortTerm5m.Max(s => s.Value));
                }
                if (binanceModel.Candels_15min.Any() != false)
                {
                    int k = 0;
                    candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 500).Take(500).ToList();
                    var sortTerm15m = BinanceHelper.ShortTermFC(candels, currentPrice);
                    shortTerm15m.DataPoints.Clear();
                    shortTerm15m.DataPoints.AddRange(
                        sortTerm15m.Where(s => s.Value != 0).Select(s => new CategoricalDataPoint((double)s.Value, k++)));
                    //minValue = Math.Min(minValue, sortTerm15m.Min(s => s.Value));
                    //maxValue = Math.Max(maxValue, sortTerm15m.Max(s => s.Value));

                }
                if (binanceModel.Candels_30min.Any() != false)
                {
                    int k = 0;
                    candels = binanceModel.Candels_30min.Skip(binanceModel.Candels_30min.Count() - 500).Take(500).ToList();
                    var sortTerm30m = BinanceHelper.ShortTermFC(candels, currentPrice);
                    shortTerm30m.DataPoints.Clear();
                    shortTerm30m.DataPoints.AddRange(
                        sortTerm30m.Where(s => s.Value != 0).Select(s => new CategoricalDataPoint((double)s.Value, k++)));
                    //minValue = Math.Min(minValue, sortTerm30m.Min(s => s.Value));
                    //maxValue = Math.Max(maxValue, sortTerm30m.Max(s => s.Value));

                }
                if (binanceModel.Candels_60min.Any() != false)
                {
                    int k = 0;
                    candels = binanceModel.Candels_60min.Skip(binanceModel.Candels_60min.Count() - 500).Take(500).ToList();
                    var sortTerm1h = BinanceHelper.ShortTermFC(candels, currentPrice);
                    shortTerm1H.DataPoints.Clear();
                    shortTerm1H.DataPoints.AddRange(
                        sortTerm1h.Where(s => s.Value != 0).Select(s => new CategoricalDataPoint((double)s.Value, k++)));
                    //minValue = Math.Min(minValue, sortTerm1h.Min(s => s.Value));
                    //maxValue = Math.Max(maxValue, sortTerm1h.Max(s => s.Value));

                }
                if (binanceModel.Candels_4hour.Any() != false)
                {
                    int k = 0;
                    candels = binanceModel.Candels_4hour.Skip(binanceModel.Candels_4hour.Count() - 500).Take(500).ToList();
                    var sortTerm4H = BinanceHelper.ShortTermFC(candels, currentPrice);
                    shortTerm4H.DataPoints.Clear();
                    shortTerm4H.DataPoints.AddRange(
                        sortTerm4H.Where(s => s.Value != 0).Select(s => new CategoricalDataPoint((double)s.Value, k++)));
                    //minValue = Math.Min(minValue, sortTerm4H.Min(s => s.Value));
                    //maxValue = Math.Max(maxValue, sortTerm4H.Max(s => s.Value));

                }
                if (binanceModel.Candels_oneDay.Any() != false)
                {
                    int k = 0;
                    candels = binanceModel.Candels_oneDay.Skip(binanceModel.Candels_oneDay.Count() - 500).Take(500).ToList();
                    var sortTerm1D = BinanceHelper.ShortTermFC(candels, currentPrice);
                    shortTerm1D.DataPoints.Clear();
                    shortTerm1D.DataPoints.AddRange(
                        sortTerm1D.Where(s => s.Value != 0).Select(s => new CategoricalDataPoint((double)s.Value, k++)));
                    //minValue = Math.Min(minValue, sortTerm1D.Min(s => s.Value));
                    //maxValue = Math.Max(maxValue, sortTerm1D.Max(s => s.Value));

                }
                //ConfigureAxis((double)minValue, (double) maxValue, 0.1);
                ConfigureAxis((double)0.8, (double)1.2, 0.1);

                /*price.DataPoints.Clear();
                price.DataPoints.AddRange(candels.Select(c => new CategoricalDataPoint((double)c.Close, c.CloseTime)));*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void strategiesShortSfForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;

        }
    }
}
