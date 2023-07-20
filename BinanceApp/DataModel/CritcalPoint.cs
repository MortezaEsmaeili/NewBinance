using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.DataModel
{
    public class CritcalPoint
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal EMA5 { get; set; }
        public decimal EMA25 { get; set; }
        public decimal EMA75 { get; set; }
        public decimal EMA150 { get; set; }
        public decimal EMA300 { get; set; }
        public double DEMA5 { get; set; }
        public double DEMA25 { get; set; }
        public double DEMA75 { get; set; }
        public double DEMA150 { get; set; }
        public double DEMA300 { get; set; }
        public decimal MH1 { get; set; }
        public decimal MH5 { get; set; }
        public decimal MH15 { get; set; }
        public decimal MH30 { get; set; }
        public decimal MH60 { get; set; }
        public decimal ML1 { get; set; }
        public decimal ML5 { get; set; }
        public decimal ML15 { get; set; }
        public decimal ML30 { get; set; }
        public decimal ML60 { get; set; }
        public string Description { get; set; }
    }
    
}
