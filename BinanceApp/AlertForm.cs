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
    public partial class AlertForm : Telerik.WinControls.UI.RadForm
    {
        public AlertForm()
        {
            InitializeComponent();
        }

        private void AlertForm_Load(object sender, EventArgs e)
        {
            StrategySFBusiness.AlertEvent += NewSignal;
        }

        
        public void NewSignal(string coin, KlineInterval interval, AlertInfo alertInfo)
        {
            ((AlertCoinCollection)(AlertView.Child)).AddNewSignal(coin, interval.ToString(), alertInfo);
        }

        
    }
}
