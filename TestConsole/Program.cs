using Binance.BotState;
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
            /* using (var context = new TradeContext())
             {
                 context.Migrate();


                 var data = new TradeBotRange() { BuyStopLossPrice = 10, 
                     BuyTakeProfitPrice = 50, CoinName = "ggg", IsActiveSell = true, IsActiveBuy=true
                 };
                 context.TradeBotRanges.Add(data);
                 context.SaveChanges();
             }*/
            Account account = CreateTestAccount();
            SaveTradeData2DB(account);
        }

        private static Account CreateTestAccount()
        {
            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerBuyPrice = 60;
            tradeBox.stopLossBuyPrice = 40;
            tradeBox.takeProfitBuyPrice = 100;
            tradeBox.upperBuyPrice = 80;

            Account account = new Account("Ali", 10000, tradeBox, "Buy");
            account.TrafficLogEvent += Account_TrafficLogEvent;
            var prices = new decimal[] { 85, 80, 75, 70, 65, 60, 55, 50, 45, 60, 70, 90, 100, 120, 130 };
            account.TrafficLogEvent += Account_TrafficLogEvent;

            //Act
            foreach (var price in prices)
                account.SetNewPrice(price);
            return account;
        }

        private static void Account_TrafficLogEvent(string message)
        {
            Console.WriteLine(message);
        }

        private static void SaveTradeData2DB(Account tradeAcount)
        {
            using (var context = new TradeContext())
            {
                try
                {
                    if (tradeAcount.tradingData.Available == 0 ||
                        tradeAcount.tradingData.Leverage == 0)
                        return;
                    tradeAcount.tradingData.CloseDate = DateTime.Now;
                    tradeAcount.tradingData.Leverage = 3;
                    tradeAcount.tradingData.PositionValue =
                        tradeAcount.tradingData.Amount * tradeAcount.tradingData.OpenPrice;
                    tradeAcount.tradingData.PositionMargin =
                        tradeAcount.tradingData.PositionValue / tradeAcount.tradingData.Leverage;
                    tradeAcount.tradingData.PercentageinTrade =
                        tradeAcount.tradingData.PositionMargin / tradeAcount.tradingData.Available;
                    tradeAcount.tradingData.DaysinPosition =
                        (int)(tradeAcount.tradingData.CloseDate - tradeAcount.tradingData.OpenDate).TotalDays;
                    tradeAcount.tradingData.Net_Profit_Loss =
                        (tradeAcount.tradingData.OpenPrice - tradeAcount.tradingData.ClosePrice) * tradeAcount.tradingData.Amount;
                    tradeAcount.tradingData.Net_Profit_Loss =
                        tradeAcount.tradingData.AvailableAfterPosition - tradeAcount.tradingData.Available;
                    tradeAcount.tradingData.Net_Portfo_Profit =
                        tradeAcount.tradingData.Net_Profit_Loss / tradeAcount.tradingData.Available;
                    tradeAcount.tradingData.GrossProfit_Loss =
                        (tradeAcount.tradingData.OpenPrice - tradeAcount.tradingData.ClosePrice) * tradeAcount.tradingData.Amount;
                    tradeAcount.tradingData.Cost =
                        tradeAcount.tradingData.AvailableAfterPosition - tradeAcount.tradingData.Net_Profit_Loss;
                    tradeAcount.tradingData.GrossProfit_Loss_Percentage =
                        tradeAcount.tradingData.AvailableAfterPosition / tradeAcount.tradingData.Available;

                    context.TradingDatas.Add(tradeAcount.tradingData);
                    context.SaveChanges();
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine( ex.ToString());
                }
            }
        }
    }
}
