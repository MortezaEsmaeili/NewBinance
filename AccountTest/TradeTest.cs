using Binance.BotState;

namespace AccountTest
{
    [TestClass]
    public class TradeTest
    {
        public List<string> logsList = new List<string>();
        
        [TestMethod]
        public void TestMethodBuy_Profit()
        {
            // Arrange
       /*     CandleDto candle = new CandleDto();
            candle.highPrice = 100;
            candle.lowPrice = 50;
            candle.openPrice = 90;
            candle.closePrice = 60;*/

            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerBuyPrice = 60;
            tradeBox.stopLossBuyPrice = 40;
            tradeBox.takeProfitBuyPrice = 100;
            tradeBox.upperBuyPrice = 80;

            Account account = new Account("CEO", 10000, tradeBox,"Buy");
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
        public void TestMethodSell_StopLoss()
        {
            // Arrange
      /*      CandleDto candle = new CandleDto();
            candle.highPrice = 100;
            candle.lowPrice = 50;
            candle.openPrice = 90;
            candle.closePrice = 60;*/

            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerSellPrice = 60;
            tradeBox.stopLossSellPrice = 100;
            tradeBox.takeProfitSellPrice = 40;
            tradeBox.upperSellPrice = 80;

            Account account = new Account("Morteza", 10000, tradeBox, "Sell");
            account.TrafficLogEvent += Account_TrafficLogEvent;
            var prices = new decimal[] {55, 50, 45, 60, 70, 80, 100, 120, 130, 110, 90, 80, 60, 40, 30 };
            //Act
            foreach (var price in prices)
                account.SetNewPrice(price);

            //Assert
            decimal actual = account.Balance;
            Assert.AreEqual(account.tradingData.AvailableAfterPosition, actual, "Buy position worked correctly");
        }
        [TestMethod]
        public void TestMethodBuy_StopLoss()
        {
            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerBuyPrice = 60;
            tradeBox.stopLossBuyPrice = 40;
            tradeBox.takeProfitBuyPrice = 100;
            tradeBox.upperBuyPrice = 80;

            Account account = new Account("CEO", 10000, tradeBox, "Buy");
            account.TrafficLogEvent += Account_TrafficLogEvent;
            var prices = new decimal[] { 85, 80, 75, 70, 65, 60, 55, 50, 45, 42, 40, 38, 41, 43, 47 };
            //Act
            foreach (var price in prices)
                account.SetNewPrice(price);

            //Assert
            decimal actual = account.Balance;
            Assert.AreEqual(account.tradingData.AvailableAfterPosition, actual, "Buy position worked correctly");
        }
        [TestMethod]
        public void TestMethodSell_TakeProfit()
        {
            // Arrange
            /*      CandleDto candle = new CandleDto();
                  candle.highPrice = 100;
                  candle.lowPrice = 50;
                  candle.openPrice = 90;
                  candle.closePrice = 60;*/

            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerSellPrice = 60;
            tradeBox.stopLossSellPrice = 100;
            tradeBox.takeProfitSellPrice = 40;
            tradeBox.upperSellPrice = 80;

            Account account = new Account("Morteza", 10000, tradeBox, "Sell");
            account.TrafficLogEvent += Account_TrafficLogEvent;
            var prices = new decimal[] { 55, 50, 45, 60, 70, 80, 90, 87, 83, 76, 64, 53, 48, 40, 30 };
            //Act
            foreach (var price in prices)
                account.SetNewPrice(price);

            //Assert
            decimal actual = account.Balance;
            Assert.AreEqual(account.tradingData.AvailableAfterPosition, actual, "Sell position worked correctly");
        }
        [TestMethod]
        public void TestMethodSell_StopLoss2()
        {
            // Arrange

            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerSellPrice = (decimal)21.8;
            tradeBox.stopLossSellPrice = (decimal)22.91;
            tradeBox.takeProfitSellPrice = (decimal)21.05;
            tradeBox.upperSellPrice = (decimal)22.50;

            Account account = new Account("Morteza", 10000, tradeBox, "Sell");
            account.TrafficLogEvent += Account_TrafficLogEvent;
            var prices = new double[] { 21.60, 21.63, 21.7, 21.81, 21.85, 21.97, 22.1,
                21.95, 22.10, 22.33, 22.70, 22.83, 22.90, 22.95, 23.01 };
            //Act
            
            foreach (var price in prices)
                account.SetNewPrice((decimal)price);

            //Assert
            decimal actual = account.Balance;
            Assert.AreEqual(account.tradingData.AvailableAfterPosition, actual, "Sell position worked correctly");
        }
        
        [TestMethod]
        public void TestMethodSell_StopLoss3()
        {
            // Arrange

            TradeBox tradeBox = new TradeBox();
            tradeBox.lowerSellPrice = (decimal)24.10;
            tradeBox.stopLossSellPrice = (decimal)24.30;
            tradeBox.takeProfitSellPrice = (decimal)23.90;
            tradeBox.upperSellPrice = (decimal)24.25;

            Account account = new Account("Morteza", 10000, tradeBox, "Sell");
            account.TrafficLogEvent += Account_TrafficLogEvent;
            var prices = new double[] { 21.60, 22.90, 23.70, 24.09, 24.20, 24.27, 24.7,
                24.01, 22.10, 22.33, 22.70, 22.83, 22.90, 22.95, 23.01 };
            //Act

            foreach (var price in prices)
                account.SetNewPrice((decimal)price);

            //Assert
            decimal actual = account.Balance;
            Assert.AreEqual(account.tradingData.AvailableAfterPosition, actual, "Sell position worked correctly");
        }
    }
}