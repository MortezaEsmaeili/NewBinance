using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp
{
    public class CandleDataInfo : INotifyPropertyChanged
    {
        private DateTime date;
        private decimal open;
        private decimal high;
        private decimal low;
        private decimal close;

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
                this.OnPropertyChanged("Date");
            }
        }

        public decimal Open
        {
            get
            {
                return this.open;
            }
            set
            {
                this.open = value;
                this.OnPropertyChanged("Open");
            }
        }

        public decimal High
        {
            get
            {
                return this.high;
            }
            set
            {
                this.high = value;
                this.OnPropertyChanged("High");
            }
        }

        public decimal Low
        {
            get
            {
                return this.low;
            }
            set
            {
                this.low = value;
                this.OnPropertyChanged("Low");
            }
        }

        public decimal Close
        {
            get
            {
                return this.close;
            }
            set
            {
                this.close = value;
                this.OnPropertyChanged("Close");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}


