using BinanceApp.Business;
using BinanceApp.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BinanceApp
{
    /// <summary>
    /// Interaction logic for AlertCoinsCollection.xaml
    /// </summary>
    public partial class AlertCoinCollection : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<AlertIntervalCollection> CoinCollectionList { get; set; }
        public AlertCoinCollection()
        {
            InitializeComponent();
            CoinCollectionList = new ObservableCollection<AlertIntervalCollection>();
            foreach(var coin in BinanceDataCollector.Instance.CoinNames)
            {
                CoinCollectionList.Add(new AlertIntervalCollection(coin));
            }
            this.DataContext = this;
            OnPropertyChanged("CoinCollectionList");

        }

        internal void AddNewSignal(string coin, string interval, AlertInfo alertInfo)
        {
            try
            {
                AlertIntervalCollection item = CoinCollectionList.FirstOrDefault(c => c.CoinName == coin);
                if (item != null)
                {
                    item.AddSignal(interval, alertInfo);
                }

            }
            catch (Exception ex)
            {
                LogHelper.SendError(this.GetType(), "AddNewSignal to AlertCoinCollection error:" + ex.Message, ex, null);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
