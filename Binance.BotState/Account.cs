﻿using Binance.Dal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.BotState
{
    public enum TradeState
    {
        Initial,
        OpenBuyPosition,
        OpenSellPosition,
        WaitForTakeProfitOrStopLoss,
        PositionClosed
    }
    public class Account
    {
        private TradeState state;
        public readonly string CoinName;
        private decimal balance;
    //    private CandleDto candle;
        public TradeBox tradeBox;
        public TradeBoundry tradeBoundry;
        private decimal coinCount;
        public TradingData tradingData;
        private decimal available;
        private decimal price;
        private decimal Trade_Sum = 0;
        // Constructor
        public Account(string coinName, decimal _available/*, CandleDto candleDto*/, TradeBox tradeBox, string position)
        {
            this.CoinName = coinName;
            coinCount = 0;
            this.state = TradeState.Initial;
            this.balance = available;
            this.available = _available;
   //         this.candle = candleDto;
            this.tradeBox = tradeBox;
            tradeBoundry = new TradeBoundry(tradeBox);
            tradingData = new TradingData();
            tradingData.Leverage = 3;
            tradingData.Available = available;
            tradingData.CoinName = coinName;
            tradingData.OpenPrice = 0;
            tradingData.Position = position;
            tradingData.Amount = 0;
            Trade_Sum = 0;
            Deposit(available);
        }
        public decimal Balance
        {
            get { return balance; }
        }
        public TradeState State
        {
            get { return state; }
        }
        private void Deposit(decimal amount)
        {
            balance += amount;
        }
        private bool Withdraw(decimal amount)
        {
            if (balance < amount)
                return false;
            balance -= amount;
            return true;
        }
        public void StopTrade()
        {
            SendLog("Trade was stoped by command.");
            if ((state == TradeState.OpenSellPosition || state == TradeState.WaitForTakeProfitOrStopLoss) &&
                tradingData.Position == "Sell")
                CloseSellPosition(price);
            if((state == TradeState.OpenBuyPosition || state == TradeState.WaitForTakeProfitOrStopLoss) &&
                tradingData.Position == "Buy")
                CloseBuyPosition(price);
        }
        public void SetNewPrice(decimal price)
        {
            this.price = price;
            switch (state)
            {
                case TradeState.Initial:
                case TradeState.OpenBuyPosition:
                case TradeState.OpenSellPosition:
                    TradeCommand tradeCommand = tradeBoundry.CheckState(price, tradingData.Position);
                    if (tradeCommand != null)
                        ExecuteCommand(tradeCommand, price);
                    break;
                case TradeState.PositionClosed:
                    break;
            }
            CheckStopLoss(price);
            CheckTakeProfit(price);
        }

        private void CheckTakeProfit(decimal price)
        {
            if((state == TradeState.OpenBuyPosition || state == TradeState.WaitForTakeProfitOrStopLoss) &&
                tradingData.Position == "Buy")
            {
                if(price >= tradeBox.takeProfitBuyPrice)
                {
                    SendLog($"Take Profit was reached in Buy Position for {CoinName} at {DateTime.Now} ");
                    CloseBuyPosition(price);
                }
            }
            if((state == TradeState.OpenSellPosition || state == TradeState.WaitForTakeProfitOrStopLoss) &&
                tradingData.Position == "Sell")

            {
                if (price <= tradeBox.takeProfitSellPrice)
                {
                    SendLog($"Take Profit was reached in Sell Position for {CoinName} at {DateTime.Now} ");
                    CloseSellPosition(price);
                }
            }
        }

        private void CloseSellPosition(decimal price)
        {
            SendLog($"Sell Position Closed for {CoinName} at price {price} at {DateTime.Now}");
            tradingData.OpenPrice = -( Trade_Sum / coinCount);
            tradingData.CloseDate = DateTime.Now;
            tradingData.ClosePrice = price;
            Deposit(coinCount * price);
            tradingData.AvailableAfterPosition = Balance;
            tradingData.Amount = -1*coinCount;
            state = TradeState.PositionClosed;
            SendLog($"Available After Position for {CoinName} is {Balance}");
        }

        private void CloseBuyPosition(decimal price)
        {
            SendLog($"Buy Position Closed for {CoinName} at price {price} at {DateTime.Now}");
            tradingData.OpenPrice = Trade_Sum / coinCount;
            tradingData.CloseDate = DateTime.Now;
            tradingData.ClosePrice = price;
            Deposit(coinCount * price);
            tradingData.AvailableAfterPosition = Balance;
            tradingData.Amount = coinCount;
            state = TradeState.PositionClosed;
            SendLog($"Available After Position for {CoinName} is {Balance}");
        }

        private void CheckStopLoss(decimal price)
        {
            if ((state == TradeState.OpenBuyPosition || state == TradeState.WaitForTakeProfitOrStopLoss)
                && tradingData.Position == "Buy")
            {
                if (price <= tradeBox.stopLossBuyPrice)
                {
                    SendLog($"Stop Loss was reached in Buy Position for {CoinName} at {DateTime.Now} ");
                    CloseBuyPosition(price);
                }
            }
            if ((state == TradeState.OpenSellPosition || state == TradeState.WaitForTakeProfitOrStopLoss) &&
                tradingData.Position == "Sell")
            {
                if (price >= tradeBox.stopLossSellPrice)
                {
                    SendLog($"Stop Loss was reached in Sell Position for {CoinName} at {DateTime.Now} ");
                    CloseSellPosition(price);
                }
            }
        }

        private void ExecuteCommand(TradeCommand tradeCommand, decimal price)
        {
            decimal amount = available / 5;

            if (tradeCommand.command == CommandType.Buy && tradingData.Position == "Buy")
            {
                if (Withdraw(amount))
                {
                    if (state == TradeState.Initial)
                    {
                        tradingData.OpenDate = DateTime.Now;
                        tradingData.OpenPrice = price;
                    }
                    SendLog($"Buy {amount} from {CoinName} at {DateTime.Now} with price: {price}");
                    state = tradeCommand.nextState;
                    coinCount += amount / price;
                    Trade_Sum += amount;
                    SetNewPrice(price);
                }
                else
                {
                    SendLog($"Cant Buy {CoinName} because of Lack of funds at {DateTime.Now}");
                    state = TradeState.WaitForTakeProfitOrStopLoss;
                }
            }
            if (tradeCommand.command == CommandType.Sell && tradingData.Position == "Sell")
            {
                if (state == TradeState.Initial)
                {
                    tradingData.OpenDate = DateTime.Now;
                    tradingData.OpenPrice = price;
                }
                Deposit(amount);
                SendLog($"Sell {amount} from {CoinName} at {DateTime.Now} with price: {price}");
                state = tradeCommand.nextState;
                coinCount -= amount / price;
                Trade_Sum += amount;
                SetNewPrice(price);
            }
        }
        public delegate  void TrafficLog(string message);
        public event TrafficLog TrafficLogEvent;        
            
        private void SendLog(string log)
        {
            TrafficLogEvent?.Invoke(log);
        }
    
    }
}
