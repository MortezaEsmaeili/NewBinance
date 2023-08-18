using BinanceApp.Business;
using Observer.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceApp
{
    public partial class mainForm : Telerik.WinControls.UI.RadForm
    {
        private TradeForm tradeForm = null;
        private CoinAlertForm alertForm = null;
        private string UserName = "Guest";
        public mainForm()
        {
            // OpenWindows = new List<Form>();
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
           /* LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                UserName = loginForm.UserName;
                if (UserName == "Guest")
                {
                    strategyAllSFsToolStripMenuItem.Enabled = false;
                    strategySFToolStripMenuItem.Enabled = false;
                    allShortTermForOneCoinToolStripMenuItem.Enabled = false;
                    allLongTermForOneCoinToolStripMenuItem.Enabled = false;
                }

            }
            else
            {
                MessageBox.Show("You should login to continue.", "Alert", MessageBoxButtons.OK);
                this.Close();
            }*/
            Task a = initGettingDataAsync();
            
            alertForm = new CoinAlertForm();
          //  alertForm.MdiParent = this;
            alertForm.Hide();

            LogHelper.Initialize(100);
            //CriticalPointCollector.Initialize(); //pegah commented -- this calculation is no more in use

            
        }

        private async Task initGettingDataAsync()
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Esmaeili\\Markets");
            var _market = key.GetValue("Market");
            key.Close();
            if (_market == null)
                await Task.Run( ()=>BinanceDataCollector.Instance.InitializeModels(DataModel.MarketType.SPOT));
            else
            {
                if (_market.ToString() == "FUTURE")
                {
                    futureToolStripMenuItem.Checked = true;
                    spotToolStripMenuItem.Checked = false;
                    await Task.Run(()=>BinanceDataCollector.Instance.InitializeModels(DataModel.MarketType.FUTURE));
                }
                else
                   await Task.Run(() => BinanceDataCollector.Instance.InitializeModels(DataModel.MarketType.SPOT));
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void sabaFamBinanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new AboutForm();
            frm.ShowDialog();
        }

        private void marketViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var marketForm = new Form1();
            marketForm.MdiParent = this;
            //OpenWindows.Add(marketForm);
            marketForm.Show();
        }

        private void waToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var weightedPrice = new WeightedPriceChart();
            weightedPrice.MdiParent = this;
            //OpenWindows.Add(weightedPrice);
            weightedPrice.Show();

        }

        private void strategy01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var strategy01 = new strategies01Form();
            strategy01.MdiParent = this;
            //OpenWindows.Add(weightedPrice);
            strategy01.Show();
        }

        private void strategy02ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var strategy02 = new strategies02Form();
            strategy02.MdiParent = this;
            //OpenWindows.Add(weightedPrice);
            strategy02.Show();
        }

        private void strategy03ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var strategy03 = new strategies03Form();
            strategy03.MdiParent = this;
            strategy03.Show();
        }

        private void spotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (spotToolStripMenuItem.Checked)
                return;
            futureToolStripMenuItem.Checked = false;
            spotToolStripMenuItem.Checked = true;

            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Esmaeili\\Markets");
            key.SetValue("Market", "SPOT");
            key.Close();
            if (MessageBox.Show("Do you want to continue?", "Application should be closed!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                System.Windows.Forms.Application.Exit();
        }

        private void futureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (futureToolStripMenuItem.Checked)
                return;
            futureToolStripMenuItem.Checked = true;
            spotToolStripMenuItem.Checked = false;
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Esmaeili\\Markets");
            key.SetValue("Market", "FUTURE");
            key.Close();
            if(MessageBox.Show("Do you want to continue?", "Application should be closed!",MessageBoxButtons.OKCancel ,MessageBoxIcon.Warning) == DialogResult.OK)
                System.Windows.Forms.Application.Exit();
        }

        private void strategy04ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var strategy04 = new strategies04Form();
            strategy04.MdiParent = this;
            strategy04.Show();
        }

        private void strategySFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var strategySf = new strategiesSfForm();
            strategySf.MdiParent = this;
            strategySf.Show();
        }

        private void strategy1SFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var st_1SfForm = new strategies1_SfForm();
            st_1SfForm.MdiParent = this;
            st_1SfForm.Show();
        }

        private void strategyAllSFsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var strategyAllSf = new strategiesAllSfForm();
            strategyAllSf.MdiParent = this;
            strategyAllSf.Show();
        }

        private void priseMLMHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dmhlForm = new DmhDmlForm();
            dmhlForm.MdiParent = this;
            dmhlForm.Show();
        }

        private void alertToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (alertToolStripMenuItem.Checked)
            {
                if (alertForm == null)
                {
                    alertForm = new CoinAlertForm();
                   // alertForm.MdiParent = this;
                }
                alertForm.Show();
            }
            else
                alertForm.Hide();
        }


        private void alertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alertToolStripMenuItem.Checked = !alertToolStripMenuItem.Checked;
        }

        private void tradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tradeToolStripMenuItem.Checked = !tradeToolStripMenuItem.Checked;
        }

        private void tradeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (tradeToolStripMenuItem.Checked)
            {
                if (tradeForm == null)
                {
                    tradeForm = new TradeForm();
                }
                tradeForm.Show();
            }
            else
                tradeForm.Hide();
        }

        private void allShortTermForOneCoinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var allShortterm = new strategiesShortSfForm();
            allShortterm.MdiParent = this;
            allShortterm.Show();
        }

        private void allLongTermForOneCoinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var allLongterm = new strategiesLongSfForm();
            allLongterm.MdiParent = this;
            allLongterm.Show();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                UserName = loginForm.UserName;
                if (UserName == "Guest")
                {
                    strategyAllSFsToolStripMenuItem.Enabled = false;
                    strategySFToolStripMenuItem.Enabled = false;
                    allShortTermForOneCoinToolStripMenuItem.Enabled = false;
                    allLongTermForOneCoinToolStripMenuItem.Enabled = false;
                    strategy1SFToolStripMenuItem.Enabled = false;
                }
                else
                {
                    strategyAllSFsToolStripMenuItem.Enabled = true;
                    strategySFToolStripMenuItem.Enabled = true;
                    allShortTermForOneCoinToolStripMenuItem.Enabled = true;
                    allLongTermForOneCoinToolStripMenuItem.Enabled = true;
                    strategy1SFToolStripMenuItem.Enabled = true;
                }
            }
           
        }

        private void buyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var buyForm = new BuyForm();
            buyForm.MdiParent = this;
            buyForm.Show();
        }

        private void strategyMASFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sf_ma = new strategiesSfMaForm();
            sf_ma.MdiParent = this;
            sf_ma.Show();
        }

        private void localMinMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var minmax = new localMinMaxForm();
            minmax.MdiParent = this;
            minmax.Show();
        }

        private void kMeansEMAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var kmeansEma = new strategiesEma();
            kmeansEma.MdiParent = this;
            kmeansEma.Show();
        }

        private void percentStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Percentfrm = new PercentForm();
            Percentfrm.MdiParent = this;
            Percentfrm.Show();
        }

        private void mAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var macdPmaMpricefrm = new macdPmaMpriceForm();
            macdPmaMpricefrm.MdiParent = this;
            macdPmaMpricefrm.Show();
        }

        private void cPMA1001ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cp_MA100Form = new CP_MA100Form();
            cp_MA100Form.MdiParent = this;
            cp_MA100Form.Show();
        }

        private void mAMLMHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ma100MHMLForm = new MA100MHMLForm();
            ma100MHMLForm.MdiParent = this;
            ma100MHMLForm.Show();
        }
    }
}
