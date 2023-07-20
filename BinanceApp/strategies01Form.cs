using Binance.Net;
using Binance.Net.Enums;

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
    public partial class strategies01Form : Telerik.WinControls.UI.RadForm
    {
        List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(), KlineInterval.FiveMinutes.ToString(), KlineInterval.FifteenMinutes.ToString() };
        
        private string coinName = string.Empty;
        private BinanceModel binanceModel;
        private KlineInterval interval = KlineInterval.OneMinute;
        public Strategy01Business strategy01;

        private SignalState signalState = SignalState.wait;

        public strategies01Form()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

 //       decimal majorHighPrice;
 //       decimal majorLowPrice;
 //       decimal currentPrice;

 //       decimal stopLostPrice = 0;
 //       decimal takeProfitPrice = 0;

 //       decimal lowPrice = 0;
 //       decimal highPrice = 0;

        LineSeries price = new LineSeries();
        BarSeries histogram = new BarSeries();
        //LineSeries macd = new LineSeries();
        //LineSeries signal = new LineSeries();


        LineSeries MajorHigh = new LineSeries();
        LineSeries MajorLow = new LineSeries();
        LineSeries highLine = new LineSeries();
        LineSeries lowLine = new LineSeries();
        LineSeries stopLost = new LineSeries();
        LineSeries takeProfit = new LineSeries();

        private delegate void UpdateFormDelegate(BinanceModel binanceModel);


        private void strategies01Form_Load(object sender, EventArgs e)
        {
            
            initAxis();
            InitSeries();
            WireEvents();
            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;
            this.radDropDownList4.DataSource = Intervals;

            radChartView1.BackColor = Color.LightGray;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall();
            coinName = this.radDropDownList1.SelectedItem.Text;
            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);
            int marginPercentage = (int)NUD_PercentageMargin.Value;

            strategy01 = new Strategy01Business(coinName, interval, marginPercentage);
 
            sendLog("Initializing ...");
        }

        private void sendLog(string strLog)
        {

        }
        
        
        private void OnDataReadyEvent()
        {
            try
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

                strategy01.percentageMargin = (int)NUD_PercentageMargin.Value;
                strategy01.DataReady(binanceModel);

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
            if (strategy01 == null)
                return;
            try
            {
                if(signalState != strategy01.signalState)
                {
                    signalState = strategy01.signalState;
                    SystemSounds.Beep.Play();
                }
                if (listBox1.Items.Count != strategy01.messageList.Count)
                {
                    listBox1.Items.Clear();
                    foreach (string str in strategy01.messageList)
                        listBox1.Items.Add(str);
                }
                
                price_LB.Text = strategy01.currentPrice.ToString();
                lb_trade.Text = strategy01.generalState;
                if (strategy01.signalState == SignalState.BuyPosition)
                    createBuySignal();
                else if (strategy01.signalState == SignalState.SellPosition)
                    createSellSignal();
                else
                    closeScolpPosition();
                DrawChart();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        private void closeScolpPosition()
        {

        }

        private void createSellSignal()
        {
            lb_trade.Text = "Sell Position";
            BTN_Sell.BackColor = Color.Red;
            BTN_Buy.BackColor = Color.Gray;
        }

        private void createBuySignal()
        {
            lb_trade.Text = "Buy Position";
            BTN_Buy.BackColor = Color.Blue;
            BTN_Sell.BackColor = Color.Gray;
        }

        private void DrawChart()
        {
            if (strategy01.samples == null)
                return;
            price.DataPoints.Clear();
            histogram.DataPoints.Clear();
            MajorHigh.DataPoints.Clear();
            MajorLow.DataPoints.Clear();
            lowLine.DataPoints.Clear();
            highLine.DataPoints.Clear();
            stopLost.DataPoints.Clear();
            takeProfit.DataPoints.Clear();


            var temp = new ConcurrentBag<CategoricalDataPoint>();

            temp.Add(new CategoricalDataPoint((double) strategy01.majorHighPrice.Value, strategy01.StartDate));
            temp.Add(new CategoricalDataPoint((double) strategy01.majorHighPrice.Value, strategy01.EndDate));
            temp.Add(new CategoricalDataPoint((double) strategy01.majorHighPrice.Value, strategy01.majorHighPrice.Time));
            MajorHigh.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy01.majorLowPrice.Value, strategy01.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.majorLowPrice.Value, strategy01.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.majorLowPrice.Value, strategy01.majorLowPrice.Time));
            MajorLow.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy01.takeProfitPrice, strategy01.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.takeProfitPrice, strategy01.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.takeProfitPrice, strategy01.majorHighPrice.Time));
            takeProfit.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy01.stopLostPrice, strategy01.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.stopLostPrice, strategy01.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.stopLostPrice, strategy01.majorLowPrice.Time));
            stopLost.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy01.lowPrice.Value, strategy01.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.lowPrice.Value, strategy01.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.lowPrice.Value, strategy01.lowPrice.Time));
            lowLine.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy01.highPrice.Value, strategy01.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.highPrice.Value, strategy01.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy01.highPrice.Value, strategy01.highPrice.Time));
            highLine.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            strategy01.macdResult.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)(item.Histogram??0), item.Date)); });
            histogram.DataPoints.AddRange( temp);
            linearAxis1.Maximum = Math.Ceiling(temp.Max(t => t.Value).Value) * 5;
            linearAxis1.Minimum = Math.Floor(temp.Min(t => t.Value).Value);

            //temp = new ConcurrentBag<CategoricalDataPoint>();
            //macdResult1.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Macd, item.Date)); });
            //macd.DataPoints.AddRange(temp);

            //temp = new ConcurrentBag<CategoricalDataPoint>();
            //macdResult1.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)item.Signal, item.Date)); });
            //signal.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            strategy01.samples.Where(s=>s.CloseTime>= strategy01.macdResult.First().Date).AsParallel().ForAll(item => 
            { temp.Add(new CategoricalDataPoint((double)item.ClosePrice, item.CloseTime)); });
            price.DataPoints.AddRange(temp);
            
            /*
            if (scolpState == ScolpState.initial)
            {
                if (radChartView1.Series.Where(s => s.Name == takeProfit.Name).Any())
                {
                    radChartView1.Series.Remove(takeProfit);
                    radChartView1.Series.Remove(stopLost);
                }
            }
            else
            {
                if (!radChartView1.Series.Where(s => s.Name == takeProfit.Name).Any())
                {
                    radChartView1.Series.Add(takeProfit);
                    radChartView1.Series.Add(stopLost);
                }
            }*/

        }
        private void initAxis()
        {
            dateTimeCategoricalAxis1.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis1.IsPrimary = true;
            dateTimeCategoricalAxis1.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis1.LabelFormat = "{0:HH:mm:ss}";
            dateTimeCategoricalAxis1.MajorTickInterval = 20;
            linearAxis1.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis1.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Right;
            linearAxis1.IsPrimary = true;
           // linearAxis1.MajorStep = 10D;
            //linearAxis1.Maximum = 30D;
            //linearAxis1.Minimum = -15D;
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
            price.HorizontalAxis = dateTimeCategoricalAxis1;
            price.VerticalAxis = linearAxis2;
            price.LegendTitle = "Price";
            price.BorderColor = Color.Black;
            price.BorderWidth = 4;



            MajorHigh.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorHigh.VerticalAxis = linearAxis2;
            MajorHigh.LegendTitle = "Major High";
            MajorHigh.BorderColor = Color.Green;
            MajorHigh.BorderWidth = 6;
            MajorHigh.PointSize = new SizeF(12,12);
 
            MajorLow.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorLow.VerticalAxis = linearAxis2;
            MajorLow.LegendTitle = "Major Low";
            MajorLow.BorderColor = Color.Red;
            MajorLow.BorderWidth = 6;
            MajorLow.PointSize = new SizeF(16, 16);

            highLine.HorizontalAxis = dateTimeCategoricalAxis1;
            highLine.VerticalAxis = linearAxis2;
            highLine.LegendTitle = "Local High";
            highLine.BorderColor = Color.AliceBlue;
            highLine.BorderWidth = 4;
            highLine.PointSize = new SizeF(12, 12);

            lowLine.HorizontalAxis = dateTimeCategoricalAxis1;
            lowLine.VerticalAxis = linearAxis2;
            lowLine.LegendTitle = "Local Low";
            lowLine.BorderColor = Color.BurlyWood;
            lowLine.BorderWidth = 4;
            lowLine.PointSize = new SizeF(12, 12);

            takeProfit.HorizontalAxis = dateTimeCategoricalAxis1;
            takeProfit.VerticalAxis = linearAxis2;
            takeProfit.LegendTitle = "Take Profit";
            takeProfit.BorderColor = Color.LightGreen;
            takeProfit.BorderWidth = 4;
            takeProfit.PointSize = new SizeF(16, 16);

            stopLost.HorizontalAxis = dateTimeCategoricalAxis1;
            stopLost.VerticalAxis = linearAxis2;
            stopLost.LegendTitle = "Stop Loss";
            stopLost.BorderColor = Color.DarkRed;
            stopLost.BorderWidth = 4;
            stopLost.PointSize = new SizeF(16, 16);

            histogram.HorizontalAxis = dateTimeCategoricalAxis1;
            histogram.VerticalAxis = linearAxis1;
            histogram.LegendTitle = "MACD_Histogram";
            histogram.BorderColor = Color.Red;
            histogram.BorderWidth = 4;


            radChartView1.Series.Add(price);
            radChartView1.Series.Add(histogram);
            radChartView1.Series.Add(MajorHigh);
            radChartView1.Series.Add(MajorLow);

            //just for test
            radChartView1.Series.Add(takeProfit);
            radChartView1.Series.Add(stopLost);

            radChartView1.Series.Add(highLine);
            radChartView1.Series.Add(lowLine);

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
                binanceModel = coinInfo;
                if (strategy01 == null) return;
                UpdateData(binanceModel);
                setToInitialState();
            }
        }

        private void setToInitialState()
        {
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);

            strategy01 = new Strategy01Business(coinName, interval, (int)NUD_PercentageMargin.Value);
            BTN_Buy.BackColor = Color.Gray;
            BTN_Sell.BackColor = Color.Gray;
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
                strategy01 = new Strategy01Business(coinName, interval, (int)NUD_PercentageMargin.Value);
                strategy01.DataReady(binanceModel);
                UpdateData(binanceModel);
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

        private void ResetTrading_BT_Click(object sender, EventArgs e)
        {
            setToInitialState();
        }

        private void txt_Percentage_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;
            string price = listBox1.SelectedItem.ToString();
            if (!price.Contains("="))
                return;

            var arr = price.Split('=');
            var number = arr.Last();
            number.Replace(" ", string.Empty);
            System.Windows.Forms.Clipboard.SetText(number);
            SystemSounds.Beep.Play();
        }

        private void NUD_PercentageMargin_ValueChanged(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
        }

        private void strategies01Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;

        }

        
    }
}
