using Binance.BotState;
using Binance.Dal;
using Binance.Dal.Model;
using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.Business;
using BinanceApp.DataModel;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using FastReport.DataVisualization.Charting;
using FastReport.DevComponents.DotNetBar.Controls;
using Skender.Stock.Indicators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;


namespace BinanceApp
{
    public partial class TradeBotForm : Telerik.WinControls.UI.RadForm
    {
        public TradeBotForm()
        {
            InitializeComponent();
        }

        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis2 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

        CandlestickSeries candleSery = new CandlestickSeries();
        SteplineSeries buyUpperLine = new SteplineSeries();
        SteplineSeries buyLowerLine = new SteplineSeries();
        SteplineSeries buyProfitLine = new SteplineSeries();
        SteplineSeries buyStopLossLine = new SteplineSeries();
        SteplineSeries sellUpperLine = new SteplineSeries();
        SteplineSeries sellLowerLine = new SteplineSeries();
        SteplineSeries sellProfitLine = new SteplineSeries();
        SteplineSeries sellStopLossLine = new SteplineSeries();


        private string coinName = string.Empty;
        private bool isNewRange = true;
        long iDRow = 0;

        List<Account> tradeAccountList = new List<Account>();


        private BinanceModel binance;

        private List<IBinanceKline> candels;

        private decimal buyUpperPrice = 0;
        private decimal buyLowerPrice = 0;
        private decimal buytakeProfit = 0;
        private decimal buyStopLoss = 0;
        private decimal buyAvailable = 0;

        private decimal sellUpperPrice = 0;
        private decimal sellLowerPrice = 0;
        private decimal selltakeProfit = 0;
        private decimal sellStopLoss = 0;
        private decimal sellAvailable = 0;

        private void Form1_Load(object sender, EventArgs e)
        {

            initAxis();
            InitSeries();
            InitializeData();
            WireEvents();

            this.radDropDownList3.DataSource = BinanceDataCollector.Instance.CoinNames;
            this.radDropDownList3.SelectedIndex = 0;
            if (this.radDropDownList3.SelectedItem != null)
            {
                coinName = this.radDropDownList3.SelectedItem.Text;
                LoadFromDB(coinName);
            }
            radChartView2.BackColor = Color.Gray;

            SetCartesianGrid(this.radChartView2);
            SetTrackBall();

            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
        }

        private void LoadFromDB(string coinName)
        {
            using (var context = new TradeContext())
            {
                TradeBotRange tradeBotRange = context.TradeBotRanges.FirstOrDefault(r => r.CoinName == coinName);
                if(tradeBotRange == null)
                {
                    isNewRange = true;
                    InitializeData();
                    return;
                }
                isNewRange = false;
                UpdateFormDataFromDB(tradeBotRange);
            }
        }

        private void UpdateFormDataFromDB(TradeBotRange tradeBotRange)
        {
            tx_buy_up_price.Text = tradeBotRange.UpBuyPrice.ToString();
            tx_buy_low_price.Text = tradeBotRange.LowBuyPrice.ToString();
            tx_buy_profit_limit.Text = tradeBotRange.BuyTakeProfitPrice.ToString();
            tx_buy_stop_loss.Text = tradeBotRange.BuyStopLossPrice.ToString();
            tx_buy_available.Text = tradeBotRange.BuyAvailable.ToString();
            //cb_buy_isActive.Checked = tradeBotRange.IsActiveBuy;

            tx_sell_up_price.Text = tradeBotRange.UpSellPrice.ToString();
            tx_sell_low_price.Text = tradeBotRange.LowSellPrice.ToString();
            tx_sell_profit_limit.Text = tradeBotRange.SellTakeProfitPrice.ToString();
            tx_sell_stop_loss.Text = tradeBotRange.SellStopLossPrice.ToString();
            tx_sell_available.Text = tradeBotRange.SellAvailable.ToString();
            //cb_sell_isActive.Checked = tradeBotRange.IsActiveSell;

            buyUpperPrice = tradeBotRange.UpBuyPrice;
            buyLowerPrice = tradeBotRange.LowBuyPrice;
            buytakeProfit = tradeBotRange.BuyTakeProfitPrice;
            buyStopLoss = tradeBotRange.BuyStopLossPrice;
            buyAvailable = tradeBotRange.BuyAvailable;

            sellUpperPrice = tradeBotRange.UpSellPrice;
            sellLowerPrice = tradeBotRange.LowSellPrice;
            selltakeProfit = tradeBotRange.SellTakeProfitPrice;
            sellStopLoss = tradeBotRange.SellStopLossPrice;
            sellAvailable = tradeBotRange.SellAvailable;

            iDRow = tradeBotRange.Id;
        }

        private void initLineVariable()
        {
            buyUpperPrice = 0;
            buyLowerPrice = 0;
            buytakeProfit = 0;
            buyStopLoss = 0;
            buyAvailable = 0;

            sellUpperPrice = 0;
            sellLowerPrice = 0;
            selltakeProfit = 0;
            sellStopLoss = 0;
            sellAvailable = 0;
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
                UpdateStockSeries();
            }
            catch { }
        }
       
        private void InitializeData()
        {
            tx_buy_up_price.Text = "0";
            tx_buy_low_price.Text = "0";
            tx_buy_profit_limit.Text = "0";
            tx_buy_stop_loss.Text = "0";
            tx_buy_available.Text = "0";

            tx_sell_up_price.Text = "0";
            tx_sell_low_price.Text = "0";
            tx_sell_profit_limit.Text = "0";
            tx_sell_stop_loss.Text = "0";
            tx_sell_available.Text = "0";
        }

        private void initAxis()
        {
            LassoZoomController lassoZoomController = new LassoZoomController();

            dateTimeCategoricalAxis2.DateTimeComponent = Telerik.Charting.DateTimeComponent.Ticks;
            dateTimeCategoricalAxis2.IsPrimary = true;
            dateTimeCategoricalAxis2.LabelFitMode = Telerik.Charting.AxisLabelFitMode.MultiLine;
            dateTimeCategoricalAxis2.LabelFormat = "{0:M/d HH:mm:ss}";
            dateTimeCategoricalAxis2.MajorTickInterval = 15;

            linearAxis2.AxisType = Telerik.Charting.AxisType.Second;
            linearAxis2.HorizontalLocation = Telerik.Charting.AxisHorizontalLocation.Right;
            linearAxis2.IsPrimary = true;
            linearAxis2.MajorStep = 5D;
            linearAxis2.Maximum = 80D;
            linearAxis2.Minimum = 50D;
            linearAxis2.TickOrigin = null;

            radChartView2.Area.View.Palette = KnownPalette.Material;
            this.radChartView2.Axes.AddRange(new Telerik.WinControls.UI.Axis[] {
            dateTimeCategoricalAxis2,
            linearAxis2});

            LassoZoomController lassoZoomController1 = new LassoZoomController();
            radChartView2.Controllers.Add(lassoZoomController1);

            radChartView2.ShowGrid = false;
            radChartView2.ShowLegend = true;
        }
        private void SetCartesianGrid(RadChartView chart)
        {
            CartesianArea area = chart.GetArea<CartesianArea>();
           // area.ShowGrid = true;

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

        private void radDropDownList3_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (this.radDropDownList3.SelectedItem != null)
            {
                coinName = this.radDropDownList3.SelectedItem.Text;
                CorrectButtonState();
                initLineVariable();
                LoadFromDB(coinName);
                var coinInfo = BinanceDataCollector.Instance.GetBinance(coinName);
                if (coinInfo == null) return;
                binance = coinInfo;
                UpdateStockSeries();
            }
        }

        private void CorrectButtonState()
        {
            
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

            buyUpperLine.HorizontalAxis = dateTimeCategoricalAxis2;
            buyUpperLine.VerticalAxis = linearAxis2;
            buyUpperLine.LegendTitle = "Buy Upper Line";
            buyUpperLine.BorderColor = Color.Black;
            buyUpperLine.BackColor = Color.Black;
            buyUpperLine.BorderWidth = 3;

            buyLowerLine.HorizontalAxis = dateTimeCategoricalAxis2;
            buyLowerLine.VerticalAxis = linearAxis2;
            buyLowerLine.LegendTitle = "Buy Lower Line";
            buyLowerLine.BorderColor = Color.Blue;
            buyLowerLine.BackColor = Color.Blue;
            buyLowerLine.BorderWidth = 3;

            buyProfitLine.HorizontalAxis = dateTimeCategoricalAxis2;
            buyProfitLine.VerticalAxis = linearAxis2;
            buyProfitLine.LegendTitle = "Buy Profit Line";
            buyProfitLine.BorderColor = Color.Green;
            buyProfitLine.BackColor = Color.Green;
            buyProfitLine.BorderWidth = 3;

            buyStopLossLine.HorizontalAxis = dateTimeCategoricalAxis2;
            buyStopLossLine.VerticalAxis = linearAxis2;
            buyStopLossLine.LegendTitle = "Buy Loss Line";
            buyStopLossLine.BorderColor = Color.Red;
            buyStopLossLine.BackColor = Color.Red;
            buyStopLossLine.BorderWidth = 3;

            sellUpperLine.HorizontalAxis = dateTimeCategoricalAxis2;
            sellUpperLine.VerticalAxis = linearAxis2;
            sellUpperLine.LegendTitle = "Sell Upper Line";
            sellUpperLine.BorderColor = Color.Black;
            sellUpperLine.BackColor = Color.Black;
            sellUpperLine.BorderWidth = 3;

            sellLowerLine.HorizontalAxis = dateTimeCategoricalAxis2;
            sellLowerLine.VerticalAxis = linearAxis2;
            sellLowerLine.LegendTitle = "Sell Lower Line";
            sellLowerLine.BorderColor = Color.Blue;
            sellLowerLine.BackColor = Color.Blue;
            sellLowerLine.BorderWidth = 3;

            sellProfitLine.HorizontalAxis = dateTimeCategoricalAxis2;
            sellProfitLine.VerticalAxis = linearAxis2;
            sellProfitLine.LegendTitle = "Sell Profit Line";
            sellProfitLine.BorderColor = Color.Green;
            sellProfitLine.BackColor = Color.Green;
            sellProfitLine.BorderWidth = 3;

            sellStopLossLine.HorizontalAxis = dateTimeCategoricalAxis2;
            sellStopLossLine.VerticalAxis = linearAxis2;
            sellStopLossLine.LegendTitle = "Sell Loss Line";
            sellStopLossLine.BorderColor = Color.Red;
            sellStopLossLine.BackColor = Color.Red;
            sellStopLossLine.BorderWidth = 3;
        }
        private void UpdateStockSeries()
        {
            try
            {
                if (binance == null ) return;

                UpdatePricessinTrade(binance);

                this.radChartView2.Series.Clear();
                if (binance.Tbqv_15min.Any() == false)
                    binance = BinanceDataCollector.Instance.GetBinance(coinName);
                candels = binance.Candels_4hour.Skip(binance.Candels_4hour.Count() - 150).Take(150).ToList();

                if (candels == null || candels.Any() == false) return;
                candleSery.DataSource = candels;

                var max = candels.Max(v => v.HighPrice);
                var min = candels.Min(v => v.LowPrice);

                if (sellStopLoss>0)
                {
                    if(max < sellStopLoss) max = sellStopLoss + sellStopLoss / 15;
                }
                if (buyStopLoss > 0)
                {
                    if (min > buyStopLoss) min = buyStopLoss - buyStopLoss/15;
                }

                if (max-min<3)
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
                var time1 = candels[0].OpenTime;
                var time2 = candels.Last().CloseTime;
                if(cb_show_chart_sell.Checked)
                {
                    DrawLossLine(time1,time2);
                }
                if(cb_show_chart_buy.Checked)
                {
                    DrawBuyLine(time1,time2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UpdatePricessinTrade(BinanceModel binance)
        {
            foreach (var account in tradeAccountList)
            {
                var cointInfo = BinanceDataCollector.Instance.GetBinance(account.CoinName);
                account.SetNewPrice(cointInfo.CurrentPrice);
                if(account.State == TradeState.PositionClosed)
                {
                    StopTrading(account.CoinName);
                }
            }
        }

        private void DrawBuyLine(DateTime time1, DateTime time2)
        {
            buyStopLossLine.DataPoints.Clear();
            var temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)buyStopLoss, time1));
            temp.Add(new CategoricalDataPoint((double)buyStopLoss, time2));
            buyStopLossLine.DataPoints.AddRange(temp);
            this.radChartView2.Series.Add(buyStopLossLine);

            buyUpperLine.DataPoints.Clear();
            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)buyUpperPrice, time1));
            temp.Add(new CategoricalDataPoint((double)buyUpperPrice, time2));
            buyUpperLine.DataPoints.AddRange(temp);
            this.radChartView2.Series.Add(buyUpperLine);

            buyLowerLine.DataPoints.Clear();
            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)buyLowerPrice, time1));
            temp.Add(new CategoricalDataPoint((double)buyLowerPrice, time2));
            buyLowerLine.DataPoints.AddRange(temp);
            this.radChartView2.Series.Add(buyLowerLine);

            buyProfitLine.DataPoints.Clear();
            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)buytakeProfit, time1));
            temp.Add(new CategoricalDataPoint((double)buytakeProfit, time2));
            buyProfitLine.DataPoints.AddRange(temp);
            this.radChartView2.Series.Add(buyProfitLine);
        }

        private void DrawLossLine(DateTime time1, DateTime time2)
        {
            sellStopLossLine.DataPoints.Clear();
            var temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)sellStopLoss, time1));
            temp.Add(new CategoricalDataPoint((double)sellStopLoss, time2));
            sellStopLossLine.DataPoints.AddRange(temp);
            this.radChartView2.Series.Add(sellStopLossLine);

            sellUpperLine.DataPoints.Clear();
            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)sellUpperPrice, time1));
            temp.Add(new CategoricalDataPoint((double)sellUpperPrice, time2));
            sellUpperLine.DataPoints.AddRange(temp);
            this.radChartView2.Series.Add(sellUpperLine);

            sellLowerLine.DataPoints.Clear();
            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)sellLowerPrice, time1));
            temp.Add(new CategoricalDataPoint((double)sellLowerPrice, time2));
            sellLowerLine.DataPoints.AddRange(temp);
            this.radChartView2.Series.Add(sellLowerLine);

            sellProfitLine.DataPoints.Clear();
            temp = new ConcurrentBag<CategoricalDataPoint>();
            temp.Add(new CategoricalDataPoint((double)selltakeProfit, time1));
            temp.Add(new CategoricalDataPoint((double)selltakeProfit, time2));
            sellProfitLine.DataPoints.AddRange(temp);
            this.radChartView2.Series.Add(sellProfitLine);
        }

        protected  void WireEvents()
        {
            this.radDropDownList3.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList3_SelectedIndexChanged);
        }

        private void radChartView2_DoubleClick(object sender, EventArgs e)
        {
            this.radChartView2.Zoom(1, 1);

        }

        private void btn_buy_save_Click(object sender, EventArgs e)
        {
            if(checkValue() == false)
            {
                MessageBox.Show("Error in value");
                return;
            }
            Save2DB();
        }

        private void Save2DB()
        {
            using (var context = new TradeContext())
            {
                var data = new TradeBotRange() {
                    UpBuyPrice = buyUpperPrice,
                    LowBuyPrice = buyLowerPrice,
                    BuyTakeProfitPrice = buytakeProfit,
                    BuyStopLossPrice = buyStopLoss,
                    BuyAvailable = buyAvailable,
                    IsActiveBuy = true,// cb_buy_isActive.Checked,
                    UpSellPrice = sellUpperPrice,
                    LowSellPrice = sellLowerPrice,
                    SellTakeProfitPrice = selltakeProfit,
                    SellStopLossPrice = sellStopLoss,
                    SellAvailable = sellAvailable,
                    IsActiveSell = true,//cb_sell_isActive.Checked,
                    CoinName = coinName,
                };
                if (isNewRange)
                    context.TradeBotRanges.Add(data);
                else
                {
                    data.Id = iDRow;
                    context.TradeBotRanges.AddOrUpdate(data);
                }
                context.SaveChanges();
            }
        }

        private bool checkValue()
        {
            if (!decimal.TryParse(tx_buy_up_price.Text, out buyUpperPrice)) return false;
            if (!decimal.TryParse(tx_buy_low_price.Text, out buyLowerPrice)) return false;
            if (!decimal.TryParse(tx_buy_profit_limit.Text, out buytakeProfit)) return false;
            if (!decimal.TryParse(tx_buy_stop_loss.Text, out buyStopLoss)) return false;
            if (!decimal.TryParse(tx_buy_available.Text, out buyAvailable)) return false;

            if (!decimal.TryParse(tx_sell_up_price.Text, out sellUpperPrice)) return false;
            if (!decimal.TryParse(tx_sell_low_price.Text, out sellLowerPrice)) return false;
            if (!decimal.TryParse(tx_sell_profit_limit.Text, out selltakeProfit)) return false;
            if (!decimal.TryParse(tx_sell_stop_loss.Text, out sellStopLoss)) return false;
            if (!decimal.TryParse(tx_sell_available.Text, out sellAvailable)) return false;

            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;
            foreach(var account in tradeAccountList)
                account.TrafficLogEvent-= TradeAcount_TrafficLogEvent;
            tradeAccountList.Clear();
        }

        private void cb_show_chart_buy_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStockSeries();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                using (var context = new TradeContext())
                {
                    dgTragingData.DataSource = context.TradingDatas.OrderBy(x=>x.CoinName).ToList();
                }
            }

        }
        
        private void btnExport_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                if(saveFileDialog1.FileName.ToLower().EndsWith(".csv")==false)
                {
                    saveFileDialog1.FileName += ".csv";
                }
                string row = string.Empty;
                foreach (DataGridViewColumn col in dgTragingData.Columns)
                {
                    row += "," + col.HeaderText;
                }
                row=row.TrimStart(',');
                using (var file =new StreamWriter(saveFileDialog1.FileName))
                {
                    file.WriteLine(row);
                    
                    for (int i = 0; i <= dgTragingData.RowCount - 1; i++)
                    {
                        row = string.Empty;
                        for (int j = 0; j <= dgTragingData.ColumnCount - 1; j++)
                        {
                            DataGridViewCell cell = dgTragingData[j, i];
                            row+=","+ cell.Value.ToString();
                        }
                        row=row.TrimStart(',');
                        file.WriteLine(row);
                    }
                }
            }
        }

        private void bt_start_buy_Click(object sender, EventArgs e)
        {
            var binanceInfo = BinanceDataCollector.Instance.GetBinance(coinName);
            if(binanceInfo == null)
            {
                MessageBox.Show("There is no Binance Info");
                return;
            }
            foreach (var account in tradeAccountList)
            {
                if(account.CoinName == coinName)
                {
                    bt_start_buy.Enabled = false;
                    bt_start_sell.Enabled = false;
                    return;
                }
            }
            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerBuyPrice = buyLowerPrice;
            tradeBox.stopLossBuyPrice = buyStopLoss;
            tradeBox.takeProfitBuyPrice = buytakeProfit;
            tradeBox.upperBuyPrice = buyUpperPrice;
            var lastCandle = binanceInfo.Candels_4hour.Last();
            CandleDto candle = new CandleDto();
            candle.lowPrice = lastCandle.LowPrice;
            candle.openPrice = lastCandle.OpenPrice;
            candle.closePrice = lastCandle.ClosePrice;
            candle.highPrice = lastCandle.HighPrice;

            Account tradeAcount = new Account(coinName, buyAvailable, candle, tradeBox);
            tradeAcount.TrafficLogEvent += TradeAcount_TrafficLogEvent;
            tradeAccountList.Add(tradeAcount);
            bt_start_buy.Enabled=false;
            bt_start_sell.Enabled=false;
        }

        private void TradeAcount_TrafficLogEvent(string message)
        {
            Action action = () => { listBox1.Items.Add(message); };
            if (InvokeRequired)
                action.Invoke();
            else { action();}                  
        }

        private void bt_stop_buy_Click(object sender, EventArgs e)
        {
            StopTrading(coinName);
        }

        private void StopTrading(string coinName)
        {
            Account tradeAcount = tradeAccountList.FirstOrDefault(t => t.CoinName == coinName);
            if (tradeAcount == null)
                return;

            tradeAcount.StopTrade();

            SaveTradeData2DB(tradeAcount);

            tradeAccountList.Remove(tradeAcount);
            bt_start_buy.Enabled = true;
            bt_start_sell.Enabled = true;
        }

        private void SaveTradeData2DB(Account tradeAcount)
        {
           using(var context = new TradeContext())
            {
                tradeAcount.tradingData.Net_Portfo_Profit=
                tradeAcount.tradingData.Net_Profit_Loss;
                tradeAcount.tradingData.DaysinPosition;
                tradeAcount.tradingData.GrossProfit_Loss;
                tradeAcount.tradingData.Cost;
                tradeAcount.tradingData.GrossProfit_Loss_Percentage;
                tradeAcount.tradingData.PercentageinTrade;
                tradeAcount.tradingData.PositionMargin;
                tradeAcount.tradingData.PositionValue;
                context.TradingDatas.Add(tradeAcount.tradingData);
                context.SaveChanges();
            }
        }

        private void bt_start_sell_Click(object sender, EventArgs e)
        {
            var binanceInfo = BinanceDataCollector.Instance.GetBinance(coinName);
            if (binanceInfo == null)
            {
                MessageBox.Show("There is no Binance Info");
                return;
            }
            foreach (var account in tradeAccountList)
            {
                if (account.CoinName == coinName)
                {
                    bt_start_buy.Enabled = false;
                    bt_start_sell.Enabled = false;
                    return;
                }
            }
            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerBuyPrice = buyLowerPrice;
            tradeBox.stopLossBuyPrice = buyStopLoss;
            tradeBox.takeProfitBuyPrice = buytakeProfit;
            tradeBox.upperBuyPrice = buyUpperPrice;
            var lastCandle = binanceInfo.Candels_4hour.Last();
            CandleDto candle = new CandleDto();
            candle.lowPrice = lastCandle.LowPrice;
            candle.openPrice = lastCandle.OpenPrice;
            candle.closePrice = lastCandle.ClosePrice;
            candle.highPrice = lastCandle.HighPrice;

            Account tradeAcount = new Account(coinName, buyAvailable, candle, tradeBox);
            tradeAccountList.Add(tradeAcount);
            tradeAcount.TrafficLogEvent += TradeAcount_TrafficLogEvent;
            bt_start_buy.Enabled = false;
            bt_start_sell.Enabled = false;
        }

        private void bt_stop_sell_Click(object sender, EventArgs e)
        {
            StopTrading(coinName);
        }
    }
}
