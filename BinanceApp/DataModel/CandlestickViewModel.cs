
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using BinanceApp.DataModel;
using System.Linq;

namespace BinanceApp
{
    public class CandlestickViewModel : INotifyPropertyChanged
    {
        BindingList<CandleDataInfo> data;

        public CandlestickViewModel()
        {
        }

        public BindingList<CandleDataInfo> Data
        {
            get
            {
                return data;
            }
            set
            {
                if (this.data != value)
                {
                    this.data = value;
                    this.OnPropertyChanged("Data");
                }
            }
        }
        private const int SampleCount = 50;
        public static BindingList<CandleDataInfo> ParseData(List<TradeModel> trades)
        {
            BindingList<CandleDataInfo> chartData = new BindingList<CandleDataInfo>();

            int step = trades.Count / SampleCount;
            for(int i=0; i<SampleCount; i++)
            {
                var values =trades.GetRange(i * step, step).OrderBy(d=>d.Time);
                if(values!=null && values.Any())
                {
                    var lastValue = values.LastOrDefault();
                    chartData.Add(new CandleDataInfo
                    {
                        Close = lastValue.Price,
                        Date = lastValue.Time,
                        High = values.Max(p => p.Price),
                        Low = values.Min(p => p.Price),
                        Open = values.First().Price
                    });

                }
            }
            return chartData;
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

