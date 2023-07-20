using Binance.Net;
using Binance.Net.Objects.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("78KoVDEzrJM58vGxuz1nG6MZV3HnXRAHlQsfH9POskT5FrZa7204Oaa92jfK34ZL", "4jtBPUQyMloPBVLFGDljW90SboVIp4LziUEki6ycaV6dXQFomsSalpjAn36KHwGT"),
                /*LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }*/
            });
            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials("78KoVDEzrJM58vGxuz1nG6MZV3HnXRAHlQsfH9POskT5FrZa7204Oaa92jfK34ZL", "4jtBPUQyMloPBVLFGDljW90SboVIp4LziUEki6ycaV6dXQFomsSalpjAn36KHwGT"),
                /*LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }*/
            });
        }

        private void Test_BTN_Click(object sender, EventArgs e)
        {
            using (var client = new BinanceClient())
            {
                WebCallResult<Binance.Net.Objects.Spot.MarketData.BinanceOrderBook> data3 = client.Spot.Market.GetOrderBook("ETHUSDT");
                if (data3.ResponseStatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show(data3.Data.ToString());
                }
            }
        }
    }
}
