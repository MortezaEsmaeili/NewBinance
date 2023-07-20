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
    public partial class strategies03Form : Telerik.WinControls.UI.RadForm
    {
        List<string> Intervals = new List<string> { KlineInterval.OneMinute.ToString(),
            KlineInterval.FiveMinutes.ToString(), KlineInterval.FifteenMinutes.ToString(),
            KlineInterval.ThirtyMinutes.ToString(), KlineInterval.OneHour.ToString(),
            KlineInterval.FourHour.ToString(), KlineInterval.OneDay.ToString()};
        
        private string coinName = string.Empty;
        private BinanceModel binanceModel;
        private KlineInterval _interval = KlineInterval.OneMinute;

        private SignalState signalState = SignalState.wait;

        public strategies03Form()
        {
            InitializeComponent();
        }
        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1 =
            new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        LinearAxis linearAxis1 = new Telerik.WinControls.UI.LinearAxis();
        LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1_MH_ML = 
            new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1_MH_ML = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2_MH_ML = new Telerik.WinControls.UI.LinearAxis();

        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1_MH_MH = 
            new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1_MH_MH = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2_MH_MH = new Telerik.WinControls.UI.LinearAxis();

        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis1_ML_ML =
            new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis1_ML_ML = new Telerik.WinControls.UI.LinearAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2_ML_ML = new Telerik.WinControls.UI.LinearAxis();

        LineSeries price = new LineSeries();
      //  BarSeries histogram = new BarSeries();
       

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

        SteplineSeries MH_ML1Min = new SteplineSeries();
        SteplineSeries MH_ML5Min = new SteplineSeries();
        SteplineSeries MH_ML15Min = new SteplineSeries();
        SteplineSeries MH_ML30Min = new SteplineSeries();
        SteplineSeries MH_ML60Min = new SteplineSeries();

        SteplineSeries MH1_MH5 = new SteplineSeries();
        SteplineSeries MH5_MH15 = new SteplineSeries();
        SteplineSeries MH5_MH30 = new SteplineSeries();
        SteplineSeries MH5_MH60 = new SteplineSeries();
        LineSeries ZeroMH1 = new LineSeries();
        LineSeries ZeroML1 = new LineSeries();

        SteplineSeries ML1_ML5 = new SteplineSeries();
        SteplineSeries ML5_ML15 = new SteplineSeries();
        SteplineSeries ML5_ML30 = new SteplineSeries();
        SteplineSeries ML5_ML60 = new SteplineSeries();

        //Esi
        SteplineSeries ML1_ML15 = new SteplineSeries();
        SteplineSeries ML1_ML30 = new SteplineSeries();
        SteplineSeries ML1_ML60 = new SteplineSeries();

        SteplineSeries MH1_MH15 = new SteplineSeries();
        SteplineSeries MH1_MH30 = new SteplineSeries();
        SteplineSeries MH1_MH60 = new SteplineSeries();
        //Esi


        private delegate void UpdateFormDelegate(BinanceModel binanceModel);

        private bool chartWasDrew = false;
        private void strategies03Form_Load(object sender, EventArgs e)
        {
            initAxis();
            InitSeries();

            initAxisMH_ML();
            initAxisMH_MH();
            initAxisML_ML();

            initSeriesMH_ML();
            InitSeriesMH_MH();
            InitSeriesML_ML();

            WireEvents();
            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;
            this.radDropDownList4.DataSource = Intervals;

            radChartView1.BackColor = Color.LightGray;

            SetCartesianGrid(this.radChartView1);
            SetTrackBall(radChartView1);
            SetTrackBall(radChartView_MH_MH);
            SetTrackBall(radChartView_MH_ML);
            SetTrackBall(radChartView_ML_ML);
            coinName = this.radDropDownList1.SelectedItem.Text;
            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
            BinanceDataCollector.Instance.CandleReadyEvent += OnCandleReadyEvent;

            Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out _interval);

            if (!string.IsNullOrWhiteSpace(coinName))
            {
                var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                if (coinInfo == null) return;
                binanceModel = coinInfo;
            }
            UpdateData(binanceModel);
        }
        
        private void OnDataReadyEvent()
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
            Action act = new Action(() => {
                if (binanceModel.PriceList.Any())
                {
                    price_LB.Text = binanceModel.CurrentPrice.ToString();
                    if(chartWasDrew==false)
                        DrawChart();
                }
            });
            if (InvokeRequired)
            {
                this.Invoke(act);
            }
            else
            {
                act();
            }
        }
        private void OnCandleReadyEvent(KlineInterval interval, string coin)
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
            try
            {
                if (binanceModel.Candels_1min == null || binanceModel.Candels_1min.Count == 0 || binanceModel.MacdResult1Min == null)
                    return;

                var startTime = binanceModel.MacdResult1Min.First().Date;
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
                if (mh1min > 0 && ml1min > 0)
                {
                    var percent = 100 * (mh1min - ml1min) / ml1min;
                    tb_1minDiff.Text = string.Format("1Min={0:00.00}%", percent);

                }
                temp_temp = Subtract_MH_ML(temp_H1, temp_L1);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH_ML1Min.DataPoints.Clear();
                    MH_ML1Min.DataPoints.AddRange(temp_temp);
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
                if (mh5min > 0 && ml5min > 0)
                {
                    var percent = 100 * (mh5min - ml5min) / ml5min;
                    tb_5minDiff.Text = string.Format("5Min={0:00.00}%", percent);
                }
                temp_temp = Subtract_MH_ML(temp_H5 , temp_L5);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH_ML5Min.DataPoints.Clear();
                    MH_ML5Min.DataPoints.AddRange(temp_temp);
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
                if (mh15min > 0 && ml15min > 0)
                {
                    var percent = 100 * (mh15min - ml15min) / ml15min;
                    tb_15minDiff.Text = string.Format("15Min={0:00.00}%", percent);
                }
                temp_temp = Subtract_MH_ML(temp_H15, temp_L15);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH_ML15Min.DataPoints.Clear();
                    MH_ML15Min.DataPoints.AddRange(temp_temp);
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
                if (mh30min > 0 && ml30min > 0)
                {
                    var percent = 100 * (mh30min - ml30min) / ml30min;
                    tb_30minDiff.Text = string.Format("30Min={0:00.00}%", percent);
                }
                temp_temp = Subtract_MH_ML(temp_H30, temp_L30);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH_ML30Min.DataPoints.Clear();
                    MH_ML30Min.DataPoints.AddRange(temp_temp);
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
                if (mh60min > 0 && ml60min > 0)
                {
                    var percent = 100 * (mh60min - ml60min) / ml60min;
                    tb_60minDiff.Text = string.Format("60Min={0:00.00}%", percent);

                }
                temp_temp = Subtract_MH_ML(temp_H60, temp_L60);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH_ML60Min.DataPoints.Clear();
                    MH_ML60Min.DataPoints.AddRange(temp_temp);
                }
                
                temp_temp = Subtract_MH_ML(temp_H5, temp_H1, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH1_MH5.DataPoints.Clear();
                    MH1_MH5.DataPoints.AddRange(temp_temp); 
                    ZeroMH1.DataPoints.Clear();
                    ZeroMH1.DataPoints.Add(new CategoricalDataPoint(0, temp_temp.First().Category));
                    ZeroMH1.DataPoints.Add(new CategoricalDataPoint(0, temp_temp.Last().Category));
                }
                temp_temp = Subtract_MH_ML(temp_H5, temp_H15, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH5_MH15.DataPoints.Clear();
                    MH5_MH15.DataPoints.AddRange(temp_temp);
                }
                temp_temp = Subtract_MH_ML(temp_H5, temp_H30, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH5_MH30.DataPoints.Clear();
                    MH5_MH30.DataPoints.AddRange(temp_temp);
                }
                temp_temp = Subtract_MH_ML(temp_H5, temp_H60, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH5_MH60.DataPoints.Clear();
                    MH5_MH60.DataPoints.AddRange(temp_temp);
                }

                //Esi
                temp_temp = Subtract_MH_ML(temp_H15, temp_H1, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH1_MH15.DataPoints.Clear();
                    MH1_MH15.DataPoints.AddRange(temp_temp);
                }
                temp_temp = Subtract_MH_ML(temp_H30, temp_H1, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH1_MH30.DataPoints.Clear();
                    MH1_MH30.DataPoints.AddRange(temp_temp);
                }
                temp_temp = Subtract_MH_ML(temp_H60, temp_H1, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    MH1_MH60.DataPoints.Clear();
                    MH1_MH60.DataPoints.AddRange(temp_temp);
                }

                temp_temp = Subtract_MH_ML(temp_L1, temp_L5, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    ML1_ML5.DataPoints.Clear();
                    ML1_ML5.DataPoints.AddRange(temp_temp);
                    ZeroML1.DataPoints.Clear();
                    ZeroML1.DataPoints.Add(new CategoricalDataPoint(0, temp_temp.First().Category));
                    ZeroML1.DataPoints.Add(new CategoricalDataPoint(0, temp_temp.Last().Category));

                }
                temp_temp = Subtract_MH_ML(temp_L5, temp_L15, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    ML5_ML15.DataPoints.Clear();
                    ML5_ML15.DataPoints.AddRange(temp_temp);
                }
                temp_temp = Subtract_MH_ML(temp_L5, temp_L30, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    ML5_ML30.DataPoints.Clear();
                    ML5_ML30.DataPoints.AddRange(temp_temp);
                }
                temp_temp = Subtract_MH_ML(temp_L5, temp_L60, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    ML5_ML60.DataPoints.Clear();
                    ML5_ML60.DataPoints.AddRange(temp_temp);
                }

                //Esi
                temp_temp = Subtract_MH_ML(temp_L1, temp_L15, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    ML1_ML15.DataPoints.Clear();
                    ML1_ML15.DataPoints.AddRange(temp_temp);
                }
                temp_temp = Subtract_MH_ML(temp_L1, temp_L30, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    ML1_ML30.DataPoints.Clear();
                    ML1_ML30.DataPoints.AddRange(temp_temp);
                }
                temp_temp = Subtract_MH_ML(temp_L1, temp_L60, true);
                if (temp_temp != null && temp_temp.Any())
                {
                    ML1_ML60.DataPoints.Clear();
                    ML1_ML60.DataPoints.AddRange(temp_temp);
                }
                //Esi
                int telorance = 0;
                int.TryParse(TB_Telorance.Text, out telorance);

                int iML1_ML5 = Convert.ToInt32(ML1_ML5.DataPoints.First().Label);
                int iML1_ML15 = Convert.ToInt32(ML1_ML15.DataPoints.First().Label);
                int iML1_ML30 = Convert.ToInt32(ML1_ML30.DataPoints.First().Label);
                int iML1_ML60 = Convert.ToInt32(ML1_ML60.DataPoints.First().Label);
                if(iML1_ML5 < telorance &&
                    iML1_ML15 < telorance &&
                    iML1_ML30 < telorance &&
                    iML1_ML60 < telorance)
                    GenerateAlarm("ML1-MLX Alarm");

                int iMH1_MH5 = Convert.ToInt32(MH1_MH5.DataPoints.First().Label);
                int iMH1_MH15 = Convert.ToInt32(MH1_MH15.DataPoints.First().Label);
                int iMH1_MH30 = Convert.ToInt32(MH1_MH30.DataPoints.First().Label);
                int iMH1_MH60 = Convert.ToInt32(MH1_MH60.DataPoints.First().Label);
                if (iMH1_MH5 < telorance &&
                    iMH1_MH15 < telorance &&
                    iMH1_MH30 < telorance &&
                    iMH1_MH60 < telorance)
                    GenerateAlarm("MH1-MHX Alarm");


                temp_temp = new ConcurrentBag<CategoricalDataPoint>();

               
                binanceModel.Candels_1min.Where(s => s.CloseTime >= binanceModel.MacdResult1Min.First().Date).AsParallel().ForAll(item =>
                    { temp_temp.Add(new CategoricalDataPoint((double)item.ClosePrice, item.CloseTime)); });
                if (temp_temp.Any())
                {
                    price.DataPoints.Clear();
                    price.DataPoints.AddRange(temp_temp);
                }
                chartWasDrew = true;
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        private void GenerateAlarm(string strAlarmText)
        {
            String alarmText = strAlarmText + "  " + DateTime.Now.ToLongTimeString();
            if (listBox1.Items.Count > 0)
            {
                if (alarmText == listBox1.Items[listBox1.Items.Count - 1].ToString())
                    return;
            }
            SystemSounds.Beep.Play();
            listBox1.Items.Add(alarmText);
        }

        private ConcurrentBag<CategoricalDataPoint> Subtract_MH_ML(ConcurrentBag<CategoricalDataPoint> temp1,
            ConcurrentBag<CategoricalDataPoint> temp2, bool H_H = false)
        {
            List<DateTime> timeArr = new List<DateTime>();
            timeArr.AddRange(temp1.Select(item =>(DateTime)( item.Category)));
            timeArr.AddRange(temp2.Select(item => (DateTime)(item.Category)));
            timeArr.Sort();

            var test1 = temp1.ToList().OrderBy(item => (DateTime)item.Category).ToList();
            var test2 = temp2.ToList().OrderBy(item => (DateTime)item.Category).ToList();

            var temp = new ConcurrentBag<CategoricalDataPoint>();

            foreach (DateTime dt in timeArr)
            {
                double? value = 0;
                var cat1 = test1.Where(t => (DateTime)t.Category <= dt).LastOrDefault();
                var cat2 = test2.Where(t => (DateTime)t.Category <= dt).LastOrDefault();
                if (cat1 == null || cat2 == null)
                    value = 0;
               /* else if (cat2.Value != 0 && !H_H)
                    value = (cat1.Value - cat2.Value) / cat2.Value;*/
                else
                    value = (cat1.Value - cat2.Value);

                Console.WriteLine(value);
                temp.Add(new CategoricalDataPoint((double)value, (DateTime)dt));
            }

            return temp;
        }

        private ConcurrentBag<CategoricalDataPoint> GetMarginPoints(List<WeightedValue>valueList, DateTime startTime, 
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
                    if(temp.Count==0)
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
                        if(firstPoint!=null)
                            temp.Add(new CategoricalDataPoint((double)firstPoint.Value, startTime));
                    }

                    temp.Add(new CategoricalDataPoint((double)lastvalue, endTime));
                }
            }
            catch(Exception ex)
            { }
            return temp;
           
        }
        private void initAxis()
        {
            dateTimeCategoricalAxis1.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis1.IsPrimary = true;
            dateTimeCategoricalAxis1.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis1.LabelFormat = "{0:HH:mm:ss}";
            dateTimeCategoricalAxis1.MajorTickInterval = 50;

            
            linearAxis2.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis2.IsPrimary = true;
            linearAxis2.TickOrigin = null;

            this.radChartView1.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1,
             linearAxis2});

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
            price.BackColor = Color.Black;
            price.BorderWidth = 2;

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

           
            radChartView1.Series.Add(price);

            radChartView1.Series.Add(MajorHigh1Min);
            radChartView1.Series.Add(MajorLow1Min);

            radChartView1.Series.Add(MajorHigh5Min);
            radChartView1.Series.Add(MajorLow5Min);

            radChartView1.Series.Add(MajorHigh15Min);
            radChartView1.Series.Add(MajorLow15Min);

            radChartView1.Series.Add(MajorHigh30Min);
            radChartView1.Series.Add(MajorLow30Min);

            radChartView1.Series.Add(MajorHigh60Min);
            radChartView1.Series.Add(MajorLow60Min);
        }


        private void initAxisMH_ML()
        {
            dateTimeCategoricalAxis1_MH_ML.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis1_MH_ML.IsPrimary = true;
            dateTimeCategoricalAxis1_MH_ML.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis1_MH_ML.LabelFormat = "{0:HH:mm:ss}";
            dateTimeCategoricalAxis1_MH_ML.MajorTickInterval = 10;

            linearAxis2_MH_ML.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2_MH_ML.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis2_MH_ML.IsPrimary = true;
            linearAxis2_MH_ML.TickOrigin = null;

            this.radChartView_MH_ML.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1_MH_ML,
             linearAxis2_MH_ML});

            LassoZoomController lassoZoomController_MH_ML = new LassoZoomController();
            radChartView_MH_ML.Controllers.Add(lassoZoomController_MH_ML);
            radChartView_MH_ML.BackColor = Color.FromArgb(200,200,240);
            radChartView_MH_ML.ShowLegend = true;
        }
        private void initAxisMH_MH()
        {
            dateTimeCategoricalAxis1_MH_MH.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis1_MH_MH.IsPrimary = true;
            dateTimeCategoricalAxis1_MH_MH.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis1_MH_MH.LabelFormat = "{0:HH:mm:ss}";
            dateTimeCategoricalAxis1_MH_MH.MajorTickInterval = 5;

            linearAxis2_MH_MH.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2_MH_MH.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis2_MH_MH.IsPrimary = true;
            linearAxis2_MH_MH.TickOrigin = null;

            this.radChartView_MH_MH.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1_MH_MH,
             linearAxis2_MH_MH});

            LassoZoomController lassoZoomController_MH_MH = new LassoZoomController();
            radChartView_MH_MH.Controllers.Add(lassoZoomController_MH_MH);
            radChartView_MH_MH.BackColor = Color.FromArgb(240, 200, 200);
            radChartView_MH_MH.ShowLegend = true;
        }

        private void initAxisML_ML()
        {
            dateTimeCategoricalAxis1_ML_ML.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis1_ML_ML.IsPrimary = true;
            dateTimeCategoricalAxis1_ML_ML.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis1_ML_ML.LabelFormat = "{0:HH:mm:ss}";
            dateTimeCategoricalAxis1_ML_ML.MajorTickInterval = 5;

            linearAxis2_ML_ML.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2_ML_ML.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Left;
            linearAxis2_ML_ML.IsPrimary = true;
            linearAxis2_ML_ML.TickOrigin = null;

            this.radChartView_ML_ML.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis1_ML_ML,
             linearAxis2_ML_ML});

            LassoZoomController lassoZoomController_MH_MH = new LassoZoomController();
            radChartView_ML_ML.Controllers.Add(lassoZoomController_MH_MH);
            radChartView_ML_ML.BackColor = Color.FromArgb(200, 240, 200);
            radChartView_ML_ML.ShowLegend = true;
        }

        private void initSeriesMH_ML()
        {
            MH_ML1Min.HorizontalAxis = dateTimeCategoricalAxis1_MH_ML;
            MH_ML1Min.VerticalAxis = linearAxis2_MH_ML;
            MH_ML1Min.LegendTitle = "MH-ML 1m";
            MH_ML1Min.BorderColor = Color.Green;
            MH_ML1Min.BackColor = Color.Green;
            MH_ML1Min.BorderWidth = 2;

            MH_ML5Min.HorizontalAxis = dateTimeCategoricalAxis1_MH_ML;
            MH_ML5Min.VerticalAxis = linearAxis2_MH_ML;
            MH_ML5Min.LegendTitle = "MH-ML 5m";
            MH_ML5Min.BorderColor = Color.Blue;
            MH_ML5Min.BackColor = Color.Blue;
            MH_ML5Min.BorderWidth = 2;

            MH_ML15Min.HorizontalAxis = dateTimeCategoricalAxis1_MH_ML;
            MH_ML15Min.VerticalAxis = linearAxis2_MH_ML;
            MH_ML15Min.LegendTitle = "MH-ML 15m";
            MH_ML15Min.BorderColor = Color.Orange;
            MH_ML15Min.BackColor = Color.Orange;
            MH_ML15Min.BorderWidth = 2;

            MH_ML30Min.HorizontalAxis = dateTimeCategoricalAxis1_MH_ML;
            MH_ML30Min.VerticalAxis = linearAxis2_MH_ML;
            MH_ML30Min.LegendTitle = "MH-ML 30m";
            MH_ML30Min.BorderColor = Color.Red;
            MH_ML30Min.BackColor = Color.Red;
            MH_ML30Min.BorderWidth = 2;

            MH_ML60Min.HorizontalAxis = dateTimeCategoricalAxis1_MH_ML;
            MH_ML60Min.VerticalAxis = linearAxis2_MH_ML;
            MH_ML60Min.LegendTitle = "MH-ML 60m";
            MH_ML60Min.BorderColor = Color.Purple;
            MH_ML60Min.BackColor = Color.Purple;
            MH_ML60Min.BorderWidth = 2;


            radChartView_MH_ML.Series.Add(MH_ML1Min);
            radChartView_MH_ML.Series.Add(MH_ML5Min);
            radChartView_MH_ML.Series.Add(MH_ML15Min);
            radChartView_MH_ML.Series.Add(MH_ML30Min);
            radChartView_MH_ML.Series.Add(MH_ML60Min);
        }
        private void InitSeriesMH_MH()
        {
            MH1_MH5.HorizontalAxis = dateTimeCategoricalAxis1_MH_MH;
            MH1_MH5.VerticalAxis = linearAxis2_MH_MH;
            MH1_MH5.LegendTitle = "MH1-MH5";
            MH1_MH5.BorderColor = Color.Green;
            MH1_MH5.BackColor = Color.Green;
            MH1_MH5.BorderWidth = 2;

            ZeroMH1.HorizontalAxis = dateTimeCategoricalAxis1_MH_MH;
            ZeroMH1.VerticalAxis = linearAxis2_MH_MH;
            ZeroMH1.LegendTitle = "Zero";
            ZeroMH1.BorderColor = Color.Black;
            ZeroMH1.BackColor = Color.Black;
            ZeroMH1.BorderWidth = 1;

            /* Esi
            MH5_MH15.HorizontalAxis = dateTimeCategoricalAxis1_MH_MH;
            MH5_MH15.VerticalAxis = linearAxis2_MH_MH;
            MH5_MH15.LegendTitle = "MH5-MH15";
            MH5_MH15.BorderColor = Color.Blue;
            MH5_MH15.BackColor = Color.Blue;
            MH5_MH15.BorderWidth = 2;

            MH5_MH30.HorizontalAxis = dateTimeCategoricalAxis1_MH_MH;
            MH5_MH30.VerticalAxis = linearAxis2_MH_MH;
            MH5_MH30.LegendTitle = "MH5-MH30";
            MH5_MH30.BorderColor = Color.Orange;
            MH5_MH30.BackColor = Color.Orange;
            MH5_MH30.BorderWidth = 2;

            MH5_MH60.HorizontalAxis = dateTimeCategoricalAxis1_MH_MH;
            MH5_MH60.VerticalAxis = linearAxis2_MH_MH;
            MH5_MH60.LegendTitle = "MH5-MH60";
            MH5_MH60.BorderColor = Color.Red;
            MH5_MH60.BackColor = Color.Red;
            MH5_MH60.BorderWidth = 2;*/

            //Esi
            MH1_MH15.HorizontalAxis = dateTimeCategoricalAxis1_MH_MH;
            MH1_MH15.VerticalAxis = linearAxis2_MH_MH;
            MH1_MH15.LegendTitle = "MH1-MH15";
            MH1_MH15.BorderColor = Color.Blue;
            MH1_MH15.BackColor = Color.Blue;
            MH1_MH15.BorderWidth = 2;

            MH1_MH30.HorizontalAxis = dateTimeCategoricalAxis1_MH_MH;
            MH1_MH30.VerticalAxis = linearAxis2_MH_MH;
            MH1_MH30.LegendTitle = "MH1-MH30";
            MH1_MH30.BorderColor = Color.Orange;
            MH1_MH30.BackColor = Color.Orange;
            MH1_MH30.BorderWidth = 2;

            MH1_MH60.HorizontalAxis = dateTimeCategoricalAxis1_MH_MH;
            MH1_MH60.VerticalAxis = linearAxis2_MH_MH;
            MH1_MH60.LegendTitle = "MH1-MH60";
            MH1_MH60.BorderColor = Color.Red;
            MH1_MH60.BackColor = Color.Red;
            MH1_MH60.BorderWidth = 2;

            radChartView_MH_MH.Series.Add(MH1_MH5);
            radChartView_MH_MH.Series.Add(MH1_MH15);
            radChartView_MH_MH.Series.Add(MH1_MH30);
            radChartView_MH_MH.Series.Add(MH1_MH60);
            radChartView_MH_MH.Series.Add(ZeroMH1);

        }
        private void InitSeriesML_ML()
        {
            ML1_ML5.HorizontalAxis = dateTimeCategoricalAxis1_ML_ML;
            ML1_ML5.VerticalAxis = linearAxis2_ML_ML;
            ML1_ML5.LegendTitle = "ML1-ML5";
            ML1_ML5.BorderColor = Color.Green;
            ML1_ML5.BackColor = Color.Green;
            ML1_ML5.BorderWidth = 2; 
            
            ZeroML1.HorizontalAxis = dateTimeCategoricalAxis1_ML_ML;
            ZeroML1.VerticalAxis = linearAxis2_ML_ML;
            ZeroML1.LegendTitle = "Zero";
            ZeroML1.BorderColor = Color.Black;
            ZeroML1.BackColor = Color.Black;
            ZeroML1.BorderWidth = 1;

            /*
            ML5_ML15.HorizontalAxis = dateTimeCategoricalAxis1_ML_ML;
            ML5_ML15.VerticalAxis = linearAxis2_ML_ML;
            ML5_ML15.LegendTitle = "ML5-ML15";
            ML5_ML15.BorderColor = Color.Blue;
            ML5_ML15.BackColor = Color.Blue;
            ML5_ML15.BorderWidth = 2;

            ML5_ML30.HorizontalAxis = dateTimeCategoricalAxis1_ML_ML;
            ML5_ML30.VerticalAxis = linearAxis2_ML_ML;
            ML5_ML30.LegendTitle = "ML5-ML30";
            ML5_ML30.BorderColor = Color.Orange;
            ML5_ML30.BackColor = Color.Orange;
            ML5_ML30.BorderWidth = 2;

            ML5_ML60.HorizontalAxis = dateTimeCategoricalAxis1_ML_ML;
            ML5_ML60.VerticalAxis = linearAxis2_ML_ML;
            ML5_ML60.LegendTitle = "ML5-ML60";
            ML5_ML60.BorderColor = Color.Red;
            ML5_ML60.BackColor = Color.Red;
            ML5_ML60.BorderWidth = 2;
            */
            //Esi

            ML1_ML15.HorizontalAxis = dateTimeCategoricalAxis1_ML_ML;
            ML1_ML15.VerticalAxis = linearAxis2_ML_ML;
            ML1_ML15.LegendTitle = "ML1-ML15";
            ML1_ML15.BorderColor = Color.Blue;
            ML1_ML15.BackColor = Color.Blue;
            ML1_ML15.BorderWidth = 2;

            ML1_ML30.HorizontalAxis = dateTimeCategoricalAxis1_ML_ML;
            ML1_ML30.VerticalAxis = linearAxis2_ML_ML;
            ML1_ML30.LegendTitle = "ML1-ML30";
            ML1_ML30.BorderColor = Color.Orange;
            ML1_ML30.BackColor = Color.Orange;
            ML1_ML30.BorderWidth = 2;

            ML1_ML60.HorizontalAxis = dateTimeCategoricalAxis1_ML_ML;
            ML1_ML60.VerticalAxis = linearAxis2_ML_ML;
            ML1_ML60.LegendTitle = "ML1-ML60";
            ML1_ML60.BorderColor = Color.Red;
            ML1_ML60.BackColor = Color.Red;
            ML1_ML60.BorderWidth = 2;


            radChartView_ML_ML.Series.Add(ML1_ML5);
            radChartView_ML_ML.Series.Add(ML1_ML15);
            radChartView_ML_ML.Series.Add(ML1_ML30);
            radChartView_ML_ML.Series.Add(ML1_ML60);
            radChartView_ML_ML.Series.Add(ZeroML1);
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

        private void SetTrackBall(RadChartView radChartView)
        {
            ChartTrackballController trackBallElement = new ChartTrackballController();

            trackBallElement.Element.TextAlignment = ContentAlignment.MiddleLeft;
            trackBallElement.Element.BorderColor = Color.Black;
            trackBallElement.Element.BackColor = Color.White;
            trackBallElement.Element.BorderGradientStyle = Telerik.WinControls.GradientStyles.Solid;
            trackBallElement.Element.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
            trackBallElement.Element.Padding = new Padding(3, 0, 3, 3);

            radChartView.Controllers.Add(trackBallElement);
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
                setToInitialState();
            }
        }

        private void setToInitialState()
        {
            
            if(this.radDropDownList4.SelectedItem ==null || Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out _interval)==false)
                _interval = KlineInterval.OneMinute;
            BTN_Buy.BackColor = Color.Gray;
            BTN_Sell.BackColor = Color.Gray;
        }

        void radDropDownList4_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (this.radDropDownList1.SelectedItem != null)
            {
                coinName = this.radDropDownList1.SelectedItem.Text;
                Enum.TryParse(this.radDropDownList4.SelectedItem.Text, out _interval);
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
            listBox1.Items.Clear();
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

        private void radChartView_MH_ML_DoubleClick(object sender, EventArgs e)
        {
            radChartView_MH_ML.Zoom(1, 1);
        }

        private void radChartView_MH_MH_DoubleClick(object sender, EventArgs e)
        {
            radChartView_MH_MH.Zoom(1, 1);
        }

        private void radChartView_ML_ML_DoubleClick(object sender, EventArgs e)
        {
            radChartView_ML_ML.Zoom(1, 1);
        }

        private void strategies03Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;
            BinanceDataCollector.Instance.CandleReadyEvent -= OnCandleReadyEvent;

        }
    }
}
