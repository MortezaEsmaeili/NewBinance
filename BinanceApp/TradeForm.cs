using Binance.Net.Enums;
using Binance.Net.Interfaces;
using BinanceApp.Business;
using BinanceApp.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace BinanceApp
{
    public partial class TradeForm : Telerik.WinControls.UI.RadForm
    {
        private delegate void UpdateFormDelegate(BinanceModel binanceModel);

        private string coinName = string.Empty;
        private BinanceModel binanceModel;

        private double OpenPositionPrice = 0;
        private double stopLossPrice = 0;
        private double takeProfitPrice = 0;

        private bool tradeStart = false;
        enum TradePosition
        {
            Buy,
            Sell,
            wait
        }

        TradePosition tradePosition = TradePosition.wait;
        
        public TradeForm()
        {
            InitializeComponent();
        }
        
        private void TradeForm_Load(object sender, EventArgs e)
        {
            WireEvents();
            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
            this.radDropDownList1.DataSource = BinanceDataCollector.Instance.CoinNames;

            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Esmaeili\\User");
            var _user = key.GetValue("User");
            key.Close();
            if(_user != null)
            {
                userName_TB.Text = _user.ToString();
            }

        }
        private void TradeForm_Closed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;
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
            if (InvokeRequired)
            {
                var d = new UpdateFormDelegate(UpdateData);
                this.Invoke(d, binanceModel);
            }
            else
                UpdateData(binanceModel);
        }

        protected void WireEvents()
        {
            this.radDropDownList1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(radDropDownList1_SelectedIndexChanged);
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
            }
        }

        private void UpdateData(BinanceModel binanceModel)
        {
            try
            {
                if (binanceModel == null) return;
                price_LB.Text = binanceModel.CurrentPrice.ToString("F");
                checkTrade(binanceModel.CurrentPrice);
            }
            catch(Exception ex)
            { }
        }

        private void btn_trend_Click(object sender, EventArgs e)
        {
            if (btn_Trade.Text == "Close Position")
                return;
            if(btn_Position.Text.StartsWith("Position"))
            {
                btn_Position.Text = "Buy";
                btn_Position.BackColor = Color.Green;
                tradePosition = TradePosition.Buy;

                radDropDownList1.Enabled = true;
                btn_Trade.Enabled = true;
            }
            else if(btn_Position.Text == "Buy")
            {
                btn_Position.Text = "Sell";
                btn_Position.BackColor = Color.Red;
                tradePosition = TradePosition.Sell;

                radDropDownList1.Enabled = true;
                btn_Trade.Enabled = true;
            }
            else
            {
                btn_Position.Text = "Position";
                btn_Position.BackColor = Color.Gray;
                tradePosition = TradePosition.wait;

                radDropDownList1.Enabled = false;
                btn_Trade.Enabled = false;
            }
        }

        private void btn_Trade_Click(object sender, EventArgs e)
        {
            if(btn_Trade.Text == "Open Position")
            {
                
                openPosition();
            }
            else
            {
                
                closePosition();
            }
        }

        private void closePosition()
        {
            btn_Trade.Text = "Open Position";
            tradeStart = false;
            double closePrice = 0;
            double.TryParse(price_LB.Text, out closePrice);
            string position = string.Empty;
            double iPercent = 0;
            if (tradePosition == TradePosition.Buy)
            {
                listBox_Report.Items.Add($"{DateTime.Now}: Sell {coinName} for {price_LB.Text}");
                iPercent = ((closePrice - OpenPositionPrice) / OpenPositionPrice) * 100;
                listBox_Report.Items.Add($"your profit: {iPercent.ToString()} %");
                listBox_Report.Items.Add("------------------------------------");
                position = "Close Buy";
                
            }
            if(tradePosition == TradePosition.Sell)
            {
                listBox_Report.Items.Add($"{DateTime.Now}: Buy {coinName} for {price_LB.Text}");
                iPercent = ((OpenPositionPrice - closePrice) / OpenPositionPrice) * 100;
                listBox_Report.Items.Add($"your profit: {iPercent.ToString()} %");
                listBox_Report.Items.Add("------------------------------------");
                position = "Close Sell";
            }
            if(string.IsNullOrWhiteSpace(position)==false)
            {
                AddReport(new ReportModel
                {
                    Time = DateTime.Now,
                    CoinName = coinName,
                    Price = price_LB.Text,
                    UserName = userName_TB.Text,
                    SP = tb_stopLoss.Text +" %",
                    TP = tb_takeProfit.Text+" %",
                    Position = position,
                    Profit = iPercent.ToString("F") + " %"
                });
            }
        }

        private void openPosition()
        {
            btn_Trade.Text = "Close Position";
            tradeStart = true;
            double.TryParse(price_LB.Text, out OpenPositionPrice);
            string position = string.Empty;
            if (tradePosition == TradePosition.Buy )
            {
                listBox_Report.Items.Add($"{DateTime.Now}: Buy {coinName} for {price_LB.Text}");
                double temp = 0;
                double.TryParse(tb_takeProfit.Text, out temp);
                takeProfitPrice = (1 + (temp / 100)) * OpenPositionPrice;
                double.TryParse(tb_stopLoss.Text, out temp);
                stopLossPrice = (1 - (temp / 100)) * OpenPositionPrice;
                position = "Buy Position";
            }
            if(tradePosition == TradePosition.Sell)
            {
                listBox_Report.Items.Add($"{DateTime.Now}: Sell {coinName} for {price_LB.Text}");
                double temp = 0;
                double.TryParse(tb_takeProfit.Text, out temp);
                takeProfitPrice = (1 - (temp / 100)) * OpenPositionPrice;
                double.TryParse(tb_stopLoss.Text, out temp);
                stopLossPrice = (1 + (temp / 100)) * OpenPositionPrice;
                position = "Sell Position";
            }
            if (string.IsNullOrWhiteSpace(position) == false)
            {
                AddReport(new ReportModel
                {
                    Time = DateTime.Now,
                    CoinName = coinName,
                    Price = price_LB.Text,
                    UserName = userName_TB.Text,
                    SP = tb_stopLoss.Text + " %",
                    TP = tb_takeProfit.Text + " %",
                    Position = position,
                    Profit = "-------"
                });
            }
        }

        private void checkTrade(decimal currentPrice1)
        {
            if (!tradeStart)
                return;
            double currentPrice = 0;
            double.TryParse(currentPrice1.ToString(), out currentPrice);

            if (tradePosition == TradePosition.Buy)
            {
                if (currentPrice > takeProfitPrice ||
                    currentPrice < stopLossPrice)
                {
                    closePosition();
                }
            }
            if (tradePosition == TradePosition.Sell)
            {
                if (currentPrice < takeProfitPrice ||
                    currentPrice > stopLossPrice)
                {
                    closePosition();
                }
            }
        }

        private void userName_TB_Leave(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Esmaeili\\User");
            key.SetValue("User", userName_TB.Text);
            key.Close();
        }

        private void price_LB_Click(object sender, EventArgs e)
        {

        }

        private void tb_stopLoss_Leave(object sender, EventArgs e)
        {
            if (!tradeStart)
                return;
            if (tradePosition == TradePosition.Buy)
            {
                double temp = 0;
                double.TryParse(tb_takeProfit.Text, out temp);
                takeProfitPrice = (1 + (temp / 100)) * OpenPositionPrice;
                double.TryParse(tb_stopLoss.Text, out temp);
                stopLossPrice = (1 - (temp / 100)) * OpenPositionPrice;
            }
            if (tradePosition == TradePosition.Sell)
            {
                double temp = 0;
                double.TryParse(tb_takeProfit.Text, out temp);
                takeProfitPrice = (1 - (temp / 100)) * OpenPositionPrice;
                double.TryParse(tb_stopLoss.Text, out temp);
                stopLossPrice = (1 + (temp / 100)) * OpenPositionPrice;
            }
        }
        internal void AddReport(ReportModel reportModel)
        {
            usersTrades1.DataTable1.AddDataTable1Row(reportModel.Time, reportModel.UserName, reportModel.TP,
                reportModel.SP, reportModel.Price, reportModel.Position, reportModel.Profit, reportModel.CoinName);
        }

        private void btn_getReport_Click(object sender, EventArgs e)
        {
            //for(int i = usersTrades2.DataTable1.Rows.Count -1 ; i>=0; i--)
            //{
            //    usersTrades1.DataTable1.AddDataTable1Row((UsersTrades.DataTable1Row)
            //        usersTrades2.DataTable1.Rows[i]);
            //}
            this.Hide();
            report1.Show();
            this.Show();
        }
    }
}
