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
    public partial class Form1 : Telerik.WinControls.UI.RadForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();

        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis2 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();
       // Telerik.WinControls.UI.LinearAxis linearAxis3 = new Telerik.WinControls.UI.LinearAxis();

        CandlestickSeries candleSery = new CandlestickSeries();
        CandlestickSeries tbqvSery = new CandlestickSeries();
        LineSeries tbqvLine = new LineSeries();


        private List<string> movingAverageIndicators, financialIndicators;
        
        private string coinName = string.Empty;

        private KlineInterval interval = KlineInterval.OneMinute;

        private BinanceModel binance;

        private List<IBinanceKline> candels;
        private List<Quote> tbqv;

        private void Form1_Load(object sender, EventArgs e)
        {
            //BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            //{
            //    ApiCredentials = new ApiCredentials("78KoVDEzrJM58vGxuz1nG6MZV3HnXRAHlQsfH9POskT5FrZa7204Oaa92jfK34ZL", "4jtBPUQyMloPBVLFGDljW90SboVIp4LziUEki6ycaV6dXQFomsSalpjAn36KHwGT"),
            //    /*LogVerbosity = LogVerbosity.Debug,
            //    LogWriters = new List<TextWriter> { Console.Out }*/
            //});
            //BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            //{
            //    ApiCredentials = new ApiCredentials("78KoVDEzrJM58vGxuz1nG6MZV3HnXRAHlQsfH9POskT5FrZa7204Oaa92jfK34ZL", "4jtBPUQyMloPBVLFGDljW90SboVIp4LziUEki6ycaV6dXQFomsSalpjAn36KHwGT"),
            //    /*LogVerbosity = LogVerbosity.Debug,
            //    LogWriters = new List<TextWriter> { Console.Out }*/
            //});
            initAxis();
            InitSeries();
            InitializeData();
            WireEvents();
            this.radDropDownList1.DataSource = movingAverageIndicators;
            this.radDropDownList2.DataSource = financialIndicators;
            this.radDropDownList3.DataSource = BinanceDataCollector.Instance.CoinNames;
            List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(), KlineInterval.FiveMinutes.ToString(), KlineInterval.FifteenMinutes.ToString() };
            this.radDropDownList4.DataSource = Intervals;
            

            radChartView1.BackColor = Color.LightBlue;
            radChartView2.BackColor = Color.Gray;

            SetCartesianGrid(this.radChartView1);
            SetCartesianGrid(this.radChartView2);
            SetTrackBall();
            //RefreshTimer.Interval = 5000;
            //RefreshTimer.Tick += RefreshTimer_Tick;
            //RefreshTimer.Start();
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
                dgv_Buyer.DataSource = binanceModel.BuyerList;
                dgv_Seller.DataSource = binanceModel.SellerList;
                dgv_Trade.DataSource = binanceModel.TradeList;
                UpdateStockSeries(); //pegah commented
               // UpdateFinancialIndicator(this.radDropDownList2.SelectedItem.Text);
            }
            catch { }
        }
       
        private void InitializeData()
        {
           // viewModel = new CandlestickViewModel();


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
            this.radChartView1.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1,
            linearAxis1});

            LassoZoomController lassoZoomController = new LassoZoomController();
            radChartView1.Controllers.Add(lassoZoomController);
            radChartView1.Area.View.Palette = KnownPalette.Material;

            dateTimeCategoricalAxis2.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis2.IsPrimary = true;
            dateTimeCategoricalAxis2.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis2.LabelFormat = "{0:HH:mm:ss}";
            dateTimeCategoricalAxis2.MajorTickInterval = 5;

            linearAxis2.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Right;
            linearAxis2.IsPrimary = true;
            linearAxis2.MajorStep = 5D;
            linearAxis2.Maximum = 80D;
            linearAxis2.Minimum = 50D;
            linearAxis2.TickOrigin = null;

            //linearAxis3.AxisType = Telerik.Charting.AxisType.Second;
            //linearAxis3.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            //linearAxis3.IsPrimary = false;
           // linearAxis3.MajorStep = 5D;
            //linearAxis3.Maximum = 80D;
            //linearAxis3.Minimum = 50D;
            //linearAxis3.TickOrigin = null;

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

        void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            UpdateStockSeries();
        }

        void radDropDownList2_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            this.radChartView1.View.Margin = new System.Windows.Forms.Padding(15, 10, 10, 10);
           // UpdateFinancialIndicator(this.radDropDownList2.SelectedItem.Text);
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


        private void radDropDownList4_SelectedIndexChanged(object sender, PositionChangedEventArgs e)
        {
            if (this.radDropDownList4.SelectedItem != null)
            {
                Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);
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

            tbqvSery.HorizontalAxis = dateTimeCategoricalAxis1;
            tbqvSery.VerticalAxis = linearAxis1;
            tbqvSery.BackColor = Color.Blue;
            tbqvSery.BorderColor = Color.Blue;
            tbqvSery.OpenValueMember = "Open";
            tbqvSery.HighValueMember = "High";
            tbqvSery.LowValueMember = "Low";
            tbqvSery.CloseValueMember = "Close";
            tbqvSery.CategoryMember = "Date";

            tbqvLine.HorizontalAxis = dateTimeCategoricalAxis1;
            tbqvLine.VerticalAxis = linearAxis1;
            tbqvLine.BackColor = Color.Blue;
            tbqvLine.BorderColor = Color.Blue;

        }
        private void UpdateStockSeries()
        {
            try
            {
                if (binance == null ) return;
                //DateTimeCategoricalAxis horizontalAxis = this.radChartView2.Axes[0] as DateTimeCategoricalAxis;
                //LinearAxis verticalAxis = this.radChartView2.Axes[1] as LinearAxis;
                //if (this.radChartView2.Series == null) return;
                this.radChartView2.Series.Clear();
                this.radChartView1.Series.Clear();
                tbqvLine.DataPoints.Clear();

                if (binance.Tbqv_15min.Any() == false)
                    binance = BinanceDataCollector.Instance.GetBinance(coinName);
                switch (interval)
                {
                    case KlineInterval.OneMinute:
                        candels = binance.Candels_1min.Skip(binance.Candels_1min.Count()-100).Take(100).ToList();
                        tbqv = binance.Tbqv_1min;
                        break;
                    case KlineInterval.FiveMinutes:
                        candels = binance.Candels_5min.Skip(binance.Candels_5min.Count() - 100).Take(100).ToList();
                        tbqv = binance.Tbqv_5min;
                        break;
                    case KlineInterval.FifteenMinutes:
                        candels = binance.Candels_15min.Skip(binance.Candels_15min.Count() - 100).Take(100).ToList();
                        tbqv = binance.Tbqv_15min;
                        break;
                }
                if (candels == null || candels.Any() == false) return;
                candleSery.DataSource = candels;

                var temp = tbqv.Select(x=>new CategoricalDataPoint((double)x.Close, x.Date)).ToList();
                tbqvLine.DataPoints.AddRange(temp);

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

                if (this.radDropDownList1.SelectedItem != null)
                {
                    IndicatorBase indicator = CreateMAIndicator(this.radDropDownList1.SelectedItem.Text);
                    indicator.BorderColor = Color.Red;
                    indicator.PointSize = SizeF.Empty;

                    IParentIndicator parentIndicator = indicator as IParentIndicator;
                    if (parentIndicator != null)
                    {
                        parentIndicator.ChildIndicator.BorderColor = Color.Black;
                    }

                    this.radChartView2.Series.Add(indicator);
                }

                tbqvSery.DataSource = tbqv;
                this.radChartView1.Series.Add(tbqvSery);
                this.radChartView1.Series.Add(tbqvLine);
            }
            catch (Exception ex)
            { }
        }
        //private void UpdateStockSeries()
        //{
        //    return;
        //    if (viewModel.Data == null || !viewModel.Data.Any()) return;
        //    DateTimeCategoricalAxis horizontalAxis = this.radChartView2.Axes[0] as DateTimeCategoricalAxis;
        //    LinearAxis verticalAxis = this.radChartView2.Axes[1] as LinearAxis;
        //    this.radChartView2.Series.Clear();

        //    string seriesType = "Candlestick";

        //        if (seriesType == "Candlestick" || seriesType == "OHLC")
        //        {
        //        CandlestickSeries series = new CandlestickSeries();
        //         //OhlcSeries series = (seriesType == "Candlestick") ? new CandlestickSeries() : new OhlcSeries();

        //            series.BorderColor = Color.FromArgb(102, 102, 102);
        //            series.BackColor = Color.FromArgb(102, 102, 102);
        //            series.HorizontalAxis = horizontalAxis;
        //            series.VerticalAxis = verticalAxis;
        //            series.OpenValueMember = "Open";
        //            series.HighValueMember = "High";
        //            series.LowValueMember = "Low";
        //            series.CloseValueMember = "Close";
        //            series.CategoryMember = "Date";
        //            series.DataSource = viewModel.Data;

        //            linearAxis2.MajorStep = 10;
        //            linearAxis2.Maximum =(double) Math.Ceiling( viewModel.Data.Max(v=>v.High));
        //            linearAxis2.Minimum =(double) Math.Floor( viewModel.Data.Min(v => v.Low));


        //        this.radChartView2.Series.Add(series);

        //            if (!(series is CandlestickSeries))
        //            {
        //                foreach (OhlcPointElement point in series.Children)
        //                {
        //                    if (point.IsFalling)
        //                    {
        //                        point.BorderColor = Color.FromArgb(175, 175, 175);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            HlcSeries series = new HlcSeries();

        //            series.BorderColor = Color.FromArgb(102, 102, 102);
        //            series.BackColor = Color.FromArgb(102, 102, 102);
        //            series.HorizontalAxis = horizontalAxis;
        //            series.VerticalAxis = verticalAxis;
        //            series.HighValueMember = "High";
        //            series.LowValueMember = "Low";
        //            series.CloseValueMember = "Close";
        //            series.CategoryMember = "Date";
        //            series.DataSource = viewModel.Data;

        //            this.radChartView2.Series.Add(series);
        //        }
        //  //  }

        //    if (this.radDropDownList1.SelectedItem != null)
        //    {
        //        IndicatorBase indicator = CreateMAIndicator(this.radDropDownList1.SelectedItem.Text);
        //        indicator.BorderColor = Color.Red;
        //        indicator.PointSize = SizeF.Empty;

        //        IParentIndicator parentIndicator = indicator as IParentIndicator;
        //        if (parentIndicator != null)
        //        {
        //            parentIndicator.ChildIndicator.BorderColor = Color.Black;
        //        }

        //        this.radChartView2.Series.Add(indicator);
        //    }
        //}

        private void UpdateFinancialIndicator(string indicatorType)
        {
            try
            {
                //if (viewModel.Data == null || !viewModel.Data.Any()) return;
                if (candels == null || !candels.Any()) return;
                DateTimeCategoricalAxis horizontalAxis = this.radChartView1.Axes[0] as DateTimeCategoricalAxis;
                LinearAxis verticalAxis = this.radChartView1.Axes[1] as LinearAxis;

                this.radChartView1.Series.Clear();

                IndicatorBase indicator = CreateFinancialIndicator("MACD"/*indicatorType*/);
                indicator.HorizontalAxis = horizontalAxis;
                indicator.VerticalAxis = verticalAxis;
                indicator.PointSize = SizeF.Empty;
                indicator.BorderColor = Color.Black;

                IParentIndicator parentIndicator = indicator as IParentIndicator;
                if (parentIndicator != null)
                {
                    parentIndicator.ChildIndicator.BorderColor = Color.Red;
                }

                //var max = (double)indicator.DataPoints.Max(d => ((IndicatorHlcValueDataPoint)(d)).Value);
                //var min = (double)indicator.DataPoints.Min(d => ((IndicatorHlcValueDataPoint)(d)).Value);
                //if(min==max)
                //{
                //    min = min - 1;
                //    max = max + 1;
                //}
                //var step = (max - min) / 10;
                //ConfigureAxis(min, max, step);
                this.radChartView1.Series.Add(indicator);

            }
            catch (Exception ex)
            { }


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

        private IndicatorBase CreateMAIndicator(string indicatorType)
        {
            IndicatorBase indicator;

            if (indicatorType.StartsWith("MA ("))
            {
                indicator = new MovingAverageIndicator();
                indicator.Name = "MA";
            }
            else if (indicatorType.StartsWith("Exponential MA"))
            {
                indicator = new ExponentialMovingAverageIndicator();
                indicator.Name = "EMA";
            }
            else if (indicatorType.StartsWith("Modified MA"))
            {
                indicator = new ModifiedMovingAverageIndicator();
                indicator.Name = "MMA";
            }
            else if (indicatorType.StartsWith("Modified Exponential MA"))
            {
                indicator = new ModifiedExponentialMovingAverageIndicator();
                indicator.Name = "MEMA";
            }
            else if (indicatorType.StartsWith("Weighted MA"))
            {
                indicator = new WeightedMovingAverageIndicator();
                indicator.Name = "WMA";
            }
            else if (indicatorType.StartsWith("Adaptive MA"))
            {
                indicator = new AdaptiveMovingAverageKaufmanIndicator();
                indicator.Name = "KAMA";
                ((AdaptiveMovingAverageKaufmanIndicator)indicator).SlowPeriod = 10;
                ((AdaptiveMovingAverageKaufmanIndicator)indicator).FastPeriod = 4;
            }
            else
            {
                indicator = new BollingerBandsIndicator();
                indicator.Name = "BOLL";
                ((BollingerBandsIndicator)indicator).StandardDeviations = 2;
            }
            
            ((ValuePeriodIndicatorBase)indicator).Period = 5;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            return indicator;
        }

        private IndicatorBase CreateFinancialIndicator(string indicatorType)
        {
            if (indicatorType.StartsWith("Average True Range"))
            {
                return CreateAverageTrueRangeIndicator();
            }
            else if (indicatorType.StartsWith("Commodity Channel Index"))
            {
                return CreateCommodityChannelIndexIndicator();
            }
            else if (indicatorType.StartsWith("MACD"))
            {
                return CreateMACDIndicator();
            }
            else if (indicatorType.StartsWith("Momentum"))
            {
                return CreateMomentumIndicator();
            }
            else if (indicatorType.StartsWith("Oscillator"))
            {
                return CreateOscillatorIndicator();
            }
            else if (indicatorType.StartsWith("RAVI"))
            {
                return CreateRaviIndicator();
            }
            else if (indicatorType.StartsWith("Rate Of Change"))
            {
                return CreateRateOfChangeIndicator();
            }
            else if (indicatorType.StartsWith("Relative Momentum Index"))
            {
                return CreateRelativeMomentumIndexIndicator();
            }
            else if (indicatorType.StartsWith("Relative Strength Index"))
            {
                return CreateRelativeStrengthIndexIndicator();
            }
            else if (indicatorType.StartsWith("Stochastic Fast"))
            {
                return CreateStochasticFastIndicator();
            }
            else if (indicatorType.StartsWith("Stochastic Slow"))
            {
                return CreateStochasticSlowIndicator();
            }
            else if (indicatorType.StartsWith("True Range"))
            {
                return CreateTrueRangeIndicator();
            }
            else if (indicatorType.StartsWith("TRIX"))
            {
                return CreateTrixIndicator();
            }
            return CreateUltimateOscillatorIndicator();
        }

        private AverageTrueRangeIndicator CreateAverageTrueRangeIndicator()
        {
            AverageTrueRangeIndicator indicator = new AverageTrueRangeIndicator();
            indicator.Period = 5;
            indicator.CategoryMember = "CloseTime";
            indicator.HighValueMember = "High";
            indicator.LowValueMember = "Low";
            indicator.CloseValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(0, 5, 1);

            return indicator;
        }

        private CommodityChannelIndexIndicator CreateCommodityChannelIndexIndicator()
        {
            CommodityChannelIndexIndicator indicator = new CommodityChannelIndexIndicator();
            indicator.Period = 5;
            indicator.CategoryMember = "CloseTime";
            indicator.HighValueMember = "High";
            indicator.LowValueMember = "Low";
            indicator.CloseValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(-200, 200, 100);

            return indicator;
        }


        private IndicatorBase CreateMACDIndicator()
        {
            MacdIndicator indicator = new MacdIndicator();
            indicator.ShortPeriod = 9;
            indicator.LongPeriod = 12;
            indicator.SignalPeriod = 6;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(-2, 2, 1);

            return indicator;
        }

        private MomentumIndicator CreateMomentumIndicator()
        {
            MomentumIndicator indicator = new MomentumIndicator();
            indicator.Period = 8;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(80, 120, 10);

            return indicator;
        }

        private OscillatorIndicator CreateOscillatorIndicator()
        {
            OscillatorIndicator indicator = new OscillatorIndicator();
            indicator.ShortPeriod = 4;
            indicator.LongPeriod = 8;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(-5, 5, 5);

            return indicator;
        }

        private RaviIndicator CreateRaviIndicator()
        {
            RaviIndicator indicator = new RaviIndicator();
            indicator.ShortPeriod = 4;
            indicator.LongPeriod = 8;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(-5, 5, 5);

            return indicator;
        }

        private RateOfChangeIndicator CreateRateOfChangeIndicator()
        {
            RateOfChangeIndicator indicator = new RateOfChangeIndicator();
            indicator.Period = 8;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(-20, 20, 10);

            return indicator;
        }

        private RelativeMomentumIndexIndicator CreateRelativeMomentumIndexIndicator()
        {
            RelativeMomentumIndexIndicator indicator = new RelativeMomentumIndexIndicator();
            indicator.Period = 8;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(0, 100, 20);

            return indicator;
        }

        private RelativeStrengthIndexIndicator CreateRelativeStrengthIndexIndicator()
        {
            RelativeStrengthIndexIndicator indicator = new RelativeStrengthIndexIndicator();
            indicator.Period = 8;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "Close";
            indicator.DataSource = candels;// viewModel.Data;

            ConfigureAxis(0, 100, 20);

            return indicator;
        }

        private StochasticFastIndicator CreateStochasticFastIndicator()
        {
            StochasticFastIndicator indicator = new StochasticFastIndicator();
            indicator.MainPeriod = 14;
            indicator.SignalPeriod = 3;
            indicator.CategoryMember = "CloseTime";
            indicator.HighValueMember = "High";
            indicator.LowValueMember = "Low";
            indicator.CloseValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(0, 100, 20);

            return indicator;
        }

        private StochasticSlowIndicator CreateStochasticSlowIndicator()
        {
            StochasticSlowIndicator indicator = new StochasticSlowIndicator();
            indicator.MainPeriod = 14;
            indicator.SignalPeriod = 3;
            indicator.SlowingPeriod = 3;
            indicator.CategoryMember = "CloseTime";
            indicator.HighValueMember = "High";
            indicator.LowValueMember = "Low";
            indicator.CloseValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(0, 100, 20);

            return indicator;
        }

        private TrixIndicator CreateTrixIndicator()
        {
            TrixIndicator indicator = new TrixIndicator();
            indicator.Period = 8;
            indicator.CategoryMember = "CloseTime";
            indicator.ValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(-1, 1, 0.5);

            return indicator;
        }

        private TrueRangeIndicator CreateTrueRangeIndicator()
        {
            TrueRangeIndicator indicator = new TrueRangeIndicator();
            indicator.CategoryMember = "CloseTime";
            indicator.HighValueMember = "High";
            indicator.LowValueMember = "Low";
            indicator.CloseValueMember = "Close";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(0, 6, 1);

            return indicator;
        }

        private UltimateOscillatorIndicator CreateUltimateOscillatorIndicator()
        {
            UltimateOscillatorIndicator indicator = new UltimateOscillatorIndicator();
            indicator.Period = 6;
            indicator.Period2 = 9;
            indicator.Period3 = 12;
            indicator.CategoryMember = "CloseTime";
            indicator.CloseValueMember = "Close";
            indicator.HighValueMember = "High";
            indicator.LowValueMember = "Low";
            indicator.DataSource = candels;//viewModel.Data;

            ConfigureAxis(0, 100, 20);

            return indicator;
        }

        protected  void WireEvents()
        {
            this.radDropDownList3.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList3_SelectedIndexChanged);

            this.radDropDownList2.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList2_SelectedIndexChanged);
            this.radDropDownList1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList1_SelectedIndexChanged);
            this.radDropDownList4.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList4_SelectedIndexChanged);

        }

        

        private void radChartView2_DoubleClick(object sender, EventArgs e)
        {
            this.radChartView2.Zoom(1, 1);

        }

        private void radChartView1_DoubleClick(object sender, EventArgs e)
        {
            this.radChartView1.Zoom(1, 1);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
