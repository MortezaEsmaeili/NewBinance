using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    public class WeightedValue//: INotifyPropertyChanged
    {
        public WeightedValue() { }
        public decimal Value { get; set; }
        public DateTime Time { get; set; }
        //public decimal Value {
        //    get
        //    {
        //        return this._value;
        //    }
        //    set
        //    {
        //        this._value = value;
        //        //this.OnPropertyChanged("Value");
        //    }
        //}
        //public DateTime Time {
        //    get
        //    {
        //        return this.time;
        //    }
        //    set
        //    {
        //        this.time = value;
        //       // this.OnPropertyChanged("Time");
        //    }
        //}
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}
