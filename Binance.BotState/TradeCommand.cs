using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.BotState
{
    public enum CommandType
    {
        Buy,
        Sell
    }
    public class TradeCommand
    {
        public CommandType command;
        public TradeState nextState;
    }
}
