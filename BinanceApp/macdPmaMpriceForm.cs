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
    public partial class macdPmaMpriceForm : Telerik.WinControls.UI.RadForm
    {        
        private string coinName = string.Empty;
        private BinanceModel binanceModel;

       // private List<IBinanceKline> candels;
        private MacdParams macdParams;
        public macdPmaMpriceForm()
        {
            InitializeComponent();
            macdParams = new MacdParams(12, 26, 9);

        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();
 //       Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

   //     LineSeries price = new LineSeries();
    //    LineSeries shortTermSf = new LineSeries();
   //     LineSeries longTermSf = new LineSeries();
   //     LineSeries LineOne = new LineSeries();
        
        private  Dictionary<string, LineSeries> MADic { get; set; }
        private string[] maList = { "1Min", "5Min", "15Min", "30Min", "1H",
            "2H", "4H", "1D", "1W"};

        private delegate void UpdateFormDelegate();

        private void strategiesSfMaForm_Load(object sender, EventArgs e)
        {
            MADic = new Dictionary<string, LineSeries>();

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
//            OnCandleReadyEvent(interval, coinName);
            sendLog("Initializing ...");
        }


        private void sendLog(string strLog)
        {

        }
                
        private void OnCandleReadyEvent(KlineInterval interval, string coin)
        {
            try
            {
     //           if (coin != coinName || interval!= KlineInterval.OneMinute) return;
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
            dateTimeCategoricalAxis1.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis1.IsPrimary = true;
            dateTimeCategoricalAxis1.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis1.LabelFormat = "{0:M/d HH:mm:ss}";//"{0:HH:mm:ss}";
            dateTimeCategoricalAxis1.MajorTickInterval = 30;

            linearAxis1.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis1.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis1.IsPrimary = true;
            linearAxis1.TickOrigin = null;

  /*          linearAxis2.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis2.IsPrimary = true;
            linearAxis2.TickOrigin = null;*/

            this.radChartView1.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1/*, linearAxis2*/});

            LassoZoomController lassoZoomController = new LassoZoomController();
            radChartView1.Controllers.Add(lassoZoomController);
           // radChartView1.Area.View.Palette = KnownPalette.Material;
            radChartView1.ShowLegend = true;
            radChartView1.ShowTrackBall = true;

        }
        
        private void InitSeries()
        {
          /*  LineOne.HorizontalAxis = dateTimeCategoricalAxis1;
            LineOne.VerticalAxis = linearAxis2;
            LineOne.BorderColor = Color.Black;
            LineOne.BackColor = Color.Black;
            LineOne.IsVisibleInLegend = false;
            LineOne.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;*/

            int i = 1;
            foreach(var item in maList)
            {
                LineSeries ma = new LineSeries();
                ma.HorizontalAxis = dateTimeCategoricalAxis1;
                ma.VerticalAxis = linearAxis1;
                ma.LegendTitle = item;
                ma.CategoryMember = "Time";//"Date";
                ma.ValueMember = "Value";//"Sma";
                ma.Name = item;
    //            if(item.Contains("20"))
      //              ma.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                ma.BorderColor = getColore(item);
                MADic.Add(item, ma);
      //          radChartView1.Series.Add(ma);
                i++;
            }
       //     radChartView1.Series.Add(LineOne);
        }

        private Color getColore(string item)
        {
            Color temp = Color.AliceBlue;
            switch(item)
            {
                case "1Min":
                    temp = Color.FromArgb(180, 230, 30);
                    break;
                case "5Min":
                    temp = Color.FromArgb(255, 130, 255);
                    break;
                case "15Min":
                    temp = Color.FromArgb(255, 120, 0);
                    break;
                case "30Min":
                    temp = Color.FromArgb(220, 220, 0);
                    break;
                case "1H":
                    temp = Color.FromArgb(220, 10, 0);
                    break;
                case "2H":
                    temp = Color.FromArgb(120, 120, 120);
                    break;
                case "4H":
                    temp = Color.FromArgb(120, 120, 0);
                    break;
                case "1D":
                    temp = Color.FromArgb(120, 10, 0);
                    break;
                case "1W":
                    temp = Color.FromArgb(10, 10, 0);
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
        //    trackBallElement.TextNeeded += new TextNeededEventHandler(trackball_TextNeeded);
        //    this.radChartView1.Controllers.Add(trackBallElement);
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
         /*       var point = price.DataPoints[index] as CategoricalDataPoint;
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
                }/*
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
                e.Element.Location = new Point(e.Element.Location.X, 0);*/
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
                binanceModel = BinanceDataCollector.Instance.GetBinance(coinName);
                if (binanceModel == null) return;
                if (binanceModel.PriceList == null || binanceModel.PriceList.Count == 0)
                    return;

                List<IBinanceKline> refCandles = new List<IBinanceKline>();

                if(binanceModel.Candels_1min != null && binanceModel.Candels_1min.Count>500)
                {
                    var candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 500).Take(500).ToList();
                    
                    var result = Macd_Sma_priceCalc(candels);                   
                   
                    MADic["1Min"].DataSource = result;// ma100.Where(m => m.Date > startTime);
                }
                if (binanceModel.Candels_5min != null && binanceModel.Candels_5min.Count > 500)
                {
                    var candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 500).Take(500).ToList();

                    MADic["5Min"].DataSource = Macd_Sma_priceCalc(candels); 
                }
                if (binanceModel.Candels_15min != null && binanceModel.Candels_15min.Count > 500)
                {
                    var candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 500).Take(500).ToList();

                    MADic["15Min"].DataSource = Macd_Sma_priceCalc(candels); 
                }
                if (binanceModel.Candels_30min != null )
                {
                    MADic["30Min"].DataSource = Macd_Sma_priceCalc(binanceModel.Candels_30min); 
                }
                if (binanceModel.Candels_60min != null)
                {
                    MADic["1H"].DataSource = Macd_Sma_priceCalc(binanceModel.Candels_60min);
                }
                if (binanceModel.Candels_2hour != null)
                {
                    MADic["2H"].DataSource = Macd_Sma_priceCalc(binanceModel.Candels_2hour);
                }
                if (binanceModel.Candels_4hour != null )
                {
                    MADic["4H"].DataSource = Macd_Sma_priceCalc(binanceModel.Candels_4hour);
                }
                if (binanceModel.Candels_oneWeek != null)
                {
                    MADic["1W"].DataSource = Macd_Sma_priceCalc(binanceModel.Candels_oneWeek);
                }
                if (binanceModel.Candels_oneDay != null)
                {
                    MADic["1D"].DataSource = Macd_Sma_priceCalc(binanceModel.Candels_oneDay);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                      
        }
        private List<WeightedValue> Macd_Sma_priceCalc(List<IBinanceKline> candels)
        {
            var Qutoes = BinanceHelper.GetQuotes(candels);
            var ma100 = BinanceHelper.SMA(Qutoes, 100);
            var macd = BinanceHelper.GetMacd(candels, macdParams);
            var minLen = (new List<int> { Qutoes.Count, ma100.Count, macd.Count }).Min();
            var result = new List<WeightedValue>(minLen);
            for (int i = minLen; i > 0; i--)
            {
                result.Add(new WeightedValue
                {
                    Value = -ma100[ma100.Count - i].Sma.Value - macd[macd.Count - i].Macd.Value + Qutoes[Qutoes.Count - i].Close,
                    Time = ma100[ma100.Count - i].Date.ToLocalTime()
                });
            }
            return result;
        }
        private void strategiesSfForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;

        }
        private void chb_MA_CheckedChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex) { }
            
        }

        
    }
}
