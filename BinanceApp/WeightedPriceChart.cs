using Binance.Net;

using BinanceApp.Business;
using BinanceApp.DataModel;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Concurrent;
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

namespace BinanceApp
{
    public partial class WeightedPriceChart : Telerik.WinControls.UI.RadForm
    {
        public WeightedPriceChart()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();

        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

        LineSeries price = new LineSeries();
        LineSeries AskWP5 = new LineSeries();
        LineSeries AskWP10 = new LineSeries();
        LineSeries AskWP20 = new LineSeries();
        LineSeries AskWP50 = new LineSeries();

        LineSeries BidsWP5 = new LineSeries();
        LineSeries BidsWP10 = new LineSeries();
        LineSeries BidsWP20 = new LineSeries();
        LineSeries BidsWP50 = new LineSeries();

        LineSeries MP50 = new LineSeries();

        private string coinName = string.Empty;



        private void WeightedPriceChart_Load(object sender, EventArgs e)
        {
            
            initAxis();
            InitSeries();
            WireEvents();
            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;

            radChartView1.BackColor = Color.LightGray;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall();
            coinName = this.radDropDownList1.SelectedItem.Text;

            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
        }
        private  delegate  void UpdateFormDelegate(BinanceModel binanceModel);
        private void OnDataReadyEvent()
        {
            try
            {
                
                if (!string.IsNullOrWhiteSpace(coinName))
                {
                    var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                    if (coinInfo == null) return;
                    if (InvokeRequired)
                    {
                        var d = new UpdateFormDelegate(UpdateData);
                        this.Invoke(d, coinInfo);
                    }
                    else
                        UpdateData(coinInfo);
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void UpdateData(BinanceModel binanceModel)
        {
            try
            {
                radChartView1.Series.Clear();
                MP50.DataPoints.Clear();
                price.DataPoints.Clear();
                AskWP5.DataPoints.Clear();
                AskWP10.DataPoints.Clear();
                AskWP20.DataPoints.Clear();
                AskWP50.DataPoints.Clear();
                BidsWP5.DataPoints.Clear();
                BidsWP10.DataPoints.Clear();
                BidsWP20.DataPoints.Clear();
                BidsWP50.DataPoints.Clear();

                ConcurrentBag<CategoricalDataPoint> temp = new ConcurrentBag<CategoricalDataPoint>();

                binanceModel.PriceList.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                price.DataPoints.AddRange(temp);
                radChartView1.Series.Add(price);

                temp = new ConcurrentBag<CategoricalDataPoint>();
                binanceModel.MP50.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                MP50.DataPoints.AddRange(temp);
                radChartView1.Series.Add(MP50);


                if (chk_WP5.Checked)
                {
                    temp = new ConcurrentBag<CategoricalDataPoint>();
                    binanceModel.AskWightedPrice5.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                    AskWP5.DataPoints.AddRange(temp);
                    radChartView1.Series.Add(AskWP5);

                    temp = new ConcurrentBag<CategoricalDataPoint>();
                    binanceModel.BidsWightedPrice5.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                    BidsWP5.DataPoints.AddRange(temp);
                    radChartView1.Series.Add(BidsWP5);
                }
                if (chk_WP10.Checked)
                {
                    temp = new ConcurrentBag<CategoricalDataPoint>();
                    binanceModel.AskWightedPrice10.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                    AskWP10.DataPoints.AddRange(temp);
                    radChartView1.Series.Add(AskWP10);

                    temp = new ConcurrentBag<CategoricalDataPoint>();
                    binanceModel.BidsWightedPrice10.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                    BidsWP10.DataPoints.AddRange(temp);
                    radChartView1.Series.Add(BidsWP10);
                }
                if (chk_WP20.Checked)
                {
                    temp = new ConcurrentBag<CategoricalDataPoint>();
                    binanceModel.AskWightedPrice20.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                    AskWP20.DataPoints.AddRange(temp);
                    radChartView1.Series.Add(AskWP20);

                    temp = new ConcurrentBag<CategoricalDataPoint>();
                    binanceModel.BidsWightedPrice20.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                    BidsWP20.DataPoints.AddRange(temp);
                    radChartView1.Series.Add(BidsWP20);
                }
                if (chk_WP50.Checked)
                {
                    temp = new ConcurrentBag<CategoricalDataPoint>();
                    binanceModel.AskWightedPrice50.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                    AskWP50.DataPoints.AddRange(temp);
                    radChartView1.Series.Add(AskWP50);

                    temp = new ConcurrentBag<CategoricalDataPoint>();
                    binanceModel.BidsWightedPrice50.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Value, item.Time)); });
                    BidsWP50.DataPoints.AddRange(temp);
                    radChartView1.Series.Add(BidsWP50);
                }
                
            }
            catch(Exception ex)
            { }
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
            //linearAxis1.MajorStep = 10D;
            //linearAxis1.Maximum = 100D;
            //linearAxis1.Minimum = 0D;
            linearAxis1.TickOrigin = null;
            
            
            linearAxis2.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis2.IsPrimary = true;
            //linearAxis2.MajorStep = 5D;
            //linearAxis2.Maximum = 80D;
            //linearAxis2.Minimum = 50D;
            linearAxis2.TickOrigin = null;

            this.radChartView1.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1,
            linearAxis1, linearAxis2});

            LassoZoomController lassoZoomController = new LassoZoomController();
            radChartView1.Controllers.Add(lassoZoomController);
          //  radChartView1.Area.View.Palette = KnownPalette.Warm;
            radChartView1.ShowLegend = true;

        }

        private void InitSeries()
        {

            MP50.HorizontalAxis = dateTimeCategoricalAxis1;
            MP50.VerticalAxis = linearAxis1;
            MP50.LegendTitle = "MP50";
            MP50.BorderColor = Color.Gold;
            MP50.BorderWidth = 6;


            price.HorizontalAxis = dateTimeCategoricalAxis1;
            price.VerticalAxis = linearAxis2;
            price.LegendTitle = "Price";
            price.BorderColor = Color.Black;
            price.BorderWidth = 4;

            AskWP5.HorizontalAxis = dateTimeCategoricalAxis1;
            AskWP5.VerticalAxis = linearAxis2;
            AskWP5.LegendTitle = "Ask-WP5";
            AskWP5.BorderColor = Color.Blue;
            AskWP5.BorderWidth = 2;

            AskWP10.HorizontalAxis = dateTimeCategoricalAxis1;
            AskWP10.VerticalAxis = linearAxis2;
            AskWP10.LegendTitle = "Ask-WP10";
            AskWP10.BorderColor = Color.Green;
            AskWP10.BorderWidth = 2;

            AskWP20.HorizontalAxis = dateTimeCategoricalAxis1;
            AskWP20.VerticalAxis = linearAxis2;
            AskWP20.LegendTitle = "Ask-WP20";
            //AskWP20.BorderColor = Color.Red;
            //AskWP20.BackColor = Color.Red;
            AskWP20.BorderWidth = 2;

            AskWP50.HorizontalAxis = dateTimeCategoricalAxis1;
            AskWP50.VerticalAxis = linearAxis2;
            AskWP50.LegendTitle = "Ask-WP50";
            //AskWP50.BorderColor = Color.Brown;
            //AskWP50.BackColor = Color.Brown;
            AskWP50.BorderWidth = 2;

            BidsWP5.HorizontalAxis = dateTimeCategoricalAxis1;
            BidsWP5.VerticalAxis = linearAxis2;
            BidsWP5.LegendTitle = "Bid-WP5";
            BidsWP5.BorderColor = Color.Pink;
            BidsWP5.BorderWidth = 2;

            BidsWP10.HorizontalAxis = dateTimeCategoricalAxis1;
            BidsWP10.VerticalAxis = linearAxis2;
            BidsWP10.LegendTitle = "Bid-WP10";
            BidsWP10.BorderColor = Color.Red;
            BidsWP10.BorderWidth = 2;

            BidsWP20.HorizontalAxis = dateTimeCategoricalAxis1;
            BidsWP20.VerticalAxis = linearAxis2;
            BidsWP20.LegendTitle = "Bid-WP20";
            //BuyWP20.BorderColor = Color.Red;
            //BuyWP20.BackColor = Color.Red;
            BidsWP20.BorderWidth = 2;

            BidsWP50.HorizontalAxis = dateTimeCategoricalAxis1;
            BidsWP50.VerticalAxis = linearAxis2;
            BidsWP50.LegendTitle = "Bid-WP50";
            //BuyWP50.BorderColor = Color.Brown;
            //BuyWP50.BackColor = Color.Brown;
            BidsWP50.BorderWidth = 2;

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
                var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                if (coinInfo == null) return;

                UpdateData(coinInfo);
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

        private void WeightedPriceChart_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;

        }
    }
}
