using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.Business;
using BinanceApp.DataModel;
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
    public partial class MA100MHMLForm : Form
    {
        private string coinName = "BTCUSDT";
        private BinanceModel binanceModel;

        #region ChartParameter
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 =
            new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();

        private Dictionary<string, LineSeries> MADic { get; set; }
        private string[] maList = { "1Min", "5Min", "15Min", "30Min", "1H",
            "2H", "4H", "1D", "1W"};

        LineSeries price = new LineSeries();

        private Dictionary<string, SteplineSeries> MHDic { get; set; }
        private Dictionary<string, SteplineSeries> MLDic { get; set; }

        #endregion

        private delegate void UpdateFormDelegate();

        public MA100MHMLForm()
        {
            InitializeComponent();
        }

        private void MA100MHMLForm_Load(object sender, EventArgs e)
        {
            initAxis();
            InitSeries();

            MADic = new Dictionary<string, LineSeries>();
            MHDic = new Dictionary<string, SteplineSeries>();
            MLDic = new Dictionary<string, SteplineSeries>();

            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;
            radChartView1.BackColor = Color.AliceBlue;
            radChartView1.GetArea<CartesianArea>().ShowGrid = true;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall();
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;
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

            this.radChartView1.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1,
             linearAxis1});

            LassoZoomController lassoZoomController = new LassoZoomController();
            radChartView1.Controllers.Add(lassoZoomController);
            radChartView1.ShowLegend = true;

        }
        private void InitSeries()
        {
            price.HorizontalAxis = dateTimeCategoricalAxis1;
            price.VerticalAxis = linearAxis1;
            price.LegendTitle = "Price";
            price.BorderColor = Color.Black;
            price.BackColor = Color.Black;
            price.BorderWidth = 2;

            foreach (var item in maList)
            {
                LineSeries ma = new LineSeries();
                ma.HorizontalAxis = dateTimeCategoricalAxis1;
                ma.VerticalAxis = linearAxis1;
                ma.LegendTitle = $"MA {item}";
                ma.CategoryMember = "Time";
                ma.ValueMember = "Value";
                ma.Name = $"MA {item}";
                ma.BorderColor = getColore(item);
                MADic.Add(item, ma);

                SteplineSeries steplineSeries = new SteplineSeries();
                steplineSeries.HorizontalAxis = dateTimeCategoricalAxis1;
                steplineSeries.VerticalAxis = linearAxis1;
                steplineSeries.LegendTitle = $"MH {item}";
                steplineSeries.CategoryMember = "Time";
                steplineSeries.ValueMember = "Value";
                steplineSeries.Name = $"MH {item}";
                steplineSeries.BorderColor = getColore(item);
                steplineSeries.BorderWidth = 2;
                MHDic.Add(item, steplineSeries);

                SteplineSeries steplineSeries1 = new SteplineSeries();
                steplineSeries1.HorizontalAxis = dateTimeCategoricalAxis1;
                steplineSeries1.VerticalAxis = linearAxis1;
                steplineSeries1.LegendTitle = $"ML {item}";
                steplineSeries1.CategoryMember = "Time";
                steplineSeries1.ValueMember = "Value";
                steplineSeries1.Name = $"ML {item}";
                steplineSeries1.BorderColor = getColore(item);
                steplineSeries1.BorderWidth = 2;
                steplineSeries1.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                MLDic.Add(item, steplineSeries1);
            }

            radChartView1.Series.Add(price);
        }
        private Color getColore(string item)
        {
            Color temp = Color.AliceBlue;
            switch (item)
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
        }
        private void OnCandleReadyEvent(KlineInterval interval, string coin)
        {
            try
            {
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
        private void UpdateStockSeries()
        {
            try
            {
                binanceModel = BinanceDataCollector.Instance.GetBinance(coinName);
                if (binanceModel == null) return;
                if (binanceModel.PriceList == null || binanceModel.PriceList.Count == 0)
                    return;

                var tempPrice = new ConcurrentBag<CategoricalDataPoint>();


                binanceModel.Candels_1min.Where(s => s.CloseTime >= binanceModel.MacdResult1Min.First().Date).AsParallel().ForAll(item =>
                { tempPrice.Add(new CategoricalDataPoint((double)item.ClosePrice, item.CloseTime)); });
                if (tempPrice.Any())
                {
                    price.DataPoints.Clear();
                    price.DataPoints.AddRange(tempPrice);
                }
                List<IBinanceKline> refCandles = new List<IBinanceKline>();

                if (binanceModel.Candels_1min != null && binanceModel.Candels_1min.Count >= 500)
                {
                    var candels = binanceModel.Candels_1min.Skip(binanceModel.Candels_1min.Count() - 500).Take(500).ToList();
                    CalculateSeries("1Min", candels, binanceModel.MajorHighPrice1Min, binanceModel.MajorLowPrice1Min);
                }
                if (binanceModel.Candels_5min != null && binanceModel.Candels_5min.Count >= 500)
                {
                    var candels = binanceModel.Candels_5min.Skip(binanceModel.Candels_5min.Count() - 500).Take(500).ToList();
                    CalculateSeries("5Min", candels, binanceModel.MajorHighPrice5Min, binanceModel.MajorLowPrice5Min);
                }
                if (binanceModel.Candels_15min != null && binanceModel.Candels_15min.Count >= 500)
                {
                    var candels = binanceModel.Candels_15min.Skip(binanceModel.Candels_15min.Count() - 500).Take(500).ToList();
                    CalculateSeries("15Min", candels, binanceModel.MajorHighPrice15Min, binanceModel.MajorLowPrice15Min);
                }
                if (binanceModel.Candels_30min != null)
                {
                    CalculateSeries("30Min", binanceModel.Candels_30min, binanceModel.MajorHighPrice30Min, binanceModel.MajorLowPrice30Min);
                }
                if (binanceModel.Candels_60min != null)
                {
                    CalculateSeries("1H", binanceModel.Candels_60min, binanceModel.MajorHighPrice60Min, binanceModel.MajorLowPrice60Min);
                }
                if (binanceModel.Candels_2hour != null)
                {
                    CalculateSeries("2H", binanceModel.Candels_2hour, binanceModel.MajorHighPrice2hour, binanceModel.MajorLowPrice2hour);
                }
                if (binanceModel.Candels_4hour != null)
                {
                    CalculateSeries("4H", binanceModel.Candels_4hour, binanceModel.MajorHighPrice4hour, binanceModel.MajorLowPrice4hour);
                }
                if (binanceModel.Candels_oneWeek != null)
                {
                    CalculateSeries("1W", binanceModel.Candels_oneWeek, binanceModel.MajorHighPrice1Week, binanceModel.MajorLowPrice1Week);
                }
                if (binanceModel.Candels_oneDay != null)
                {
                    CalculateSeries("1D", binanceModel.Candels_oneDay, binanceModel.MajorHighPrice1Day, binanceModel.MajorLowPrice1Day);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void CalculateSeries(string DictionaryKey, List<IBinanceKline> candels, List<WeightedValue> MH, List<WeightedValue> ML)
        {
            var quotes = BinanceHelper.GetQuotes(candels);
            var sma = BinanceHelper.SMA(quotes, 100);
            var startTime = sma.First().Date;
            var endTime = sma.Last().Date;
            MADic[DictionaryKey].DataSource = sma;
            MHDic[DictionaryKey].DataSource = GetMarginPoints(MH, startTime, endTime, out decimal lastMh1min);
            MLDic[DictionaryKey].DataSource = GetMarginPoints(ML, startTime, endTime, out decimal lastMl1min);
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
        private void CHB_ContinuedEstimation_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            if (CHB_ContinuedEstimation.Checked)
                i++;
            else
                i--;
        }

        private void chb_ma100_1m_CheckedChanged(object sender, EventArgs e)
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

        private void chb_mhml_1m_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                if (MADic.ContainsKey(checkBox.Text))
                {
                    if (checkBox.Checked)
                    {
                        radChartView1.Series.Add(MHDic[checkBox.Text]);
                        radChartView1.Series.Add(MLDic[checkBox.Text]);
                    }
                    else
                    {
                        radChartView1.Series.Remove(MHDic[checkBox.Text]);
                        radChartView1.Series.Remove(MLDic[checkBox.Text]);
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void radChartView1_DoubleClick(object sender, EventArgs e)
        {
            radChartView1.Zoom(1, 1);
        }

        private void MA100MHMLForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;
        }
    }
}
