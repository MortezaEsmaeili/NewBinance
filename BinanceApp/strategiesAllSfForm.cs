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
   public partial class strategiesAllSfForm : Telerik.WinControls.UI.RadForm
    {
        List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(),
            KlineInterval.FiveMinutes.ToString(),
            KlineInterval.FifteenMinutes.ToString(),
            KlineInterval.ThirtyMinutes.ToString(),
            KlineInterval.OneHour.ToString(),
            KlineInterval.FourHour.ToString(),
            KlineInterval.OneDay.ToString()
        };
        
        
        private KlineInterval interval = KlineInterval.OneMinute;

        private List<IBinanceKline> candels;
        private Dictionary<string, SFSeries> CoinSFDic = new Dictionary<string, SFSeries>();
        public strategiesAllSfForm()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();

        LinearAxis verticalAxis = new LinearAxis();
        CategoricalAxis horizontalAxis = new CategoricalAxis();
        BarSeries barSeriesDMH = new BarSeries();
        BarSeries barSeriesDML = new BarSeries();

        private delegate void UpdateSfFormDelegate(string coin);
        private bool initialized = false;

        private void strategiesAllSfForm_Load(object sender, EventArgs e)
        {
            
            initAxis();
            WireEvents();

            this.radDropDownList4.DataSource = Intervals;
            foreach(var coin in BinanceDataCollector.Instance.CoinNames)
            {
                chCoinList.Items.Add(coin, true);
                CoinSFDic.Add(coin, new SFSeries());
            }
            InitSeries();

            radChartView1.BackColor = Color.AliceBlue;
            radChartView1.GetArea<CartesianArea>().ShowGrid = true;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall();
            
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);
           
            foreach (var coin in chCoinList.CheckedItems)
            {
                OnCandleReadyEvent(interval, coin.ToString());
            }
            BinanceDataCollector.Instance.DataReadyEvent += OnDateReadyEvent;
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;
            initialized = true;
        }
        private delegate void delegateFunc();
        private void OnDateReadyEvent()
        {
            if (DrawWasDone == false)
            {
                foreach (var coin in chCoinList.CheckedItems)
                {
                    OnCandleReadyEvent(interval, coin.ToString());
                }
            }
            if(InvokeRequired)
            {
                this.Invoke( new delegateFunc( UpdateBarSeries));
            }
            else
                UpdateBarSeries();
        }

        private void OnCandleReadyEvent(KlineInterval eventInterval, string coin)
        {
            try
            {
                for(int k=0; k<chCoinList.CheckedItems.Count; k++)
                {
                    if(chCoinList.Items[k].ToString()==coin)
                    {
                        if (InvokeRequired)
                        {
                            UpdateSfFormDelegate d = new UpdateSfFormDelegate(UpdateDataSF);
                            this.Invoke(d, coin);
                        }
                        else
                            UpdateDataSF(coin);
                        return;
                    }
                }
                //if (eventInterval == interval)
                        
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
            linearAxis1.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis1.IsPrimary = true;
            linearAxis1.TickOrigin = null;

            verticalAxis.AxisType = Telerik.Charting.AxisType.Second;
            verticalAxis.IsPrimary = true;
            verticalAxis.TickOrigin = null;

            this.radChartView1.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1, linearAxis1});

            LassoZoomController lassoZoomController = new LassoZoomController();
            radChartView1.Controllers.Add(lassoZoomController);

            radChartView1.Area.View.Palette = KnownPalette.Rainbow;
            radChartView1.ShowLegend = true;

            LassoZoomController lassoZoomController2 = new LassoZoomController();
            radChartView2.Axes.AddRange(new Axis[] { verticalAxis, horizontalAxis });
            radChartView2.Controllers.Add(lassoZoomController2);
            radChartView2.Area.View.Palette = KnownPalette.Flower;
            radChartView2.ShowLegend = true;
            radChartView2.ShowGrid = true;
        }

        private void InitSeries()
        {
            foreach(var item in CoinSFDic)
            {
                item.Value.shortTermSf.HorizontalAxis = dateTimeCategoricalAxis1;
                item.Value.shortTermSf.VerticalAxis = linearAxis1;
                item.Value.shortTermSf.LegendTitle = "ST_SF_"+item.Key;

                item.Value.longTermSf.HorizontalAxis = dateTimeCategoricalAxis1;
                item.Value.longTermSf.VerticalAxis = linearAxis1;
                item.Value.longTermSf.LegendTitle = "LT_SF_"+item.Key;

                radChartView1.Series.Add(item.Value.shortTermSf);
                radChartView1.Series.Add(item.Value.longTermSf);                
            }
            barSeriesDMH.HorizontalAxis = horizontalAxis;
            barSeriesDMH.VerticalAxis = verticalAxis;
            barSeriesDMH.CombineMode = ChartSeriesCombineMode.Cluster;
            barSeriesDMH.LegendTitle = "DMH";


            barSeriesDML.HorizontalAxis = horizontalAxis;
            barSeriesDML.VerticalAxis = verticalAxis;
            barSeriesDML.CombineMode = ChartSeriesCombineMode.Cluster;
            barSeriesDML.LegendTitle = "DML";

            radChartView2.Series.Add(barSeriesDMH);
            radChartView2.Series.Add(barSeriesDML);
            foreach (DataPointElement pointElement in barSeriesDML.Children)
            {
                pointElement.BorderWidth = 0;
            }
            foreach (DataPointElement pointElement in barSeriesDMH.Children)
            {
                pointElement.BorderWidth = 0;
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

       
        void radDropDownList4_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out interval);

            UpdateData();
        }

        
        
        public void UpdateData()
        {
            foreach (var item in chCoinList.CheckedItems)
                UpdateDataSF(item.ToString());
            UpdateBarSeries();
        }

        private void UpdateBarSeries()
        {
            try
            {
                if(barSeriesDMH.DataPoints!=null)
                    barSeriesDMH.DataPoints.Clear();
                if(barSeriesDML.DataPoints!=null)
                    barSeriesDML.DataPoints.Clear();
                foreach (var coin in BinanceDataCollector.Instance.CoinNames)
                {
                    var binanceModel = BinanceDataCollector.Instance.GetBinance(coin);
                    if(binanceModel!=null)
                    {
                        if(binanceModel.DMHDic!=null && binanceModel.DMHDic.Any())
                            barSeriesDMH.DataPoints.Add(new CategoricalDataPoint((double)binanceModel.DMHDic[interval], coin));
                        else
                            barSeriesDMH.DataPoints.Add(new CategoricalDataPoint(0, coin));
                        
                        if (binanceModel.DMLDic != null && binanceModel.DMLDic.Any())
                            barSeriesDML.DataPoints.Add(new CategoricalDataPoint((double)binanceModel.DMLDic[interval], coin));
                        else
                            barSeriesDML.DataPoints.Add(new CategoricalDataPoint(0, coin));
                    }
                }
                
            }
            catch { }
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
            this.radDropDownList4.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList4_SelectedIndexChanged);

        }

        private void radChartView1_DoubleClick(object sender, EventArgs e)
        {
            this.radChartView1.Zoom(1, 1);

        }

        bool DrawWasDone = false;         
        private void UpdateDataSF(string coin)
        {
            try
            {
                var binanceModel = BinanceDataCollector.Instance.GetBinance(coin);
                if (binanceModel == null) return;
                if (binanceModel.PriceList == null || binanceModel.PriceList.Count == 0)
                    return;
                decimal currentPrice = binanceModel.PriceList.Last().Value;
                switch (interval)
                {
                    case KlineInterval.OneMinute:
                        if (binanceModel.Candels_1min == null) return;
                        candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FiveMinutes:
                        if (binanceModel.Candels_5min == null) return;
                        candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FifteenMinutes:
                        if (binanceModel.Candels_15min == null) return;
                        candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.ThirtyMinutes:
                        if (binanceModel.Candels_30min == null) return;
                        candels = binanceModel.Candels_30min.Skip(binanceModel.Candels_30min.Count() - 500).Take(500).ToList(); ;
                        break;
                    case KlineInterval.OneHour:
                        if (binanceModel.Candels_60min == null) return;
                        candels = binanceModel.Candels_60min.Skip(binanceModel.Candels_60min.Count() - 500).Take(500).ToList();
                        break;
                    case KlineInterval.FourHour:
                        if (binanceModel.Candels_4hour== null) return;
                        candels = binanceModel.Candels_4hour.Skip(binanceModel.Candels_4hour.Count() - 500).Take(500).ToList(); ;
                        break;
                    case KlineInterval.OneDay:
                        if (binanceModel.Candels_oneDay == null) return;
                        candels = binanceModel.Candels_oneDay.Skip(binanceModel.Candels_oneDay.Count() - 500).Take(500).ToList(); ;
                        break;
                }
                if (candels == null || candels.Count == 0) return;
                var sortTerm_Sf = BinanceHelper.ShortTermFC(candels, currentPrice).Where(x=>x.Value!=0);
                var longTerm_SF = BinanceHelper.LongTermFC(candels, currentPrice).Where(x=>x.Value!=0);
               
                if (sortTerm_Sf != null)
                {
                    if(CoinSFDic[coin].shortTermSf.DataPoints!=null)
                        CoinSFDic[coin].shortTermSf.DataPoints.Clear();
                    CoinSFDic[coin].shortTermSf.DataPoints.AddRange(sortTerm_Sf.Select(s => new CategoricalDataPoint((double)s.Value, s.Time)));
                }
                if(longTerm_SF != null )
                {
                    if(CoinSFDic[coin].longTermSf.DataPoints!=null)
                        CoinSFDic[coin].longTermSf.DataPoints.Clear();
                    CoinSFDic[coin].longTermSf.DataPoints.AddRange(longTerm_SF.Select(s => new CategoricalDataPoint((double)s.Value, s.Time)));
                }
                DrawWasDone = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           
        }

        private void chCoinList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (initialized == false) return;
                var coin = chCoinList.Items[e.Index].ToString();
                if (chCoinList.GetItemChecked(e.Index) == false)
                {
                    if (cb_LT.Checked)
                        radChartView1.Series.Add(CoinSFDic[coin].longTermSf);
                    if (cb_ST.Checked)
                        radChartView1.Series.Add(CoinSFDic[coin].shortTermSf);
                }
                else
                {
                    if (radChartView1.Series.Count == 0)
                        return;
                    if (cb_ST.Checked)
                        radChartView1.Series.Remove(CoinSFDic[coin].shortTermSf);
                    if (cb_LT.Checked)
                        radChartView1.Series.Remove(CoinSFDic[coin].longTermSf);
                }
            }
            catch { }
        }

        private void cb_ST_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_ST.Checked)
            {
                foreach(var coin in chCoinList.CheckedItems)
                {
                    radChartView1.Series.Add(CoinSFDic[coin.ToString()].shortTermSf);
                }
            }
            else
            {
                foreach (var coin in chCoinList.CheckedItems)
                {
                    radChartView1.Series.Remove(CoinSFDic[coin.ToString()].shortTermSf);
                }
            }
        }

        private void cb_LT_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_LT.Checked)
            {
                foreach (var coin in chCoinList.CheckedItems)
                {
                    radChartView1.Series.Add(CoinSFDic[coin.ToString()].longTermSf);
                }
            }
            else
            {
                foreach (var coin in chCoinList.CheckedItems)
                {
                    radChartView1.Series.Remove(CoinSFDic[coin.ToString()].longTermSf);
                }
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radChartView2_DoubleClick(object sender, EventArgs e)
        {
            this.radChartView2.Zoom(1, 1);
        }

        private void strategiesAllSfForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDateReadyEvent;
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;
        }

        private void cb_Selection_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_Selection.Checked)
            {
                for (int k = 0; k < chCoinList.Items.Count; k++)
                    if (chCoinList.GetItemChecked(k) == false)
                        chCoinList.SetItemChecked(k, true);

            }
            else
            {
                for (int k = 0; k < chCoinList.Items.Count; k++)
                    if (chCoinList.GetItemChecked(k) == true)
                        chCoinList.SetItemChecked(k, false);
            }
        }
    }
    public class SFSeries
    {
        public LineSeries shortTermSf = new LineSeries();
        public LineSeries longTermSf = new LineSeries();
    }
}
