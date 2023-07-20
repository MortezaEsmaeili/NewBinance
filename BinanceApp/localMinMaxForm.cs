using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;

using BinanceApp.Business;
using BinanceApp.DataModel;
using BinanceApp.Helper;
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
    public partial class localMinMaxForm : Telerik.WinControls.UI.RadForm
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


        public List<ArrayPoint> stMins;
        public List<ArrayPoint> stMaxes;
        public List<ArrayPoint> ltMins;
        public List<ArrayPoint> ltMaxes;
        public List<Helper.ArrayPoint> mins;
        public List<Helper.ArrayPoint> maxes;

        public localMinMaxForm()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

        LineSeries price = new LineSeries();
        LineSeries shortTermSf = new LineSeries();
        LineSeries longTermSf = new LineSeries();
        
        LineSeries priceMin = new LineSeries();
        LineSeries priceMax = new LineSeries();

        LineSeries shortTermSfMin = new LineSeries();
        LineSeries shortTermSfMax = new LineSeries();

        LineSeries longTermSfMin = new LineSeries();
        LineSeries longTermSfMax = new LineSeries();

        LineSeries LineOne = new LineSeries();
        LineSeries SupportLine = new LineSeries(); 
        LineSeries ResistanceLine = new LineSeries();

        private delegate void UpdateFormDelegate();

        private void localMinMaxForm_Load(object sender, EventArgs e)
        {
            
            initAxis();
            InitSeries();
            WireEvents();

            ProgressBar1_1.Value1 = 50;
            ProgressBar1_2.Value1 = 50;
            ProgressBar1_3.Value1 = 50;

            ProgressBar5_1.Value1 = 50;
            ProgressBar5_2.Value1 = 50;
            ProgressBar5_3.Value1 = 50;

            ProgressBar15_1.Value1 = 50;
            ProgressBar15_2.Value1 = 50;
            ProgressBar15_3.Value1 = 50;

            ProgressBar30_1.Value1 = 50;
            ProgressBar30_2.Value1 = 50;
            ProgressBar30_3.Value1 = 50;

            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;
            this.radDropDownList4.DataSource = Intervals;

            radChartView1.BackColor = Color.AliceBlue;
            radChartView1.GetArea<CartesianArea>().ShowGrid = true;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall();
            coinName = this.radDropDownList1.SelectedItem.Text;
            
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;
            BinanceDataCollector.Instance.DataReadyEvent += OnDateReadyEvent;
            OnCandleReadyEvent(interval, coinName);
            //UseWaitCursor = true;
            sendLog("Initializing ...");
        }

        private void OnDateReadyEvent()
        {
            try
            {
                if (binanceModel != null && binanceModel.PriceList != null && binanceModel.PriceList.Any())
                {
                    decimal currentPrice = binanceModel.PriceList.Last().Value;
                    this.Invoke(new UpdateFormDelegate( ()=>{ price_LB.Text = currentPrice.ToString(); }));
                }
                else if(binanceModel == null&& string.IsNullOrWhiteSpace(coinName)==false)
                {
                    var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                    if (coinInfo == null) return;
                    binanceModel = coinInfo;
                    //UseWaitCursor = false;
                    OnCandleReadyEvent(interval, coinName);
                }
            }
            catch(Exception ex)
            { }
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
                        //UseWaitCursor = false;
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
            radChartView1.ShowTrackBall = true;

        }
        
        private void InitSeries()
        {
            shortTermSf.HorizontalAxis = dateTimeCategoricalAxis1;
            shortTermSf.VerticalAxis = linearAxis2;
            shortTermSf.BorderColor = Color.Red;
            shortTermSf.LegendTitle = "ShortTermSF";

            shortTermSfMin.HorizontalAxis = dateTimeCategoricalAxis1;
            shortTermSfMin.VerticalAxis = linearAxis2;
            shortTermSfMin.BorderColor = Color.Red;
            shortTermSfMin.LegendTitle = "ShortTermSFMin";
            shortTermSfMin.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            shortTermSfMax.HorizontalAxis = dateTimeCategoricalAxis1;
            shortTermSfMax.VerticalAxis = linearAxis2;
            shortTermSfMax.BorderColor = Color.Red;
            shortTermSfMax.LegendTitle = "ShortTermSFMax";
            shortTermSfMax.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            longTermSf.HorizontalAxis = dateTimeCategoricalAxis1;
            longTermSf.VerticalAxis = linearAxis2;
            longTermSf.BorderColor = Color.Green;
            longTermSf.LegendTitle = "LongTermSF";

            longTermSfMin.HorizontalAxis = dateTimeCategoricalAxis1;
            longTermSfMin.VerticalAxis = linearAxis2;
            longTermSfMin.BorderColor = Color.Green;
            longTermSfMin.LegendTitle = "LongTermSFMin";
            longTermSfMin.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            longTermSfMax.HorizontalAxis = dateTimeCategoricalAxis1;
            longTermSfMax.VerticalAxis = linearAxis2;
            longTermSfMax.BorderColor = Color.Green;
            longTermSfMax.LegendTitle = "LongTermSFMax";
            longTermSfMax.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            price.HorizontalAxis = dateTimeCategoricalAxis1;
            price.VerticalAxis = linearAxis1;
            price.BorderColor = Color.Blue;
            price.LegendTitle = "Price";

            priceMin.HorizontalAxis = dateTimeCategoricalAxis1;
            priceMin.VerticalAxis = linearAxis1;
            priceMin.BorderColor = Color.Blue;
            priceMin.LegendTitle = "PriceMin";
            priceMin.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            priceMax.HorizontalAxis = dateTimeCategoricalAxis1;
            priceMax.VerticalAxis = linearAxis1;
            priceMax.BorderColor = Color.Blue;
            priceMax.LegendTitle = "PriceMin";
            priceMax.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            LineOne.HorizontalAxis = dateTimeCategoricalAxis1;
            LineOne.VerticalAxis = linearAxis2;
            LineOne.BorderColor = Color.Black;
            LineOne.BackColor = Color.Black;
            LineOne.IsVisibleInLegend = false;
            LineOne.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            SupportLine.HorizontalAxis = dateTimeCategoricalAxis1;
            SupportLine.VerticalAxis = linearAxis1;
            SupportLine.BorderColor = Color.Orange;
            SupportLine.BackColor = Color.Orange;
            SupportLine.IsVisibleInLegend = false;
            SupportLine.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            ResistanceLine.HorizontalAxis = dateTimeCategoricalAxis1;
            ResistanceLine.VerticalAxis = linearAxis1;
            ResistanceLine.BorderColor = Color.Purple;
            ResistanceLine.BackColor = Color.Purple;
            ResistanceLine.IsVisibleInLegend = false;
            ResistanceLine.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            radChartView1.Series.Add(price);
            radChartView1.Series.Add(shortTermSf);
            radChartView1.Series.Add(longTermSf);

            //radChartView1.Series.Add(priceMin);
            //radChartView1.Series.Add(shortTermSfMin);
            //radChartView1.Series.Add(longTermSfMin);

            //radChartView1.Series.Add(priceMax);
            //radChartView1.Series.Add(shortTermSfMax);
            //radChartView1.Series.Add(longTermSfMax);

            radChartView1.Series.Add(LineOne);
            radChartView1.Series.Add(ResistanceLine);
            radChartView1.Series.Add(SupportLine);
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
            trackBallElement.TextNeeded += new TextNeededEventHandler(trackball_TextNeeded);
            this.radChartView1.Controllers.Add(trackBallElement);
        }

        private void trackball_TextNeeded(object sender, TextNeededEventArgs e)
        {
            try
            {
                if (e.Points.Count < 3) return;

                var index = e.Points[0].DataPoint.CollectionIndex;
                if (index < 0)
                {
                    return;
                }
                var point = price.DataPoints[index] as CategoricalDataPoint;
                StringBuilder textBuilder = new StringBuilder();
                textBuilder.Append("<html><color=255,0,0,0>");
                textBuilder.Append(String.Format("{0}", point.Category));
                textBuilder.Append($"\r\n<color=blue> p = {point.Value}");

                var indexLT = e.Points[1].DataPoint.CollectionIndex;
                if (indexLT >= 0)
                {
                    point = longTermSf.DataPoints[indexLT] as CategoricalDataPoint;
                    textBuilder.Append($"\r\n<color=green> LT = {point.Value}");
                }
                var indexST = e.Points[2].DataPoint.CollectionIndex;
                if (indexST >= 0)
                {
                    point = shortTermSf.DataPoints[indexST] as CategoricalDataPoint;
                    textBuilder.Append($"\r\n<color=red> ST = {point.Value}");
                }
                textBuilder.Append("</html>");
                e.Text = textBuilder.ToString();
                e.Element.BorderBoxStyle = BorderBoxStyle.FourBorders;
                e.Element.Location = new Point(e.Element.Location.X, 0);
            }
            catch { }

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
                //UseWaitCursor = false;
                SignalProccessing(binanceModel);
                
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
                var sortTerm_Sf=BinanceHelper.ShortTermFC(candels, currentPrice);
                var longTerm_SF = BinanceHelper.LongTermFC(candels, currentPrice);

                price.DataPoints.Clear();
                price.DataPoints.AddRange(candels.Select(c => new CategoricalDataPoint((double)c.ClosePrice, c.CloseTime)));



                PersonalMath price1 = new PersonalMath(candels.Select(c => c.ClosePrice).ToList());
                decimal percent = 10;
                decimal.TryParse(tb_percent.Text, out percent);
                price1.GetLocalMinMax(percent, out mins, out maxes);

                priceMin.DataPoints.Clear();
                foreach (var item in mins)
                    priceMin.DataPoints.Add(new CategoricalDataPoint( (double) item.value, candels[item.index].CloseTime));

                priceMax.DataPoints.Clear();
                foreach (var item in maxes)
                    priceMax.DataPoints.Add(new CategoricalDataPoint((double) item.value, candels[item.index].CloseTime));

                
                if (Decimal.TryParse(tb_support.Text, out decimal support))
                {
                    SupportLine.DataPoints.Clear();
                    SupportLine.DataPoints.Add(new CategoricalDataPoint((double)support, candels.First().CloseTime));
                    SupportLine.DataPoints.Add(new CategoricalDataPoint((double)support, candels.Last().CloseTime));
                }
                if (Decimal.TryParse(tb_resistance.Text, out decimal resitence))
                {
                    ResistanceLine.DataPoints.Clear();
                    ResistanceLine.DataPoints.Add(new CategoricalDataPoint((double)resitence, candels.First().CloseTime));
                    ResistanceLine.DataPoints.Add(new CategoricalDataPoint((double)resitence, candels.Last().CloseTime));
                }
                if (sortTerm_Sf != null)
                {
                    LineOne.DataPoints.Clear();
                    LineOne.DataPoints.Add(new CategoricalDataPoint(1, sortTerm_Sf.First().Time));
                    LineOne.DataPoints.Add(new CategoricalDataPoint(1, sortTerm_Sf.Last().Time));

                    shortTermSf.DataPoints.Clear();
                    shortTermSf.DataPoints.AddRange(sortTerm_Sf.Where(s=>s.Value!=0).Select(s => new CategoricalDataPoint((double)s.Value, s.Time)));
                    
                    PersonalMath st = new PersonalMath(sortTerm_Sf.Select(s => s.Value).ToList());
                    st.GetLocalMinMax(percent, out stMins, out stMaxes);
                    shortTermSfMin.DataPoints.Clear();
                    foreach (var item in stMins)
                        shortTermSfMin.DataPoints.Add(new CategoricalDataPoint((double)item.value, sortTerm_Sf[item.index].Time));

                    shortTermSfMax.DataPoints.Clear();
                    foreach (var item in stMaxes)
                        shortTermSfMax.DataPoints.Add(new CategoricalDataPoint((double)item.value, sortTerm_Sf[item.index].Time));

                }
                if (longTerm_SF != null)
                {
                    longTermSf.DataPoints.Clear();
                    longTermSf.DataPoints.AddRange(longTerm_SF.Where(s=>s.Value!=0).Select(s => new CategoricalDataPoint((double)s.Value, s.Time)));

                    PersonalMath lt = new PersonalMath(longTerm_SF.Select(s => s.Value).ToList());
                    lt.GetLocalMinMax(percent, out ltMins, out ltMaxes);
                    longTermSfMin.DataPoints.Clear();
                    foreach (var item in ltMins)
                        longTermSfMin.DataPoints.Add(new CategoricalDataPoint((double)item.value, longTerm_SF[item.index].Time ));

                    longTermSfMax.DataPoints.Clear();
                    foreach (var item in ltMaxes)
                        longTermSfMax.DataPoints.Add(new CategoricalDataPoint((double)item.value, longTerm_SF[item.index].Time));

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           
        }

        private void SignalProccessing(BinanceModel binanceModel)
        {
            if (binanceModel.Candels_1min == null || binanceModel.Candels_1min.Any() == false)
                return;
            var candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 500).Take(500).ToList();
            if (candels != null || candels.Count > 0)
                UpdateSignals(KlineInterval.OneMinute, candels);

            if(binanceModel.Candels_5min == null || binanceModel.Candels_5min.Any() == false)
                return;
            candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 500).Take(500).ToList();
            if (candels != null || candels.Count > 0)
                UpdateSignals(KlineInterval.FiveMinutes, candels);

            if (binanceModel.Candels_15min == null || binanceModel.Candels_15min.Any() == false)
                return;
            candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 500).Take(500).ToList();
            if (candels != null || candels.Count > 0)
                UpdateSignals(KlineInterval.FifteenMinutes, candels);

            if (binanceModel.Candels_30min == null || binanceModel.Candels_30min.Any() == false)
                return;
            candels = binanceModel.Candels_30min.Skip(binanceModel.Candels_30min.Count() - 500).Take(500).ToList();
            if (candels != null || candels.Count > 0)
                UpdateSignals(KlineInterval.ThirtyMinutes, candels);
        }

        private void UpdateSignals(KlineInterval klineInterval, List<IBinanceKline> candels)
        {
            try
            {
                decimal currentPrice = candels.Last().ClosePrice;
                UpdatePriceProgreccBar(currentPrice, klineInterval);

                var sortTerm_Sf = BinanceHelper.ShortTermFC(candels, currentPrice);
                var longTerm_SF = BinanceHelper.LongTermFC(candels, currentPrice);

                if (sortTerm_Sf == null || longTerm_SF == null)
                    return;

                PersonalMath price1 = new PersonalMath(candels.Select(c => c.ClosePrice).ToList());
                decimal percent = 10;
                decimal.TryParse(tb_percent.Text, out percent);
                price1.GetLocalMinMax(percent, out mins, out maxes);

                PersonalMath st = new PersonalMath(sortTerm_Sf.Select(s => s.Value).ToList());
                st.GetLocalMinMax(percent, out stMins, out stMaxes);
                shortTermSfMin.DataPoints.Clear();

                PersonalMath lt = new PersonalMath(longTerm_SF.Select(s => s.Value).ToList());
                lt.GetLocalMinMax(percent, out ltMins, out ltMaxes);

                int progressValue = 50;

                int priceAdditive = isAdditive(mins, maxes);
                int longAdditive = isAdditive(ltMins, ltMaxes);
                int shortAdditive = isAdditive(stMins, stMaxes);

                if (longAdditive + shortAdditive > 1)
                    progressValue -= 10;

                if (priceAdditive + longAdditive + shortAdditive > 2)
                    progressValue -= 15;

                if (sortTerm_Sf.Last().Value > 1 && longTerm_SF.Last().Value > 1)
                    progressValue -= 15;               
                
                
                if (longAdditive + shortAdditive < -1)
                    progressValue += 10;

                if (priceAdditive + longAdditive + shortAdditive < -2)
                    progressValue += 15;

                if (sortTerm_Sf.Last().Value < 1 && longTerm_SF.Last().Value < 1)
                    progressValue += 15;

                switch(klineInterval)
                {
                    case KlineInterval.OneMonth:
                        ProgressBar1_1.Value1 = progressValue;
                        break;
                    case KlineInterval.FiveMinutes:
                        ProgressBar5_1.Value1 = progressValue;
                        break;
                    case KlineInterval.FifteenMinutes:
                        ProgressBar15_1.Value1 = progressValue;
                        break;
                    case KlineInterval.ThirtyMinutes:
                        ProgressBar30_1.Value1 = progressValue;
                        break;
                }

                progressValue = 50;
                decimal resistence,support;
                const decimal treshold = (decimal)0.005;
                if (Decimal.TryParse(tb_resistance.Text, out resistence) && 
                    Decimal.TryParse(tb_support.Text, out support))
                {
                    if (currentPrice + treshold * resistence > resistence)
                    {
                        var ResistanceTouch = candels.Where(c => (c.ClosePrice + (decimal)treshold * resistence) > resistence).
                        Select(r => r.CloseTime);
                        List<decimal> ltList = new List<decimal>();
                        foreach (var rTime in ResistanceTouch)
                        {
                            var temp = longTerm_SF.Where(t => t.Time == rTime).Select(r => r.Value);
                            if(temp != null && temp.Any())
                                ltList.Add(temp.First());
                        }
                        Console.WriteLine(klineInterval.ToString());
                        ltList.ForEach(p => Console.WriteLine(p.ToString()));
                        decimal sum = 0;
                        foreach (var val in ltList)
                            sum += val;
                        sum = sum - ltList.Last();
                        sum = sum / (ltList.Count - 1);
                        if (ltList.Last() < sum)
                            progressValue = 20;
                    }
                    if (currentPrice - treshold * support < support)
                    {
                        var supportTouch = candels.Where(c => (c.ClosePrice - treshold * support) < support).
                        Select(r => r.CloseTime);
                        List<decimal> ltList = new List<decimal>();
                        foreach (var rTime in supportTouch)
                        {
                            var temp = longTerm_SF.Where(t => t.Time == rTime).Select(r => r.Value);
                            if (temp != null && temp.Any())
                                ltList.Add(temp.First());
                        }
                        Console.WriteLine(klineInterval.ToString());
                        ltList.ForEach(p => Console.WriteLine(p.ToString()));
                        decimal sum = 0;
                        foreach (var val in ltList)
                            sum += val;
                        sum = sum - ltList.Last();
                        sum = sum / (ltList.Count - 1);
                        if (ltList.Last() > sum)
                            progressValue = 80;
                    }
                }

                switch (klineInterval)
                {
                    case KlineInterval.OneMonth:
                        ProgressBar1_3.Value1 = progressValue;
                        break;
                    case KlineInterval.FiveMinutes:
                        ProgressBar5_3.Value1 = progressValue;
                        break;
                    case KlineInterval.FifteenMinutes:
                        ProgressBar15_3.Value1 = progressValue;
                        break;
                    case KlineInterval.ThirtyMinutes:
                        ProgressBar30_3.Value1 = progressValue;
                        break;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private int isAdditive(List<ArrayPoint> mins, List<ArrayPoint> maxes)
        {
            int minCount = mins.Count;
            int maxCount = maxes.Count;
            if (maxCount < 3 && minCount < 3)
                return 0;
            if(maxCount>=3)
            {
                if (maxes[maxCount - 1].value > maxes[maxCount - 2].value &&
                    maxes[maxCount - 2].value > maxes[maxCount - 3].value)
                    return 1;
            }
            if (minCount >= 3)
            {
                if (mins[minCount - 1].value < mins[minCount - 2].value &&
                    mins[minCount - 2].value < mins[minCount - 3].value)
                    return -1;
            }

            return 0;
        }

        private void UpdatePriceProgreccBar(decimal currentPrice, KlineInterval klineInterval)
        {
            if (!Decimal.TryParse(tb_resistance.Text, out decimal resistance))
                return;
            if (!Decimal.TryParse(tb_support.Text, out decimal support))
                return;
            if (resistance <= support)
                return;

            int value = 50;
            if (currentPrice > resistance)
                value = 100;
            if (currentPrice < support)
                value = 0;
            if(currentPrice <= resistance && currentPrice >= support)
            {
                decimal rsDiff = resistance - support;
                decimal psDiff = currentPrice - support;

                decimal percent = 100 * (rsDiff - psDiff) / rsDiff;
                value = decimal.ToInt32(percent);

            }
            switch (klineInterval)
            {
                case KlineInterval.OneMinute:
                    ProgressBar1_2.Value1 = value;
                    break;
                case KlineInterval.FiveMinutes:
                    ProgressBar5_2.Value1 = value;
                    break;
                case KlineInterval.FifteenMinutes:
                    ProgressBar15_2.Value1 = value;
                    break;
                case KlineInterval.ThirtyMinutes:
                    ProgressBar30_2.Value1 = value;
                    break;
            }
        }

        private void strategiesSfForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;
            BinanceDataCollector.Instance.DataReadyEvent -= OnDateReadyEvent;

        }

        private void tb_percent_TextChanged(object sender, EventArgs e)
        {
            UpdateStockSeries();
        }

        private void chk_series_CheckedChanged(object sender, EventArgs e)
        {
            var chkbox=(CheckBox)sender;
            switch(chkbox.Name)
            {
                case "chk_price":
                    if (chkbox.Checked)
                        radChartView1.Series.Add(price);
                    else
                        radChartView1.Series.Remove(price);
                    break;
                case "chk_priceMin":
                    if (chkbox.Checked)
                        radChartView1.Series.Add(priceMin);
                    else
                        radChartView1.Series.Remove(priceMin);
                    break;
                case "chk_priceMax":
                    if (chkbox.Checked)
                        radChartView1.Series.Add(priceMax);
                    else
                        radChartView1.Series.Remove(priceMax);
                    break;
                case "chk_ST":
                    if (chkbox.Checked)
                        radChartView1.Series.Add(shortTermSf);
                    else
                        radChartView1.Series.Remove(shortTermSf);
                    break;
                case "chk_stMin":
                    if (chkbox.Checked)
                        radChartView1.Series.Add(shortTermSfMin);
                    else
                        radChartView1.Series.Remove(shortTermSfMin);
                    break;
                case "chk_stMax":
                    if (chkbox.Checked)
                        radChartView1.Series.Add(shortTermSfMax);
                    else
                        radChartView1.Series.Remove(shortTermSfMax);
                    break;
                case "chk_lt":
                    if (chkbox.Checked)
                        radChartView1.Series.Add(longTermSf);
                    else
                        radChartView1.Series.Remove(longTermSf);
                    break;
                case "chk_ltMin":
                    if (chkbox.Checked)
                        radChartView1.Series.Add(longTermSfMin);
                    else
                        radChartView1.Series.Remove(longTermSfMin);
                    break;
                case "chk_ltMax":
                    if (chkbox.Checked)
                        radChartView1.Series.Add(longTermSfMax);
                    else
                        radChartView1.Series.Remove(longTermSfMax);
                    break;
            }
        }

        private void tb_resistance_TextChanged(object sender, EventArgs e)
        {
            if (candels == null) return;
            if (Decimal.TryParse(tb_resistance.Text, out decimal resitence))
            {
                UpdateData();
            }
        }

        private void tb_support_TextChanged(object sender, EventArgs e)
        {
            if (candels == null) return;
            if (Decimal.TryParse(tb_support.Text, out decimal support))
            {
                UpdateData();
            }
            
        }
    }
}
