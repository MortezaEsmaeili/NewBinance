﻿using Binance.Dal;
using Binance.Dal.Model;
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
using System.Data.Entity.Migrations;
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
    public partial class TradeBotForm : Telerik.WinControls.UI.RadForm
    {
        public TradeBotForm()
        {
            InitializeComponent();
        }

        Telerik.WinControls.UI.DateTimeCategoricalAxis dateTimeCategoricalAxis2 = new Telerik.WinControls.UI.DateTimeCategoricalAxis();
        Telerik.WinControls.UI.LinearAxis linearAxis2 = new Telerik.WinControls.UI.LinearAxis();

        CandlestickSeries candleSery = new CandlestickSeries();

        private string coinName = string.Empty;
        private bool isNewRange = false;
        long iDRow = 0;


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

            coinName = this.radDropDownList3.SelectedItem.Text;

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

            tx_sell_up_price.Text = tradeBotRange.UpSellPrice.ToString();
            tx_sell_low_price.Text = tradeBotRange.LowSellPrice.ToString();
            tx_sell_profit_limit.Text = tradeBotRange.SellTakeProfitPrice.ToString();
            tx_sell_stop_loss.Text = tradeBotRange.BuyStopLossPrice.ToString();
            tx_sell_available.Text = tradeBotRange.SellAvailable.ToString();
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
            dateTimeCategoricalAxis2.MajorTickInterval = 5;

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
        }
        private void UpdateStockSeries()
        {
            try
            {
                if (binance == null ) return;

                this.radChartView2.Series.Clear();
                if (binance.Tbqv_15min.Any() == false)
                    binance = BinanceDataCollector.Instance.GetBinance(coinName);
                candels = binance.Candels_4hour.Skip(binance.Candels_4hour.Count() - 250).Take(250).ToList();

                if (candels == null || candels.Any() == false) return;
                candleSery.DataSource = candels;

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
            }
            catch (Exception ex)
            { }
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
            if (cb_show_chart_buy.Checked)
                ShowbuyLines();
            if (cb_show_chart_sell.Checked)
                ShowSellLine();
        }

        private void ShowSellLine()
        {
            throw new NotImplementedException();
        }

        private void ShowbuyLines()
        {
            throw new NotImplementedException();
        }

        private void Save2DB()
        {
            using (var context = new TradeContext())
            {
                var data = new TradeBotRange() { BuyStopLossPrice = 10, BuyTakeProfitPrice = 50, CoinName = "ggg" };
                context.TradeBotRanges.AddOrUpdate(data);
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

        }

    }
}
