﻿using Binance.Net;
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
    public partial class strategiesSfMaForm : Telerik.WinControls.UI.RadForm
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

        public strategiesSfMaForm()
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
        
        private  Dictionary<string, LineSeries> MADic { get; set; }
        private string[] maList = { "MA20 1Min", "MA50 1Min", "MA20 5Min", "MA50 5Min", "MA20 15Min",
            "MA50 15Min", "MA20 30Min", "MA50 30Min", "MA20 1Hour", "MA50 1Hour"};

        private delegate void UpdateFormDelegate();

        private void strategiesSfMaForm_Load(object sender, EventArgs e)
        {
            MADic = new Dictionary<string, LineSeries>();

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
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;
            OnCandleReadyEvent(interval, coinName);
            sendLog("Initializing ...");
        }

        //private void OnDateReadyEvent()
        //{
        //    OnCandleReadyEvent(interval, coinName);
        //   // BinanceDataCollector.Instance.DataReadyEvent -= OnDateReadyEvent;
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
            radChartView1.ShowTrackBall = true;

        }
        
        private void InitSeries()
        {
            shortTermSf.HorizontalAxis = dateTimeCategoricalAxis1;
            shortTermSf.VerticalAxis = linearAxis2;
            shortTermSf.BorderColor = Color.Red;
            shortTermSf.LegendTitle = "ShortTermSF";

            longTermSf.HorizontalAxis = dateTimeCategoricalAxis1;
            longTermSf.VerticalAxis = linearAxis2;
            longTermSf.BorderColor = Color.Green;
            longTermSf.LegendTitle = "LongTermSF";

            LineOne.HorizontalAxis = dateTimeCategoricalAxis1;
            LineOne.VerticalAxis = linearAxis2;
            LineOne.BorderColor = Color.Black;
            LineOne.BackColor = Color.Black;
            LineOne.IsVisibleInLegend = false;
            LineOne.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            price.HorizontalAxis = dateTimeCategoricalAxis1;
            price.VerticalAxis = linearAxis1;
            price.BorderColor = Color.Blue;
            price.LegendTitle = "Price";

            radChartView1.Series.Add(price);
            radChartView1.Series.Add(longTermSf);
            radChartView1.Series.Add(shortTermSf);

            int i = 1;
            foreach(var item in maList)
            {
                LineSeries ma = new LineSeries();
                ma.HorizontalAxis = dateTimeCategoricalAxis1;
                ma.VerticalAxis = linearAxis1;
                ma.LegendTitle = item;
                ma.CategoryMember = "Date";
                ma.ValueMember = "Sma";
                ma.Name = item;
                if(item.Contains("20"))
                    ma.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                ma.BorderColor = getColore(item);
                MADic.Add(item, ma);
                radChartView1.Series.Add(ma);
                i++;
            }
            radChartView1.Series.Add(LineOne);
        }

        private Color getColore(string item)
        {
            Color temp = Color.AliceBlue;
            switch(item)
            {
                case "MA20 1Min":
                case "MA50 1Min":
                    temp = Color.FromArgb(180, 230, 30);
                    break;
                case "MA20 5Min":
                case "MA50 5Min":
                    temp = Color.FromArgb(255, 130, 255);
                    break;
                case "MA20 15Min":
                case "MA50 15Min":
                    temp = Color.FromArgb(255, 120, 0);
                    break;
                case "MA20 30Min":
                case "MA50 30Min":
                    temp = Color.FromArgb(220, 220, 0);
                    break;
                case "MA20 1Hour":
                case "MA50 1Hour":
                    temp = Color.FromArgb(120, 10, 0);
                    break;
            }
            return temp;
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
                for (int i = 3; i < e.Points.Count; i++)
                {
                    var idx = e.Points[i].DataPoint.CollectionIndex;
                    if (idx >= 0)
                    {
                        point = e.Points[i].Series.DataPoints[idx] as CategoricalDataPoint;
                        textBuilder.Append($"\r\n<color=black> {e.Points[i].Series.Name} = {point.Value}");
                    }
                }
                //int pointsIndex = 3;
                //if(chb_MA5_1min.Checked)
                //{
                //    var indexMa5 = e.Points[pointsIndex].DataPoint.CollectionIndex;
                //    if(indexMa5>=0)
                //    {
                //        point = MA5.DataPoints[indexMa5] as CategoricalDataPoint;
                //        textBuilder.Append($"\r\n<color=Brown> MA5 = {point.Value}");
                //    }
                //    pointsIndex++;
                //}
                //if (chb_MA20_1min.Checked)
                //{
                //    var indexMa20 = e.Points[pointsIndex].DataPoint.CollectionIndex;
                //    if (indexMa20 >= 0)
                //    {
                //        point = MA20.DataPoints[indexMa20] as CategoricalDataPoint;
                //        textBuilder.Append($"\r\n<color=MediumPurple> MA20 = {point.Value}");
                //    }
                //    pointsIndex++;
                //}
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

                List<IBinanceKline> refCandles = new List<IBinanceKline>();
                               
                foreach (var strInterval in Intervals)
                {
                    if (Enum.TryParse(strInterval, out KlineInterval _interval) == false)
                        continue;
                    string ma50Key = string.Empty;
                    string ma20Key = string.Empty;
                    switch (_interval)
                    {
                        case KlineInterval.OneMinute:
                            if (binanceModel.Candels_1min == null || binanceModel.Candels_1min.Any() == false) return;
                            candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 500).Take(500).ToList();
                            ma50Key = "MA50 1Min";
                            ma20Key = "MA20 1Min";
                            break;
                        case KlineInterval.FiveMinutes:
                            if (binanceModel.Candels_5min == null || binanceModel.Candels_5min.Any() == false) return;
                            candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 500).Take(500).ToList();
                            ma50Key = "MA50 5Min";
                            ma20Key = "MA20 5Min";
                            break;
                        case KlineInterval.FifteenMinutes:
                            if (binanceModel.Candels_15min == null || binanceModel.Candels_15min.Any() == false) return;

                            candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 500).Take(500).ToList();
                            ma50Key = "MA50 15Min";
                            ma20Key = "MA20 15Min";
                            break;
                        case KlineInterval.ThirtyMinutes:
                            if (binanceModel.Candels_30min == null || binanceModel.Candels_30min.Any() == false) return;

                            candels = binanceModel.Candels_30min.Skip(binanceModel.Candels_30min.Count() - 500).Take(500).ToList(); ;
                            ma50Key = "MA50 30Min";
                            ma20Key = "MA20 30Min";
                            break;
                        case KlineInterval.OneHour:
                            if (binanceModel.Candels_60min == null || binanceModel.Candels_60min.Any() == false) return;

                            candels = binanceModel.Candels_60min.Skip(binanceModel.Candels_60min.Count() - 501).Take(500).ToList();
                            ma50Key = "MA50 1Hour";
                            ma20Key = "MA20 1Hour";
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
                    if (interval == _interval)
                        refCandles = candels;
                    if (string.IsNullOrEmpty(ma50Key) == false)
                    {
                        var Qutoes = BinanceHelper.GetQuotes(candels);
                        var ma50 = BinanceHelper.SMA(Qutoes, 50);
                        var ma20 = BinanceHelper.SMA(Qutoes, 20);
                        if(refCandles!=null && refCandles.Any())
                        {
                            var startTine = refCandles.First().CloseTime;
                            MADic[ma50Key].DataSource = ma50.Where(m=>m.Date>startTine);
                            MADic[ma20Key].DataSource = ma20.Where(m => m.Date > startTine);
                        }
                        else
                        {
                            MADic[ma50Key].DataSource = ma50;
                            MADic[ma20Key].DataSource = ma20;
                        }
                    }
                }
                if (refCandles == null || refCandles.Count == 0) return;
                var sortTerm_Sf=BinanceHelper.ShortTermFC(refCandles, currentPrice);
                var longTerm_SF = BinanceHelper.LongTermFC(refCandles, currentPrice);

                LineOne.DataPoints.Clear();
                LineOne.DataPoints.Add(new CategoricalDataPoint(1, sortTerm_Sf.First().Time));
                LineOne.DataPoints.Add(new CategoricalDataPoint(1, sortTerm_Sf.Last().Time));

                price.DataPoints.Clear();
                price.DataPoints.AddRange(refCandles.Select(c => new CategoricalDataPoint((double)c.ClosePrice, c.CloseTime)));

                if (sortTerm_Sf != null)
                {
                    shortTermSf.DataPoints.Clear();
                    shortTermSf.DataPoints.AddRange(sortTerm_Sf.Where(s=>s.Value!=0).Select(s => new CategoricalDataPoint((double)s.Value, s.Time)));
                }
                if(longTerm_SF != null)
                {
                    longTermSf.DataPoints.Clear();
                    longTermSf.DataPoints.AddRange(longTerm_SF.Where(s=>s.Value!=0).Select(s => new CategoricalDataPoint((double)s.Value, s.Time)));
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
        private void chb_MA_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (MADic.ContainsKey(checkBox.Text))
            {
                if (checkBox.Checked)
                    radChartView1.Series.Add(MADic[checkBox.Text]);
                else
                    radChartView1.Series.Remove(MADic[checkBox.Text]);
            }
            
        }

        
    }
}
