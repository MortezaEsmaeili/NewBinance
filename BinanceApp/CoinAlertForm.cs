using Binance.Net.Enums;
using BinanceApp.Business;
using BinanceApp.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceApp
{
    public partial class CoinAlertForm : Telerik.WinControls.UI.RadForm
    {
        public CoinAlertForm()
        {
            InitializeComponent();
        }

        private void CoinAlertForm_Load(object sender, EventArgs e)
        {
            StrategySFBusiness.AlertEvent += NewSignal;

            //NewSignal("BITCOIN", KlineInterval.OneDay, new AlertInfo //for test
            //{
            //    CloseTime = DateTime.Now,
            //    DMH = 23,
            //    DML = 40,
            //    LocalTime = DateTime.Now,
            //    LT_SF = 5,
            //    MH = 4,
            //    ML = 3,
            //    Price = 1234,
            //    ST_SF = 8,
            //    Signal = SignalState.SellPosition
            //});
        }

        private void NewSignal(string coinName, KlineInterval interval, AlertInfo alertInfo)
        {
            if (InvokeRequired)
            {
                Action act = new Action(()=> AddNewSignal(coinName, interval, alertInfo));

               this.Invoke(act);
            }
            else
                AddNewSignal(coinName, interval, alertInfo);
        }

        private void AddNewSignal(string coinName, KlineInterval interval, AlertInfo alertInfo)
        {
            var item = lv_alerts.Items.Insert( 0, new ListViewItem (new[] { $"{coinName}_{interval}" , alertInfo.LocalTime.ToString("yyyy/MM/dd HH:mm:ss"), 
                alertInfo.CloseTime.ToString("yyyy/MM/dd HH:mm:ss"), alertInfo.Price.ToString(), alertInfo.ML.ToString(), alertInfo.MH.ToString(),  alertInfo.DML.ToString(),
                alertInfo.DMH.ToString(), alertInfo.ST_SF.ToString(), alertInfo.LT_SF.ToString(), alertInfo.Signal.ToString()}));
            if (alertInfo.Signal == SignalState.BuyPosition)
                item.BackColor = Color.LightGreen;
            else if (alertInfo.Signal == SignalState.SellPosition)
                item.BackColor = Color.Pink;
            else
                item.BackColor = Color.LightGray;

            if (lv_alerts.Items.Count > 200)
            {
                for(int i=1; i<10;  i++)
                    lv_alerts.Items.RemoveAt(lv_alerts.Items.Count - i);
            }
        }
    }
}
