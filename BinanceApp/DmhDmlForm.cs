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
    public partial class DmhDmlForm : Form
    {
        private Dictionary<string, CoinDML> CoinDMLs = new Dictionary<string, CoinDML>();
        private Dictionary<string, CoinDMH> CoinDMHs = new Dictionary<string, CoinDMH>();

        private List<CoinDML> DML_List = new List<CoinDML>();
        private List<CoinDMH> DMH_List = new List<CoinDMH>();

        private BindingList<CoinDML> bindingListDML { get; set; }
        private BindingList<CoinDMH> bindingListDMH { get; set; }

        public DmhDmlForm()
        {
            InitializeComponent();
        }

        private void DmhDmlForm_Load(object sender, EventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent += OnDataReadyEvent;
            OnDataReadyEvent();
            bindingListDML = new BindingList<CoinDML>(DML_List);
            bindingSource1 = new BindingSource(bindingListDML, null);
            dg_DML.DataSource = bindingSource1;

            bindingListDMH = new BindingList<CoinDMH>(DMH_List);
            bindingSource2 = new BindingSource(bindingListDMH, null);
            dg_DMH.DataSource = bindingSource2;
        }
        private delegate void delegateFunc();

        private void OnDataReadyEvent()
        {
            if (InvokeRequired)
            {
                this.Invoke(new delegateFunc( UpdateData));
            }
            else
                UpdateData();
        }
        private void UpdateData()
        {
            foreach (var coin in BinanceDataCollector.Instance.CoinNames)
            {
                var binanceModel = BinanceDataCollector.Instance.GetBinance(coin);
                if (binanceModel != null&& binanceModel.DMLDic.Any()&& binanceModel.DMLDic.Any())
                {
                    CoinDML dml = new CoinDML
                    {
                        CoinName = coin,
                        DML_1Min = binanceModel.DMLDic[KlineInterval.OneMinute],
                        DML_5Min = binanceModel.DMLDic[KlineInterval.FiveMinutes],
                        DML_15Min = binanceModel.DMLDic[KlineInterval.FifteenMinutes],
                        DML_30Min = binanceModel.DMLDic[KlineInterval.ThirtyMinutes],
                        DML_60Min = binanceModel.DMLDic[KlineInterval.OneHour],
                        DML_4Hour = binanceModel.DMLDic[KlineInterval.FourHour],
                        DML_1Day = binanceModel.DMLDic[KlineInterval.OneDay]
                    };

                    if (CoinDMLs.ContainsKey(coin))
                        CoinDMLs[coin] = dml;
                    else
                        CoinDMLs.Add(coin, dml);

                    CoinDMH dmh = new CoinDMH
                    {
                        CoinName = coin,
                        DMH_1Min = binanceModel.DMHDic[KlineInterval.OneMinute],
                        DMH_5Min = binanceModel.DMHDic[KlineInterval.FiveMinutes],
                        DMH_15Min = binanceModel.DMHDic[KlineInterval.FifteenMinutes],
                        DMH_30Min = binanceModel.DMHDic[KlineInterval.ThirtyMinutes],
                        DMH_60Min = binanceModel.DMHDic[KlineInterval.OneHour],
                        DMH_4Hour = binanceModel.DMHDic[KlineInterval.FourHour],
                        DMH_1Day = binanceModel.DMHDic[KlineInterval.OneDay]                        
                    };

                    if (CoinDMHs.ContainsKey(coin))
                        CoinDMHs[coin] = dmh;
                    else
                        CoinDMHs.Add(coin, dmh);
                }

            }
            DML_List = CoinDMLs.Values.ToList();
            bindingListDML = new BindingList<CoinDML>(DML_List);
            bindingSource1.DataSource = bindingListDML;
            bindingSource1.ResetBindings(false);

            DMH_List = CoinDMHs.Values.ToList();
            bindingListDMH = new BindingList<CoinDMH>(DMH_List);
            bindingSource2.DataSource = bindingListDMH;
            bindingSource2.ResetBindings(false);

        }

        private void DmhDmlForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BinanceDataCollector.Instance.DataReadyEvent -= OnDataReadyEvent;

        }
    }
}
