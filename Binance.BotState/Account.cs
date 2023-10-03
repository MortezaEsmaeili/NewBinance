using System;
using System.Collections.Generic;
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
        PositionClosed
    }
    public class Account
    {
        private TradeState state;
        private readonly string CoinName;
        private decimal balance;
        private CandleDto candle;
        private TradeBox tradeBox;
        private TradeBoundry tradeBoundry;
        // Constructor
        public Account(string coinName, decimal available, CandleDto candleDto, TradeBox tradeBox)
        {
            this.CoinName = coinName;
            this.state = TradeState.Initial;
            this.balance = available;
            this.candle = candleDto;
            this.tradeBox = tradeBox;
            tradeBoundry = new TradeBoundry(candleDto);
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
            balance = +amount;
        }
        private bool Withdraw(decimal amount)
        {
            if (balance < amount)
                return false;
            balance =- amount;
            return true;
        }
        public bool OpenBuyPosition()
        {
            return false;
        }
        public bool OpenSellPosition()
        {
            return false;
        }

        public void SetNewPrice(decimal price)
        {
            switch(state)
            {
                case State.Initial:
                    TradeCommand tradeCommand = tradeBoundry.CheckState(price);
                    if (tradeCommand != null)
                        ExecuteCommand(tradeCommand);
                    break;
                case State.OpenBuyPosition:
                    break;
                case State.OpenSellPosition:
                    break;
                case State.PositionClosed: 
                    break;
            }
        }

        private void ExecuteCommand(TradeCommand tradeCommand)
        {
            throw new NotImplementedException();
        }
    }
}
