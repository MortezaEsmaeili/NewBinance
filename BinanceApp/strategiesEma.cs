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
    public partial class strategiesEma : Telerik.WinControls.UI.RadForm
    {
               
        private string coinName = string.Empty;
        private BinanceModel binanceModel;
        private KlineInterval interval = KlineInterval.OneMinute;
        private double percent = 10;
        private int periodEMA = 20;
        private int numOfClasses = 4;
        public strategiesEma()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();

        CandlestickSeries candleSeries = new CandlestickSeries();

        LineSeries indicatorEMA_5m = new LineSeries();
        LineSeries indicatorEMA_15m = new LineSeries();
        LineSeries indicatorEMA_30m = new LineSeries();
        LineSeries indicatorEMA_1h = new LineSeries();
        LineSeries indicatorEMA_2h = new LineSeries();
        LineSeries indicatorEMA_4h = new LineSeries();
        LineSeries indicatorEMA_1D = new LineSeries();
        LineSeries indicatorEMA_1W = new LineSeries();

        LineSeries[] Margins;


        private delegate void UpdateFormDelegate(BinanceModel binanceModel, KlineInterval interval);

        private void strategiesEma_Load(object sender, EventArgs e)
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
            

            BinanceDataCollector.Instance.CandleReadyEvent += OnDataReadyEvent;
            BinanceDataCollector.Instance.DataReadyEvent += UpdatePrice;
            OnDataReadyEvent(interval, coinName);
        }

        private void UpdatePrice()
        {

            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { price_LB.Text = binanceModel.CurrentPrice.ToString(); }));
            }
            else

            {
                price_LB.Text = binanceModel.CurrentPrice.ToString();
            }
        }
       
        
        private void OnDataReadyEvent(KlineInterval interval, string coin)
        {
            try
            {
                if (coin != coinName) return;
                if (interval != KlineInterval.OneMinute) return;
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
                    this.Invoke(d, binanceModel, interval);
                }
                else
                    UpdateData(binanceModel, interval);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void UpdateData(BinanceModel binanceModel, KlineInterval interval)
        {
            try
            {
                UpdateStockSeries(interval);
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
            candleSeries.LegendTitle = "Candle 1min";
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

            indicatorEMA_5m.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA_5m.VerticalAxis = linearAxis2;
            indicatorEMA_5m.BorderColor = Color.GreenYellow;
            indicatorEMA_5m.CategoryMember = "Date";
            indicatorEMA_5m.ValueMember = "Ema";
            indicatorEMA_5m.LegendTitle = "Ema20-5m";

            indicatorEMA_15m.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA_15m.VerticalAxis = linearAxis2;
            indicatorEMA_15m.BorderColor = Color.Red;
            indicatorEMA_15m.CategoryMember = "Date";
            indicatorEMA_15m.ValueMember = "Ema";
            indicatorEMA_15m.LegendTitle = "Ema20-15m";

            indicatorEMA_30m.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA_30m.VerticalAxis = linearAxis2;
            indicatorEMA_30m.BorderColor = Color.Green;
            indicatorEMA_30m.CategoryMember = "Date";
            indicatorEMA_30m.ValueMember = "Ema";
            indicatorEMA_30m.LegendTitle = "Ema20-30m";

            indicatorEMA_1h.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA_1h.VerticalAxis = linearAxis2;
            indicatorEMA_1h.BorderColor = Color.Blue;
            indicatorEMA_1h.CategoryMember = "Date";
            indicatorEMA_1h.ValueMember = "Ema";
            indicatorEMA_1h.LegendTitle = "Ema20-1h";

            indicatorEMA_2h.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA_2h.VerticalAxis = linearAxis2;
            indicatorEMA_2h.BorderColor = Color.Orange;
            indicatorEMA_2h.CategoryMember = "Date";
            indicatorEMA_2h.ValueMember = "Ema";
            indicatorEMA_2h.LegendTitle = "Ema20-2h";

            indicatorEMA_4h.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA_4h.VerticalAxis = linearAxis2;
            indicatorEMA_4h.BorderColor = Color.Purple;
            indicatorEMA_4h.CategoryMember = "Date";
            indicatorEMA_4h.ValueMember = "Ema";
            indicatorEMA_4h.LegendTitle = "Ema20-4h";

            indicatorEMA_1D.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA_1D.VerticalAxis = linearAxis2;
            indicatorEMA_1D.BorderColor = Color.Brown;
            indicatorEMA_1D.CategoryMember = "Date";
            indicatorEMA_1D.ValueMember = "Ema";
            indicatorEMA_1D.LegendTitle = "Ema20-1D";

            indicatorEMA_1W.HorizontalAxis = dateTimeCategoricalAxis1;
            indicatorEMA_1W.VerticalAxis = linearAxis2;
            indicatorEMA_1W.BorderColor = Color.HotPink;
            indicatorEMA_1W.CategoryMember = "Date";
            indicatorEMA_1W.ValueMember = "Ema";
            indicatorEMA_1W.LegendTitle = "Ema20-1W";

            Margins = new LineSeries[7];
            
            for (int i = 0; i < 7; i++)
            {
                Margins[i] = new LineSeries(); 
                Margins[i].HorizontalAxis = dateTimeCategoricalAxis1;
                Margins[i].VerticalAxis = linearAxis2;
                Margins[i].CategoryMember = "Date";
                Margins[i].ValueMember = "Ema";
                Margins[i].BorderWidth = 0;
                Margins[i].PointSize = new SizeF(4,4);
                Margins[i].LegendTitle = "Margin" + i;
               // Margins[i].BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
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
                UpdateData(binanceModel, KlineInterval.OneMinute);
            }
        }

       
        private void ConfigureAxis(double min, double max)
        {
            LinearAxis axis = this.radChartView1.Axes[1] as LinearAxis;
            if (axis == null)
                return;

            axis.Minimum = min;
            axis.Maximum = max;
            //axis.MajorStep = majorStep;
        }

  
        protected  void WireEvents()
        {
            this.radDropDownList1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList1_SelectedIndexChanged);

        }

        private void radChartView1_DoubleClick(object sender, EventArgs e)
        {
            this.radChartView1.Zoom(1, 1);

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
        private void UpdateStockSeries(KlineInterval interval)
        {
            
            if (underProcess == true) return;
            underProcess = true;
            radChartView1.Series.Clear();

            radChartView1.Series.Add(candleSeries);

            try
            {
                if (binanceModel == null) return;
                if (binanceModel.Candels_1min == null || binanceModel.Candels_1min.Count == 0) 
                    return;
                decimal min = binanceModel.Candels_1min.Min(x => x.ClosePrice);
                decimal max= binanceModel.Candels_1min.Max(x => x.ClosePrice);

                int sampleCount =400;
                var candles = binanceModel.Candels_1min.
                    Skip(binanceModel.Candels_1min.Count() - sampleCount).Take(sampleCount).ToList();
                candleSeries.DataSource =  candles;

                //if (interval != KlineInterval.FifteenMinutes && binanceModel.MarginDatas != null && binanceModel.MarginDatas.Count() > 0 &&
                //    binanceModel.MarginDatas[0].Count() > 0)
                //    return;
                DateTime currentTime = binanceModel.Candels_1min.Last().CloseTime;
                DateTime candleStartDate = candles.First().CloseTime;

                List<KeyValuePair<KlineInterval, double>> lastEma20 = new List<KeyValuePair<KlineInterval, double>>();
                if (binanceModel.Candels_5min != null && binanceModel.Candels_5min.Any() == true)
                {
                    var candles5min = binanceModel.Candels_5min.
                        Skip(binanceModel.Candels_5min.Count() - sampleCount).Take(sampleCount).ToList();
                    var ema = CalcEma(candles5min, candleStartDate);
                   
                    if (ema != null)
                    {
                        min = Math.Min(min, ema.Min(x => x.Ema.Value));
                        max = Math.Max(max, ema.Max(x => x.Ema.Value));
                        if (WeighDic[KlineInterval.FiveMinutes] > 0)
                            lastEma20.Add(new KeyValuePair<KlineInterval,
                                double>(KlineInterval.FiveMinutes, (double)ema.Last().Ema.Value));
                        indicatorEMA_5m.DataSource = ema;
                        if (chk_ema5m.Checked)
                        {
                            radChartView1.Series.Add(indicatorEMA_5m);
                        }
                    }
                }
                if ( binanceModel.Candels_15min != null && binanceModel.Candels_15min.Any() == true)
                {
                    var candles15min = binanceModel.Candels_15min.
                        Skip(binanceModel.Candels_15min.Count() - sampleCount).Take(sampleCount).ToList();
                    var ema = CalcEma(candles15min, candleStartDate);
                    if (ema != null)
                    {
                        min = Math.Min(min, ema.Min(x => x.Ema.Value));
                        max = Math.Max(max, ema.Max(x => x.Ema.Value));
                        if (WeighDic[KlineInterval.FifteenMinutes] > 0)
                            lastEma20.Add(new KeyValuePair<KlineInterval,
                                double>(KlineInterval.FifteenMinutes, (double)ema.Last().Ema.Value));
                        indicatorEMA_15m.DataSource = ema;
                        if (chk_ema15m.Checked)
                        {
                            radChartView1.Series.Add(indicatorEMA_15m);
                        }
                    }
                }
                if ( binanceModel.Candels_30min != null && binanceModel.Candels_30min.Any() == true)
                {
                    var candles30min = binanceModel.Candels_30min.
                        Skip(binanceModel.Candels_30min.Count() - sampleCount).Take(sampleCount).ToList();
                    var ema = CalcEma(candles30min, candleStartDate);
                    if (ema != null)
                    {
                        min = Math.Min(min, ema.Min(x => x.Ema.Value));
                        max = Math.Max(max, ema.Max(x => x.Ema.Value));
                        if (WeighDic[KlineInterval.ThirtyMinutes] > 0 )
                            lastEma20.Add(new KeyValuePair<KlineInterval, 
                                double>(KlineInterval.ThirtyMinutes, (double)ema.Last().Ema.Value));
                        indicatorEMA_30m.DataSource = ema;
                        if (chk_ema30m.Checked)
                        {
                            radChartView1.Series.Add(indicatorEMA_30m);
                        }
                    }
                }
                if ( binanceModel.Candels_60min != null && binanceModel.Candels_60min.Any() == true)
                {
                    var candles60min = binanceModel.Candels_60min.
                       Skip(binanceModel.Candels_60min.Count() - sampleCount).Take(sampleCount).ToList();
                    var ema = CalcEma(candles60min, candleStartDate);
                    if (ema != null)
                    {
                        min = Math.Min(min, ema.Min(x => x.Ema.Value));
                        max = Math.Max(max, ema.Max(x => x.Ema.Value));
                        if (WeighDic[KlineInterval.OneHour] > 0 )
                            lastEma20.Add(new KeyValuePair<KlineInterval,
                                double>(KlineInterval.OneHour, (double)ema.Last().Ema.Value));
                        indicatorEMA_1h.DataSource = ema;
                        if (chk_ema1h.Checked)
                        {
                            radChartView1.Series.Add(indicatorEMA_1h);
                        }
                    }
                }
                if ( binanceModel.Candels_2hour != null && binanceModel.Candels_2hour.Any() == true)
                {
                    var candles2Hour = binanceModel.Candels_2hour.
                      Skip(binanceModel.Candels_2hour.Count() - sampleCount).Take(sampleCount).ToList();
                    var ema = CalcEma(candles2Hour, candleStartDate);
                    if (ema != null)
                    {
                        min = Math.Min(min, ema.Min(x => x.Ema.Value));
                        max = Math.Max(max, ema.Max(x => x.Ema.Value));
                        if (WeighDic[KlineInterval.TwoHour] > 0)
                            lastEma20.Add(new KeyValuePair<KlineInterval,
                                double>(KlineInterval.TwoHour, (double)ema.Last().Ema.Value));
                        indicatorEMA_2h.DataSource = ema;
                        if (chk_ema2h.Checked)
                        {
                            radChartView1.Series.Add(indicatorEMA_2h);
                        }
                    }
                }
                if ( binanceModel.Candels_4hour != null && binanceModel.Candels_4hour.Any() == true)
                {
                    var candles4Hour = binanceModel.Candels_4hour.
                      Skip(binanceModel.Candels_4hour.Count() - sampleCount).Take(sampleCount).ToList();
                    var ema = CalcEma(candles4Hour, candleStartDate);
                    if (ema != null)
                    {
                        min = Math.Min(min, ema.Min(x => x.Ema.Value));
                        max = Math.Max(max, ema.Max(x => x.Ema.Value));
                        if (WeighDic[KlineInterval.FourHour] > 0 )
                            lastEma20.Add(new KeyValuePair<KlineInterval, 
                                double>(KlineInterval.FourHour, (double)ema.Last().Ema.Value));
                        indicatorEMA_4h.DataSource = ema;
                        if (chk_ema4h.Checked)
                        {
                            radChartView1.Series.Add(indicatorEMA_4h);
                        }
                    }
                }
                                
                if ( binanceModel.Candels_oneDay != null && binanceModel.Candels_oneDay.Any() == true)
                {
                    var candles1Day = binanceModel.Candels_oneDay;
                    if(candles1Day.Count()>sampleCount)
                        candles1Day = binanceModel.Candels_oneDay.Skip(binanceModel.Candels_oneDay.Count() - sampleCount).Take(sampleCount).ToList();
                    var ema = CalcEma(candles1Day, candleStartDate);
                    if (ema != null)
                    {
                        min = Math.Min(min, ema.Min(x => x.Ema.Value));
                        max = Math.Max(max, ema.Max(x => x.Ema.Value));
                        if (WeighDic[KlineInterval.OneDay] > 0 )
                            lastEma20.Add(new KeyValuePair<KlineInterval, double>(KlineInterval.OneDay, 
                                (double)ema.Last().Ema.Value));
                        indicatorEMA_1D.DataSource = ema;
                        if (chk_ema1D.Checked)
                        {
                            radChartView1.Series.Add(indicatorEMA_1D);
                        }
                    }
                }
                if ( binanceModel.Candels_oneWeek != null && binanceModel.Candels_oneWeek.Any() == true)
                {
                    var candles1Week = binanceModel.Candels_oneWeek;
                    if (candles1Week.Count() > sampleCount)
                        candles1Week = binanceModel.Candels_oneWeek.Skip(binanceModel.Candels_oneWeek.Count() - sampleCount).Take(sampleCount).ToList();

                    var ema = CalcEma(candles1Week, candleStartDate);
                    if (ema != null)
                    {
                        min = Math.Min(min, ema.Min(x => x.Ema.Value));
                        max = Math.Max(max, ema.Max(x => x.Ema.Value));
                        if (WeighDic[KlineInterval.OneWeek] > 0 )
                            lastEma20.Add(new KeyValuePair<KlineInterval, double>(KlineInterval.OneWeek, 
                                (double)ema.Last().Ema.Value));
                        indicatorEMA_1W.DataSource = ema;
                        if (chk_ema1W.Checked)
                        {
                            radChartView1.Series.Add(indicatorEMA_1W);
                        }
                    }
                }
                var values = lastEma20.Select(x=>(double)x.Value).ToList();
                List<double> breaks = new List<double>();
                if (radio_jenksFisher.Checked)
                {
                    breaks = JenksFisher.CreateJenksFisherBreaksArray(values, numOfClasses);
                    breaks = breaks.Skip(1).ToList();
                }
                else
                    breaks = PercentClassifier.Classify(values, percent);
                breaks.Add(lastEma20.Max(x=>x.Value) + 1);
                int index = 0;
                List<EmaResult> testpoint = new List<EmaResult>();
                foreach(var item in breaks)
                {
                    var collection = lastEma20.Where(x => x.Value < item);
                    var result = CalculateWeightedMean(collection);
                    if (result != null)
                    {
                        if(binanceModel.MarginDatas[index].Count==0)
                            binanceModel.MarginDatas[index].Add(new EmaResult { Date = currentTime.AddMinutes(-5), Ema = (decimal)result });
                        binanceModel.MarginDatas[index].Add(new EmaResult { Date = currentTime, Ema = (decimal)result });

                    }
                    else
                    {
                        if (binanceModel.MarginDatas[index].Any())
                        {
                            var lastValue = binanceModel.MarginDatas[index].Last().Ema;
                            binanceModel.MarginDatas[index].Add(new EmaResult { Date = currentTime, Ema = lastValue });
                        }
                    }
                    lastEma20 = lastEma20.Except(collection).ToList();
                    Margins[index].DataSource = binanceModel.MarginDatas[index];
                    testpoint.AddRange(binanceModel.MarginDatas[index]);
                    index++;
                }
                for(int i=0;i<7; i++)
                {
                    if (binanceModel.MarginDatas[i] != null && binanceModel.MarginDatas[i].Any())
                    {
                        radChartView1.Series.Add(Margins[i]);
                        if (binanceModel.MarginDatas[i].Count() > 200)
                            binanceModel.MarginDatas[i].RemoveAt(0);
                    }

                }

                ConfigureAxis((double)min, (double)max);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                underProcess = false;
            }
           
        }

        private double? CalculateWeightedMean(IEnumerable<KeyValuePair<KlineInterval, double>> collection)
        {
            double sum = 0;
            double weightSum = 0;
            var l = collection.ToList();
            foreach(var item in collection)
            {
                sum += item.Value * WeighDic[item.Key];
                weightSum += WeighDic[item.Key];
            }
            if (weightSum > 0)
                return (sum / weightSum);
            return null;
        }

        private List<EmaResult> CalcEma(List<IBinanceKline> candles, DateTime candleStartDate)
        {
            try
            {
                var Qutoes = BinanceHelper.GetQuotes(candles);
                
                if (Qutoes.Count < 120)
                {
                    var q = Qutoes[0];
                    int cnt = 120 - Qutoes.Count;
                    for (int i = 0; i < cnt; i++)
                        Qutoes.Insert(0, q);
                }
                var ema = BinanceHelper.EMA(Qutoes, periodEMA);
                
                ema = ema.Where(x => x.Date >= candleStartDate).ToList();
                ema.Insert(0,new EmaResult { Date = candleStartDate, Ema = ema[0].Ema });
                
                
                return ema;
            }
            catch(Exception)
            {
                return null;
            }
        }



        private void strategiesEma_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.CandleReadyEvent -= OnDataReadyEvent;
            BinanceDataCollector.Instance.DataReadyEvent -= UpdatePrice;
        }

       
        private void Toggle_Checkbox(object sender, EventArgs e)
        {
            CheckBox chk= (CheckBox)sender;
            //if(chk.Checked)
            //    UpdateStockSeries(KlineInterval.OneMinute);
            //else
            //{
                string last = chk.Name.Substring(chk.Name.Length - 3, 3);
                switch (last)
                {
                case "a5m": //15min
                    if (chk.Checked) radChartView1.Series.Add(indicatorEMA_5m);
                    else radChartView1.Series.Remove(indicatorEMA_5m);
                    break;
                case "15m": //15min
                        if (chk.Checked) radChartView1.Series.Add(indicatorEMA_15m);
                        else radChartView1.Series.Remove(indicatorEMA_15m);
                        break;
                    case "30m": //30min
                        if(chk.Checked) radChartView1.Series.Add(indicatorEMA_30m);
                        else
                            radChartView1.Series.Remove(indicatorEMA_30m);
                        break;
                    case "a1h":
                        if (chk.Checked) radChartView1.Series.Add(indicatorEMA_1h);
                        else radChartView1.Series.Remove(indicatorEMA_1h);
                        break;
                    case "a2h":
                        if (chk.Checked) radChartView1.Series.Add(indicatorEMA_2h);
                        else    radChartView1.Series.Remove(indicatorEMA_2h);
                        break;
                    case "a4h":
                        if (chk.Checked) radChartView1.Series.Add(indicatorEMA_4h);
                        else radChartView1.Series.Remove(indicatorEMA_4h);
                        break;
                    case "a1D":
                        if (chk.Checked) radChartView1.Series.Add(indicatorEMA_1D);
                        else radChartView1.Series.Remove(indicatorEMA_1D);
                        break;
                    case "a1W":
                        if (chk.Checked) radChartView1.Series.Add(indicatorEMA_1W);
                        else radChartView1.Series.Remove(indicatorEMA_1W);
                        break;
                }

            //}
        }
        
        private void tb_breaks_TextChanged(object sender, EventArgs e)
        {
            if (binanceModel == null) return;
            if (double.TryParse(tb_percent.Text, out percent) == false)
                percent = 10;
            if (percent > 100)
            {
                percent = 100;
                tb_percent.Text = "100";
            }
            if(percent<=0)
            {
                percent = 1;
                tb_percent.Text = "1";
            }
        }

        Dictionary<KlineInterval, double> WeighDic = new Dictionary<KlineInterval, double>
        {{KlineInterval.FiveMinutes, 1 }, {KlineInterval.FifteenMinutes, 1 }, { KlineInterval.ThirtyMinutes, 1.5},
            { KlineInterval.OneHour, 2}, { KlineInterval.TwoHour, 3}, { KlineInterval.FourHour, 5},
            { KlineInterval.OneDay, 7}, { KlineInterval.OneWeek,9} };
        
        private void WeightChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if(double.TryParse(tb.Text, out double weight)==false)
                return;
            KlineInterval key = KlineInterval.OneMinute;
            string last = tb.Name.Substring(tb.Name.Length - 3, 3);
            switch (last)
            {
                case "a5m":
                    key = KlineInterval.FiveMinutes;
                    break;
                case "15m": 
                    key = KlineInterval.FifteenMinutes;
                    break;
                case "30m": 
                    key = KlineInterval.ThirtyMinutes;
                    break;
                case "a1h":
                    key = KlineInterval.OneHour;
                    break;
                case "a2h":
                    key = KlineInterval.TwoHour;
                    break;
                case "a4h":
                    key = KlineInterval.FourHour;
                    break;
                case "a1D":
                    key = KlineInterval.OneDay;
                    break;
                case "a1W":
                    key = KlineInterval.OneWeek;
                    break;
            }
            if (key != KlineInterval.OneMinute && WeighDic[key] != weight)
            {
                WeighDic[key] = weight;
                UpdateStockSeries(KlineInterval.OneMinute);
            }
        }

       
        
        private void tb_NumofClasses_TextChanged(object sender, EventArgs e)
        {
            if (binanceModel == null) return;
            if (int.TryParse(tb_NumofClasses.Text, out numOfClasses) == false)
                numOfClasses = 4;
            if (numOfClasses > 7)
            {
                numOfClasses = 7;
                tb_NumofClasses.Text = "7";
            }
            if (percent <= 0)
            {
                numOfClasses = 1;
                tb_NumofClasses.Text = "1";
            }
        }
    }
}
