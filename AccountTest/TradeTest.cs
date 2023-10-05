using Binance.BotState;

namespace AccountTest
{
    [TestClass]
    public class TradeTest
    {
        public List<string> logsList = new List<string>();
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            CandleDto candle = new CandleDto();
            candle.highPrice = 100;
            candle.lowPrice = 50;
            candle.openPrice = 90;
            candle.closePrice = 60;

            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerBuyPrice = 60;
            tradeBox.stopLossBuyPrice = 40;
            tradeBox.takeProfitBuyPrice = 120;
            tradeBox.upperBuyPrice = 80;

            Account account = new Account("Morteza", 10000, candle, tradeBox,"Buy");
            account.TrafficLogEvent += Account_TrafficLogEvent;

            //Act

            account.SetNewPrice(85);
            account.SetNewPrice(80);
            account.SetNewPrice(75);
            account.SetNewPrice(70);
            account.SetNewPrice(65);
            account.SetNewPrice(60);
            account.SetNewPrice(55);
            account.SetNewPrice(50);
            account.SetNewPrice(45);
            account.SetNewPrice(60);
            account.SetNewPrice(70);
            account.SetNewPrice(90);
            account.SetNewPrice(100);
            account.SetNewPrice(120);
            account.SetNewPrice(130);

            //Assert

            decimal actual = account.Balance;
            Assert.AreEqual(account.tradingData.AvailableAfterPosition, actual,
                13000, "Buy position worked correctly");
        }

        private void Account_TrafficLogEvent(string message)
        {
            logsList.Add(message);
            Console.WriteLine(message);
        }
    }
}