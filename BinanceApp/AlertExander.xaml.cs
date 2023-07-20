using BinanceApp.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AlertExpanderCtrl : UserControl, INotifyPropertyChanged
    {
        public string Interval { get; set; }
        public string SignalColor { get; set; }
        public SignalState CurrentState { get; set; }

        private Timer timer;
        private int blinkCount = 0;

        private string coin_interval;
        public string Coin_Interval { 
            get { return coin_interval; }
            set { coin_interval = value; } }
        public ObservableCollection<AlertInfo> AlertTable { get; set; }

        public AlertExpanderCtrl(string coin, string interval)
        {
            InitializeComponent();
            this.DataContext = this;
            Coin_Interval = $"{coin}ــ{interval}";
            Interval = interval;
            AlertTable = new ObservableCollection<AlertInfo>();
            //AlertTable.Add(new AlertInfo { CloseTime = DateTime.Now, DMH = 20, DML = 30, LocalTime = DateTime.Now, LT_SF = 5, MH = 1000, ML = 900, Price = 950, Signal = SignalState.wait, ST_SF = 30 });
            Alerts.ItemsSource = AlertTable;
            CurrentState = SignalState.wait;
            SignalColor = "Gray";
        }
        [STAThread]
        internal void AddSignal(AlertInfo alertInfo)
        {
            try
            {
                Dispatcher.Invoke(() => { 
                    AlertTable.Add(alertInfo);
                    if (alertInfo.Signal == SignalState.BuyPosition)
                    {
                        SignalColor = "LightGreen";
                        CurrentState = SignalState.BuyPosition;
                    }
                    else if (alertInfo.Signal == SignalState.SellPosition)
                    { 
                        SignalColor = "Red";
                        CurrentState = SignalState.SellPosition;
                    }
                    else
                        SignalColor = "Gray";
                    OnPropertyChanged("SignalColor");
                    if (CurrentState == SignalState.SellPosition || CurrentState == SignalState.BuyPosition)
                        timer = new Timer(Blink, null, 2000, 2000);
                });
            }
            catch(Exception ex)
            {

            }
        }

        private void Blink(object state)
        {
            if(blinkCount>10)
            {
                blinkCount = 0;
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
            }
            if (blinkCount % 2 == 0)
            {
                switch(CurrentState)
                {
                    case SignalState.BuyPosition:
                        SignalColor = "LightGreen";
                        break;
                    case SignalState.SellPosition:
                        SignalColor = "Red";
                        break;
                }                
            }
            else
                SignalColor = "Gray";
            blinkCount++;

            OnPropertyChanged("SignalColor");
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
