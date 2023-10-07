using Binance.BotState;

namespace AccountTest
{
    [TestClass]
    public class TradeTest
    {
        public List<string> logsList = new List<string>();
        
        [TestMethod]
        public void TestMethodBuy()
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
            var prices = new decimal[] { 85, 80, 75, 70, 65, 60, 55, 50, 45, 60, 70, 90, 100, 120, 130 };
            //Act
            foreach (var price in prices)
                account.SetNewPrice(price);

            //Assert
            decimal actual = account.Balance;
            Assert.AreEqual(account.tradingData.AvailableAfterPosition, actual, "Buy position worked correctly");
        }

        private void Account_TrafficLogEvent(string message)
        {
            logsList.Add(message);
            Console.WriteLine(message);
        }

        [TestMethod]
        public void TestMethodSell()
        {
            // Arrange
            CandleDto candle = new CandleDto();
            candle.highPrice = 100;
            candle.lowPrice = 50;
            candle.openPrice = 90;
            candle.closePrice = 60;

            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerSellPrice = 60;
            tradeBox.stopLossSellPrice = 140;
            tradeBox.takeProfitSellPrice = 40;
            tradeBox.upperSellPrice = 80;

            Account account = new Account("Morteza", 10000, candle, tradeBox, "Sell");
            account.TrafficLogEvent += Account_TrafficLogEvent;
            var prices = new decimal[] {55, 50, 45, 60, 70, 80, 100, 120, 130, 110, 90, 80, 60, 40, 30 };
            //Act
            foreach (var price in prices)
                account.SetNewPrice(price);

            //Assert
            decimal actual = account.Balance;
            Assert.AreEqual(account.tradingData.AvailableAfterPosition, actual, "Buy position worked correctly");
        }

    }
}