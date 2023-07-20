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
    public partial class strategies02Form : Telerik.WinControls.UI.RadForm
    {
        List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(), KlineInterval.FiveMinutes.ToString(), KlineInterval.FifteenMinutes.ToString() };
        
        private string coinName = string.Empty;
        private BinanceModel binanceModel;
        private KlineInterval interval = KlineInterval.OneMinute;
        public Strategy02Business strategy02;
        private List<IBinanceKline> candels;
        private SignalState signalState = SignalState.wait;
        private int periodMA1 = 20;
        private int periodMA2 = 200;
        private int periodMA3 = 950;
        public strategies02Form()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

        CandlestickSeries candleSeries = new CandlestickSeries();
       // LineSeries price = new LineSeries();
        BarSeries histogram = new BarSeries();
        //LineSeries macd = new LineSeries();
        //LineSeries signal = new LineSeries();

        LineSeries indicatorMA1 = new LineSeries();
        LineSeries indicatorMA2 = new LineSeries();
        LineSeries indicatorMA3 = new LineSeries();

        LineSeries indicatorEMA1 = new LineSeries();

        LineSeries MajorHigh = new LineSeries();
        LineSeries MajorLow = new LineSeries();
        /*LineSeries highLine = new LineSeries();
        LineSeries lowLine = new LineSeries();
        LineSeries stopLost = new LineSeries();
        LineSeries takeProfit = new LineSeries();*/

        private delegate void UpdateFormDelegate(BinanceModel binanceModel);


        private void strategies02Form_Load(object sender, EventArgs e)
        {
            
            initAxis();
            InitSeries();
            WireEvents();
            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;
            this.radDropDownList4.DataSource = Intervals;

            radChartView1.BackColor = Color.AliceBlue;
            radChartView1.GetArea<CartesianArea>().ShowGrid = true;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall();
            coinName = this.radDropDownList1.SelectedItem.Text;
            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);
            int marginPercentage = (int)NUD_PercentageMargin.Value;

            strategy02 = new Strategy02Business(coinName, interval, marginPercentage);
            OnDataReadyEvent();
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

                strategy02.percentageMargin = (int)NUD_PercentageMargin.Value;
                strategy02.DataReady(binanceModel);

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
            if (strategy02 == null)
                return;
            try
            {

                if (signalState != strategy02.signalState)
                {
                    signalState = strategy02.signalState;
                    SystemSounds.Beep.Play();
                }
                if (listBox1.Items.Count != strategy02.messageList.Count)
                {
                    listBox1.Items.Clear();
                    foreach (string str in strategy02.messageList)
                        listBox1.Items.Add(str);
                }
                
                price_LB.Text = strategy02.currentPrice.ToString();
                lb_trade.Text = strategy02.generalState;
                if (strategy02.signalState == SignalState.BuyPosition)
                    createBuySignal();
                else if (strategy02.signalState == SignalState.SellPosition)
                    createSellSignal();
                else
                    closeScolpPosition();
                DrawChart();
                UpdateStockSeries();

            }
            catch (Exception ex)
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
            if (strategy02.samples == null)
                return;
           // price.DataPoints.Clear();
            histogram.DataPoints.Clear();
            MajorHigh.DataPoints.Clear();
            MajorLow.DataPoints.Clear();
          /*  lowLine.DataPoints.Clear();
            highLine.DataPoints.Clear();
            stopLost.DataPoints.Clear();
            takeProfit.DataPoints.Clear();*/


            var temp = new ConcurrentBag<CategoricalDataPoint>();

            temp.Add(new CategoricalDataPoint((double) strategy02.majorHighPrice.Value, strategy02.StartDate));
            temp.Add(new CategoricalDataPoint((double) strategy02.majorHighPrice.Value, strategy02.EndDate));
            temp.Add(new CategoricalDataPoint((double) strategy02.majorHighPrice.Value, strategy02.majorHighPrice.Time));
            MajorHigh.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy02.majorLowPrice.Value, strategy02.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.majorLowPrice.Value, strategy02.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.majorLowPrice.Value, strategy02.majorLowPrice.Time));
            MajorLow.DataPoints.AddRange(temp);
            /*
            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy02.takeProfitPrice, strategy02.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.takeProfitPrice, strategy02.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.takeProfitPrice, strategy02.majorHighPrice.Time));
            takeProfit.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy02.stopLostPrice, strategy02.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.stopLostPrice, strategy02.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.stopLostPrice, strategy02.majorLowPrice.Time));
            stopLost.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy02.lowPrice.Value, strategy02.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.lowPrice.Value, strategy02.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.lowPrice.Value, strategy02.lowPrice.Time));
            lowLine.DataPoints.AddRange(temp);

            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)strategy02.highPrice.Value, strategy02.StartDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.highPrice.Value, strategy02.EndDate));
            temp.Add(new CategoricalDataPoint((double)strategy02.highPrice.Value, strategy02.highPrice.Time));
            highLine.DataPoints.AddRange(temp);*/

            temp = new ConcurrentBag<CategoricalDataPoint>();
            strategy02.macdResult.AsParallel().ForAll(item => { temp.Add(new CategoricalDataPoint((double)(item.Histogram??0), item.Date)); });
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
            strategy02.samples.Where(s=>s.CloseTime>= strategy02.macdResult.First().Date).AsParallel().ForAll(item => 
            { temp.Add(new CategoricalDataPoint((double)item.ClosePrice, item.CloseTime)); });
            //price.DataPoints.AddRange(temp);
            
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
            candleSeries.HorizontalAxis = dateTimeCategoricalAxis1;
            candleSeries.VerticalAxis = linearAxis2;
            candleSeries.LegendTitle = "Candle";
           // candleSeries.BorderColor = Color.Black;
            candleSeries.BorderWidth = 4;
            candleSeries.BorderColor = Color.FromArgb(250, 102, 0);
            candleSeries.BackColor = Color.FromArgb(250, 102, 0);
            candleSeries.ForeColor = Color.FromArgb(0, 102, 250);
            candleSeries.BackColor3 = Color.FromArgb(0, 102, 250);
            candleSeries.OpenValueMember = "Open";
            candleSeries.HighValueMember = "High";
            candleSeries.LowValueMember = "Low";
            candleSeries.CloseValueMember = "Close";
            candleSeries.CategoryMember = "CloseTime";


            indicatorMA1.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorMA1.VerticalAxis = linearAxis2;
            indicatorMA1.BorderColor = Color.Red;
            indicatorMA1.CategoryMember = "Date";
            indicatorMA1.ValueMember = "Sma";

            indicatorMA2.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorMA2.VerticalAxis = linearAxis2;
            indicatorMA2.BorderColor = Color.Green;
            indicatorMA2.CategoryMember = "Date";
            indicatorMA2.ValueMember = "Sma";

            indicatorMA3.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorMA3.VerticalAxis = linearAxis2;
            indicatorMA3.BorderColor = Color.Blue;
            indicatorMA3.CategoryMember = "Date";
            indicatorMA3.ValueMember = "Sma";

            indicatorEMA1.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA1.VerticalAxis = linearAxis2;
            indicatorEMA1.BorderColor = Color.LightGreen;
            indicatorEMA1.CategoryMember = "Date";
            indicatorEMA1.ValueMember = "Ema";

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
            /*
            highLine.HorizontalAxis = dateTimeCategoricalAxis1;
            highLine.VerticalAxis = linearAxis2;
            highLine.LegendTitle = "Local High ";
            highLine.BorderColor = Color.AliceBlue;
            highLine.BorderWidth = 4;
            highLine.PointSize = new SizeF(12, 12);

            lowLine.HorizontalAxis = dateTimeCategoricalAxis1;
            lowLine.VerticalAxis = linearAxis2;
            lowLine.LegendTitle = "Local Low ";
            lowLine.BorderColor = Color.BurlyWood;
            lowLine.BorderWidth = 4;
            lowLine.PointSize = new SizeF(12, 12);

            takeProfit.HorizontalAxis = dateTimeCategoricalAxis1;
            takeProfit.VerticalAxis = linearAxis2;
            takeProfit.LegendTitle = "Take Profit ";
            takeProfit.BorderColor = Color.LightGreen;
            takeProfit.BorderWidth = 4;
            takeProfit.PointSize = new SizeF(16, 16);

            stopLost.HorizontalAxis = dateTimeCategoricalAxis1;
            stopLost.VerticalAxis = linearAxis2;
            stopLost.LegendTitle = "Stop Loss ";
            stopLost.BorderColor = Color.DarkRed;
            stopLost.BorderWidth = 4;
            stopLost.PointSize = new SizeF(16, 16);*/

            histogram.HorizontalAxis = dateTimeCategoricalAxis1;
            histogram.VerticalAxis = linearAxis1;
            histogram.LegendTitle = "MACD_Histogram";
            histogram.BorderColor = Color.Red;
            histogram.BorderWidth = 4;

            tb_ma1_TextChanged(this, null);
            tb_ma2_TextChanged(this, null);
            tb_ma3_TextChanged(this, null);

            //radChartView1.Series.Add(price);
            radChartView1.Series.Add(candleSeries);
            radChartView1.Series.Add(indicatorMA1);
            radChartView1.Series.Add(indicatorMA2);
            radChartView1.Series.Add(indicatorMA3);
            radChartView1.Series.Add(indicatorEMA1);

            radChartView1.Series.Add(histogram);
            radChartView1.Series.Add(MajorHigh);
            radChartView1.Series.Add(MajorLow);

            //just for test
           /* radChartView1.Series.Add(takeProfit);
            radChartView1.Series.Add(stopLost);

            radChartView1.Series.Add(highLine);
            radChartView1.Series.Add(lowLine);*/

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
                if (strategy02 == null) return;
                UpdateData(binanceModel);
                setToInitialState();
            }
        }

        private void setToInitialState()
        {
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);

            strategy02 = new Strategy02Business(coinName, interval, (int)NUD_PercentageMargin.Value);
            OnDataReadyEvent();

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
                strategy02 = new Strategy02Business(coinName, interval, (int)NUD_PercentageMargin.Value);
                OnDataReadyEvent();
               // strategy02.DataReady(binanceModel);
                //UpdateData(binanceModel);
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
        private void UpdateStockSeries()
        {
            try
            {
                if (binanceModel == null) return;
                
                candleSeries.DataPoints.Clear();
                indicatorMA1.DataPoints.Clear();
                indicatorMA2.DataPoints.Clear();
                indicatorMA3.DataPoints.Clear();
                
                switch (interval)
                {
                    case KlineInterval.OneMinute:
                        candels = binanceModel.Candels_1min;
                        break;
                    case KlineInterval.FiveMinutes:
                        candels = binanceModel.Candels_5min;
                        break;
                    case KlineInterval.FifteenMinutes:
                        candels = binanceModel.Candels_15min;
                        break;
                }
                if (candels == null || candels.Any() == false) return;
                var candelsOnView = candels.Skip(candels.Count() - 100).Take(100).ToList();
                candleSeries.DataSource = candelsOnView;
                var max = candelsOnView.Max(v => v.HighPrice);
                var min = candelsOnView.Min(v => v.LowPrice);
               
                var Qutoes = BinanceHelper.GetQuotes(candels);
                var ma1 = BinanceHelper.SMA(Qutoes, periodMA1);
                var ma2 = BinanceHelper.SMA(Qutoes, periodMA2);
                var ma3 = BinanceHelper.SMA(Qutoes, periodMA3);

                var ema1 = BinanceHelper.EMA(Qutoes, periodMA1);

                var ma1OnView = ma1.Skip(ma1.Count() - 100).Take(100).ToList();
                var ma2OnView = ma2.Skip(ma2.Count() - 100).Take(100).ToList();
                var ma3OnView =  ma3.Skip(ma3.Count() - 100).Take(100).ToList();

                var ema2OnView = ema1.Skip(ema1.Count() - 100).Take(100).ToList();

                indicatorMA1.DataSource = ma1OnView;
                indicatorMA2.DataSource = ma2OnView;
                indicatorMA3.DataSource = ma3OnView;

                indicatorEMA1.DataSource = ema2OnView;
                var mamin = new List<decimal?> { ma1OnView.Min(x => x.Sma), ma2OnView.Min(x => x.Sma), ma3OnView.Min(x => x.Sma), min, strategy02.majorLowPrice.Value };
                var mamax = new List<decimal?> { ma1OnView.Max(x => x.Sma), ma2OnView.Max(x => x.Sma), ma3OnView.Max(x => x.Sma), max , strategy02.majorHighPrice.Value};

                min = mamin.Min().Value;
                max = mamax.Max().Value;
                if (max - min < 3)
                {
                    linearAxis2.Maximum = (double)max;
                    linearAxis2.Minimum = (double)min;
                    linearAxis2.MajorStep = (double)((max - min) / 10);
                }
                else
                {
                    linearAxis2.Maximum = (double)Math.Ceiling(max);
                    linearAxis2.Minimum = (double)Math.Floor(min);
                    linearAxis2.MajorStep = Math.Ceiling((linearAxis2.Maximum - linearAxis2.Minimum) / 10);
                }
                
            }
            catch (Exception ex)
            { }
        }
                
        private void tb_macd_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_fastPeriod.Text, out int fastPeriod) == false)
                fastPeriod = 12;
            if (int.TryParse(tb_slowPeriod.Text, out int slowPeriod) == false)
                slowPeriod = 12;
            if (int.TryParse(tb_signalPeriod.Text, out int signalPeriod) == false)
                signalPeriod = 12;

            strategy02.SetMacdParams(fastPeriod, slowPeriod, signalPeriod);
        }

        private void tb_ma1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_ma1.Text, out periodMA1) == false)
                periodMA1 = 20;
            indicatorMA1.LegendTitle = "MA" + periodMA1.ToString();
            indicatorEMA1.LegendTitle = "EMA" + periodMA1.ToString();

        }

        private void tb_ma2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_ma2.Text, out  periodMA2) == false)
                periodMA2 = 200;
            indicatorMA2.LegendTitle = "MA" + periodMA2.ToString();
        }

        private void tb_ma3_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_ma3.Text, out periodMA3) == false)
                periodMA3 = 950;
            indicatorMA3.LegendTitle = "MA" + periodMA3.ToString();
        }

        private void strategies02Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;

        }
    }
}
