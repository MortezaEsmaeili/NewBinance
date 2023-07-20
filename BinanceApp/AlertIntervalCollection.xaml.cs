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
    /// Interaction logic for AlertCollection.xaml
    /// </summary>
    public partial class AlertIntervalCollection : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<AlertExpanderCtrl> IntervalCollectionList{ get; set; }
        public string CoinName { get; set; }
        internal void AddNewSignal(string coin, string interval, AlertInfo alertInfo)
        {
            try
            {
                //var collectionId = $"{coin}ــ{interval}";
                var collectionId = interval;
                AlertExpanderCtrl item = IntervalCollectionList.FirstOrDefault(c => c.Coin_Interval == collectionId);
                if (item != null)
                {
                    item.AddSignal(alertInfo);
                }               
            }
            catch(Exception ex)
            {
                LogHelper.SendError(this.GetType(), "AddNewSignal to AlertForm error:" + ex.Message, ex, null);
            }
        }

        internal void AddSignal(string interval, AlertInfo alertInfo)
        {
            try
            {
                //string key = $"{CoinName}ــ{interval}";
                string key = interval;
                AlertExpanderCtrl item = IntervalCollectionList.FirstOrDefault(c => c.Interval == key);
                if (item != null)
                {
                    item.AddSignal( alertInfo);
                }

            }
            catch (Exception ex)
            {
                LogHelper.SendError(this.GetType(), "AddNewSignal to AlertCoinCollection error:" + ex.Message, ex, null);
            }
        }

        public AlertIntervalCollection(string coin)
        {
            InitializeComponent();
            CoinName = coin;

            IntervalCollectionList = new ObservableCollection<AlertExpanderCtrl>();
            foreach(var interval in BinanceDataCollector.Instance.Intervals)
            {
                var alt = new AlertExpanderCtrl(CoinName, interval.ToString());
                IntervalCollectionList.Add(alt);
            }
            
            this.DataContext = this;
            OnPropertyChanged("IntervalCollectionList");

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
   
