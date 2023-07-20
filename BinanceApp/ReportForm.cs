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
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        internal void AddReport(ReportModel reportModel)
        {
            usersTrades1.DataTable1.AddDataTable1Row(reportModel.Time, reportModel.UserName, reportModel.TP,
                reportModel.SP, reportModel.Price, reportModel.Position, reportModel.Profit, reportModel.CoinName);
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            report1.Show();
        }
    }
}
