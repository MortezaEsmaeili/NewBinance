using Binance.Dal;
using Binance.Dal.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            using (var context = new TradeContext())
            {
                context.Migrate();
                

                var data = new TradeBotRange() { BuyStopLossPrice = 10, BuyTakeProfitPrice = 50, CoinName = "ggg" };
                context.TradeBotRanges.Add(data);
                context.SaveChanges();
            }
        }
    }
}
