using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.Business
{
    public static class PercentClassifier
    {
        public static List<double> Classify(List<double> values, double percent)
        {
            try
            {
                if (percent >= 100) return new List<double>();
                if (values == null || values.Count < 3) return new List<double>();
                var orderlist = values.OrderBy(x => x).ToList();
                List<double> diff = new List<double>();
                for (int i = 1; i < orderlist.Count(); i++)
                    diff.Add(orderlist[i] - orderlist[i - 1]);
                double range = orderlist.Last() - orderlist.First();
                List<double> breaks = new List<double>();
                double rule = range * percent / 100;
                for (int i = 0; i < diff.Count; i++)
                    if (diff[i] > rule)
                        breaks.Add(orderlist[i + 1]);
                return breaks;
            }
            catch
            {
                return new List<double>();
            }
        }
        
    }
}
