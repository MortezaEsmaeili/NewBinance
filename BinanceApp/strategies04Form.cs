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
    public partial class strategies04Form : Telerik.WinControls.UI.RadForm
    {
        List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(),
            KlineInterval.FiveMinutes.ToString(), KlineInterval.FifteenMinutes.ToString(),
            KlineInterval.ThirtyMinutes.ToString(), KlineInterval.OneHour.ToString(),
            KlineInterval.FourHour.ToString(), KlineInterval.OneDay.ToString()};

        private string coinName = string.Empty;
        private BinanceModel binanceModel;
        private KlineInterval interval = KlineInterval.OneMinute;

        private List<IBinanceKline> candels;

        private int periodEMA1 = 5;
        private int periodEMA2 = 25;
        private int periodEMA3 = 75;
        private int periodEMA4 = 150;
        private int periodEMA5 = 300;
        private int periodEMA6 = 600;

        public strategies04Form()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();

        CandlestickSeries candleSeries = new CandlestickSeries();
       
        LineSeries indicatorEMA1 = new LineSeries();
        LineSeries indicatorEMA2 = new LineSeries();
        LineSeries indicatorEMA3 = new LineSeries();
        LineSeries indicatorEMA4 = new LineSeries();
        LineSeries indicatorEMA5 = new LineSeries();
        LineSeries indicatorEMA6 = new LineSeries();

        SteplineSeries MajorHigh1Min = new SteplineSeries();
        SteplineSeries MajorLow1Min = new SteplineSeries();

        SteplineSeries MajorHigh5Min = new SteplineSeries();
        SteplineSeries MajorLow5Min = new SteplineSeries();

        SteplineSeries MajorHigh15Min = new SteplineSeries();
        SteplineSeries MajorLow15Min = new SteplineSeries();

        SteplineSeries MajorHigh30Min = new SteplineSeries();
        SteplineSeries MajorLow30Min = new SteplineSeries();

        SteplineSeries MajorHigh60Min = new SteplineSeries();
        SteplineSeries MajorLow60Min = new SteplineSeries();


        private delegate void UpdateFormDelegate(BinanceModel binanceModel);

        private void strategies04Form_Load(object sender, EventArgs e)
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
            
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);

            BinanceDataCollector.Instance.CandleReadyEvent += OnDataReadyEvent;
            OnDataReadyEvent(interval, coinName);
            sendLog("Initializing ...");
        }

        private void sendLog(string strLog)
        {

        }
        
        
        private void OnDataReadyEvent(KlineInterval interval, string coin)
        {
            try
            {
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
          //  radChartView1.Area.View.Palette = KnownPalette.Warm;
            radChartView1.ShowLegend = true;

        }

        private void InitSeries()
        {
            candleSeries.HorizontalAxis = dateTimeCategoricalAxis1;
            candleSeries.VerticalAxis = linearAxis2;
            candleSeries.LegendTitle = "Candle";
            candleSeries.BorderWidth = 4;
            candleSeries.BorderColor = Color.Gray;
            candleSeries.BackColor = Color.Gray;
            candleSeries.ForeColor = Color.Gray;
            candleSeries.BackColor3 = Color.Gray;
            candleSeries.OpenValueMember = "Open";
            candleSeries.HighValueMember = "High";
            candleSeries.LowValueMember = "Low";
            candleSeries.CloseValueMember = "Close";
            candleSeries.CategoryMember = "CloseTime";

            indicatorEMA1.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA1.VerticalAxis = linearAxis2;
            indicatorEMA1.BorderColor = Color.Red;
            indicatorEMA1.CategoryMember = "Date";
            indicatorEMA1.ValueMember = "Ema";

            indicatorEMA2.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA2.VerticalAxis = linearAxis2;
            indicatorEMA2.BorderColor = Color.Green;
            indicatorEMA2.CategoryMember = "Date";
            indicatorEMA2.ValueMember = "Ema";

            indicatorEMA3.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA3.VerticalAxis = linearAxis2;
            indicatorEMA3.BorderColor = Color.Blue;
            indicatorEMA3.CategoryMember = "Date";
            indicatorEMA3.ValueMember = "Ema";

            indicatorEMA4.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA4.VerticalAxis = linearAxis2;
            indicatorEMA4.BorderColor = Color.Orange;
            indicatorEMA4.CategoryMember = "Date";
            indicatorEMA4.ValueMember = "Ema";

            indicatorEMA5.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA5.VerticalAxis = linearAxis2;
            indicatorEMA5.BorderColor = Color.Purple;
            indicatorEMA5.CategoryMember = "Date";
            indicatorEMA5.ValueMember = "Ema";

            indicatorEMA6.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA6.VerticalAxis = linearAxis2;
            indicatorEMA6.BorderColor = Color.Brown;
            indicatorEMA6.CategoryMember = "Date";
            indicatorEMA6.ValueMember = "Ema";


            tb_ma1_TextChanged(this, null);
            tb_ma2_TextChanged(this, null);
            tb_ma3_TextChanged(this, null);
            tb_4_TextChanged(this, null);
            tb_5_TextChanged(this, null);
            tb_6_TextChanged(this, null);

            MajorHigh1Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorHigh1Min.VerticalAxis = linearAxis2;
            MajorHigh1Min.LegendTitle = "MH  1  Min";
            MajorHigh1Min.BorderColor = Color.Green;
            MajorHigh1Min.BackColor = Color.Green;
            MajorHigh1Min.BorderWidth = 2;


            MajorLow1Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorLow1Min.VerticalAxis = linearAxis2;
            MajorLow1Min.LegendTitle = "ML   1  Min";
            MajorLow1Min.BorderColor = Color.Green;
            MajorLow1Min.BackColor = Color.Green;
            MajorLow1Min.BorderWidth = 2;
            MajorLow1Min.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            MajorHigh5Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorHigh5Min.VerticalAxis = linearAxis2;
            MajorHigh5Min.LegendTitle = "MH  5  Min";
            MajorHigh5Min.BorderColor = Color.Blue;
            MajorHigh5Min.BackColor = Color.Blue;
            MajorHigh5Min.BorderWidth = 2;

            MajorLow5Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorLow5Min.VerticalAxis = linearAxis2;
            MajorLow5Min.LegendTitle = "ML   5  Min";
            MajorLow5Min.BorderColor = Color.Blue;
            MajorLow5Min.BackColor = Color.Blue;
            MajorLow5Min.BorderWidth = 2;
            MajorLow5Min.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            MajorHigh15Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorHigh15Min.VerticalAxis = linearAxis2;
            MajorHigh15Min.LegendTitle = "MH  15 Min";
            MajorHigh15Min.BorderColor = Color.Orange;
            MajorHigh15Min.BackColor = Color.Orange;
            MajorHigh15Min.BorderWidth = 2;

            MajorLow15Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorLow15Min.VerticalAxis = linearAxis2;
            MajorLow15Min.LegendTitle = "ML   15 Min";
            MajorLow15Min.BorderColor = Color.Orange;
            MajorLow15Min.BackColor = Color.Orange;
            MajorLow15Min.BorderWidth = 2;
            MajorLow15Min.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            MajorHigh30Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorHigh30Min.VerticalAxis = linearAxis2;
            MajorHigh30Min.LegendTitle = "MH  30 Min";
            MajorHigh30Min.BorderColor = Color.Red;
            MajorHigh30Min.BackColor = Color.Red;
            MajorHigh30Min.BorderWidth = 2;

            MajorLow30Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorLow30Min.VerticalAxis = linearAxis2;
            MajorLow30Min.LegendTitle = "ML   30 Min";
            MajorLow30Min.BorderColor = Color.Red;
            MajorLow30Min.BackColor = Color.Red;
            MajorLow30Min.BorderWidth = 2;
            MajorLow30Min.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            MajorHigh60Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorHigh60Min.VerticalAxis = linearAxis2;
            MajorHigh60Min.LegendTitle = "MH  60 Min";
            MajorHigh60Min.BorderColor = Color.Purple;
            MajorHigh60Min.BackColor = Color.Purple;
            MajorHigh60Min.BorderWidth = 2;

            MajorLow60Min.HorizontalAxis = dateTimeCategoricalAxis1;
            MajorLow60Min.VerticalAxis = linearAxis2;
            MajorLow60Min.LegendTitle = "ML   60 Min";
            MajorLow60Min.BorderColor = Color.Purple;
            MajorLow60Min.BackColor = Color.Purple;
            MajorLow60Min.BorderWidth = 2;
            MajorLow60Min.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
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
                UpdateData(binanceModel);
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
                OnDataReadyEvent(interval, coinName);
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
        bool underProcess = false;
        private void UpdateStockSeries()
        {
            if (underProcess == true) return;
            underProcess = true;
            radChartView1.Series.Clear(); 

            radChartView1.Series.Add(candleSeries);

            if (CB_1.Checked) 
                radChartView1.Series.Add(indicatorEMA1);
            if (CB_2.Checked) 
                radChartView1.Series.Add(indicatorEMA2);
            if (CB_3.Checked) 
                radChartView1.Series.Add(indicatorEMA3);
            if (CB_4.Checked) 
                radChartView1.Series.Add(indicatorEMA4);
            if (CB_5.Checked) 
                radChartView1.Series.Add(indicatorEMA5);
            if (CB_6.Checked) 
                radChartView1.Series.Add(indicatorEMA6);

            if (CB_MHL1.Checked)
            {
                radChartView1.Series.Add(MajorHigh1Min);
                radChartView1.Series.Add(MajorLow1Min);
            }
            if (CB_MHL5.Checked)
            {
                radChartView1.Series.Add(MajorHigh5Min);
                radChartView1.Series.Add(MajorLow5Min);
            }
            if (CB_MHL15.Checked)
            {
                radChartView1.Series.Add(MajorHigh15Min);
                radChartView1.Series.Add(MajorLow15Min);
            }
            if (CB_MHL30.Checked)
            {
                radChartView1.Series.Add(MajorHigh30Min);
                radChartView1.Series.Add(MajorLow30Min);
            }
            if (CB_MHL60.Checked)
            {
                radChartView1.Series.Add(MajorHigh60Min);
                radChartView1.Series.Add(MajorLow60Min);
            }
            DateTime candleStartDate = DateTime.Now.AddDays(-1);
            try
            {
                if (binanceModel == null) return;
                
                candleSeries.DataPoints.Clear();
                indicatorEMA1.DataPoints.Clear();
                indicatorEMA2.DataPoints.Clear();
                indicatorEMA3.DataPoints.Clear();
                indicatorEMA4.DataPoints.Clear();
                indicatorEMA5.DataPoints.Clear();
                indicatorEMA6.DataPoints.Clear();


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
                int sampleCount = 600;
                if (candels == null || candels.Any() == false) return;
                var candelsOnView = candels.Skip(candels.Count() - sampleCount).Take(sampleCount).ToList();
                candleSeries.DataSource = candelsOnView;
                var max = candelsOnView.Max(v => v.HighPrice);
                var min = candelsOnView.Min(v => v.LowPrice);

                candleStartDate = candelsOnView.First().CloseTime;
                var Qutoes = BinanceHelper.GetQuotes(candels);
                var ema1 = BinanceHelper.EMA(Qutoes, periodEMA1);
                var ema2 = BinanceHelper.EMA(Qutoes, periodEMA2);
                var ema3 = BinanceHelper.EMA(Qutoes, periodEMA3);
                var ema4 = BinanceHelper.EMA(Qutoes, periodEMA4);
                var ema5 = BinanceHelper.EMA(Qutoes, periodEMA5);
                var ema6 = BinanceHelper.EMA(Qutoes, periodEMA6);


                var ema1OnView = ema1.Skip(ema1.Count() - sampleCount).Take(sampleCount).ToList();
                var ema2OnView = ema2.Skip(ema2.Count() - sampleCount).Take(sampleCount).ToList();
                var ema3OnView = ema3.Skip(ema3.Count() - sampleCount).Take(sampleCount).ToList();
                var ema4OnView = ema4.Skip(ema4.Count() - sampleCount).Take(sampleCount).ToList();
                var ema5OnView = ema5.Skip(ema5.Count() - sampleCount).Take(sampleCount).ToList();
                var ema6OnView = ema5.Skip(ema6.Count() - sampleCount).Take(sampleCount).ToList();

                //var dema1 = BinanceHelper.CalulateDerivative(ema1OnView);


                indicatorEMA1.DataSource = ema1OnView;
                indicatorEMA2.DataSource = ema2OnView;
                indicatorEMA3.DataSource = ema3OnView;
                indicatorEMA4.DataSource = ema4OnView;
                indicatorEMA5.DataSource = ema5OnView;
                indicatorEMA6.DataSource = ema6OnView;
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                underProcess = false;
            }
            try
            {
                
                if (binanceModel.Candels_1min == null || binanceModel.Candels_1min.Count == 0 || binanceModel.MacdResult1Min == null)
                    return;

                price_LB.Text = binanceModel.CurrentPrice.ToString();
                var startTime = binanceModel.MacdResult1Min.First().Date;
                if (startTime < candleStartDate)
                    startTime = candleStartDate;
                var endTime = binanceModel.MacdResult1Min.Last().Date;
                decimal mh1min = 0, ml1min = 0, mh5min = 0, ml5min = 0, mh15min = 0, ml15min = 0;
                decimal mh30min = 0, ml30min = 0, mh60min = 0, ml60min = 0;
                //1 min
                var temp_H1 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_L1 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_H5 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_L5 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_H15 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_L15 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_H30 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_L30 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_H60 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_L60 = new ConcurrentBag<CategoricalDataPoint>();
                var temp_temp = new ConcurrentBag<CategoricalDataPoint>();
                temp_H1 = GetMarginPoints(binanceModel.MajorHighPrice1Min, startTime, endTime, out mh1min);
                if (temp_H1 != null && temp_H1.Any())
                {
                    MajorHigh1Min.DataPoints.Clear();
                    MajorHigh1Min.DataPoints.AddRange(temp_H1);
                }

                temp_L1 = GetMarginPoints(binanceModel.MajorLowPrice1Min, startTime, endTime, out ml1min);
                if (temp_L1 != null && temp_L1.Any())
                {
                    MajorLow1Min.DataPoints.Clear();
                    MajorLow1Min.DataPoints.AddRange(temp_L1);
                }
                //5 min
                temp_H5 = GetMarginPoints(binanceModel.MajorHighPrice5Min, startTime, endTime, out mh5min);
                if (temp_H5 != null && temp_H5.Any())
                {
                    MajorHigh5Min.DataPoints.Clear();
                    MajorHigh5Min.DataPoints.AddRange(temp_H5);
                }

                temp_L5 = GetMarginPoints(binanceModel.MajorLowPrice5Min, startTime, endTime, out ml5min);
                if (temp_L5 != null && temp_L5.Any())
                {
                    MajorLow5Min.DataPoints.Clear();
                    MajorLow5Min.DataPoints.AddRange(temp_L5);
                }

                //15 min
                temp_H15 = GetMarginPoints(binanceModel.MajorHighPrice15Min, startTime, endTime, out mh15min);
                if (temp_H15 != null && temp_H15.Any())
                {
                    MajorHigh15Min.DataPoints.Clear();
                    MajorHigh15Min.DataPoints.AddRange(temp_H15);
                }

                temp_L15 = GetMarginPoints(binanceModel.MajorLowPrice15Min, startTime, endTime, out ml15min);
                if (temp_L15 != null && temp_L15.Any())
                {
                    MajorLow15Min.DataPoints.Clear();
                    MajorLow15Min.DataPoints.AddRange(temp_L15);
                }

                //30 min
                temp_H30 = GetMarginPoints(binanceModel.MajorHighPrice30Min, startTime, endTime, out mh30min);
                if (temp_H30 != null && temp_H30.Any())
                {
                    MajorHigh30Min.DataPoints.Clear();
                    MajorHigh30Min.DataPoints.AddRange(temp_H30);
                }

                temp_L30 = GetMarginPoints(binanceModel.MajorLowPrice30Min, startTime, endTime, out ml30min);
                if (temp_L30 != null && temp_L30.Any())
                {
                    MajorLow30Min.DataPoints.Clear();
                    MajorLow30Min.DataPoints.AddRange(temp_L30);
                }
       
                //60 min
                temp_H60 = GetMarginPoints(binanceModel.MajorHighPrice60Min, startTime, endTime, out mh60min);
                if (temp_H60 != null && temp_H60.Any())
                {
                    MajorHigh60Min.DataPoints.Clear();
                    MajorHigh60Min.DataPoints.AddRange(temp_H60);
                }

                temp_L60 = GetMarginPoints(binanceModel.MajorLowPrice60Min, startTime, endTime, out ml60min);
                if (temp_L60 != null && temp_L60.Any())
                {
                    MajorLow60Min.DataPoints.Clear();
                    MajorLow60Min.DataPoints.AddRange(temp_L60);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                underProcess = false;
            }
        }

        private void tb_ma1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_1.Text, out periodEMA1) == false)
                periodEMA1 = 5;
            indicatorEMA1.LegendTitle = "EMA " + periodEMA1.ToString();
            UpdateStockSeries();
        }

        private void tb_ma2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_2.Text, out  periodEMA2) == false)
                periodEMA2 = 25;
            indicatorEMA2.LegendTitle = "EMA " + periodEMA2.ToString();
            UpdateStockSeries();
        }

        private void tb_ma3_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_3.Text, out periodEMA3) == false)
                periodEMA3 = 75;
            indicatorEMA3.LegendTitle = "EMA " + periodEMA3.ToString();
            UpdateStockSeries();
        }

        private void tb_4_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_4.Text, out periodEMA4) == false)
                periodEMA4 = 150;
            indicatorEMA4.LegendTitle = "EMA " + periodEMA4.ToString();
            UpdateStockSeries();
        }

        private void tb_5_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_5.Text, out periodEMA5) == false)
                periodEMA5 = 300;
            indicatorEMA5.LegendTitle = "EMA " + periodEMA5.ToString();
            UpdateStockSeries();
        }

        private void tb_6_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_6.Text, out periodEMA6) == false)
                periodEMA6 = 600;
            indicatorEMA6.LegendTitle = "EMA " + periodEMA6.ToString();
            UpdateStockSeries();
        }

        private void CB_1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStockSeries();
        }

        private void strategies04Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.CandleReadyEvent -= OnDataReadyEvent;

        }

        
    }
}
