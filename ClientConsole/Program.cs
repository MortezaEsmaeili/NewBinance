using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BinanceRestClient.SetDefaultOptions(options =>
            {
                options.ApiCredentials = new ApiCredentials("78KoVDEzrJM58vGxuz1nG6MZV3HnXRAHlQsfH9POskT5FrZa7204Oaa92jfK34ZL",
                "4jtBPUQyMloPBVLFGDljW90SboVIp4LziUEki6ycaV6dXQFomsSalpjAn36KHwGT");
            });
            BinanceSocketClient.SetDefaultOptions(options =>
            {
                options.ApiCredentials = new ApiCredentials("78KoVDEzrJM58vGxuz1nG6MZV3HnXRAHlQsfH9POskT5FrZa7204Oaa92jfK34ZL",
                "4jtBPUQyMloPBVLFGDljW90SboVIp4LziUEki6ycaV6dXQFomsSalpjAn36KHwGT");
            });

            using (var client = new BinanceRestClient())
            {
                var kandle = client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", KlineInterval.OneMinute);
                var socketClient = new BinanceSocketClient();
                _ = socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync("ETHUSDT", KlineInterval.OneMinute, data =>
                {
                    if (data.Data.Data.Final)
                        Console.WriteLine("Morteza.candleUpdate");
                });




                // Spot.Market | Spot market info endpoints
                var data = client.SpotApi.ExchangeData.GetBookPriceAsync("BTCUSDT");

                // Spot.Order | Spot order info endpoints
                WebCallResult<BinanceOrderBook> data2 =
                    await client.SpotApi.ExchangeData.GetOrderBookAsync("BTCUSDT");
                if (data2.ResponseStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Morteza_spot.order");

                    if (data2.Data.Bids.Count() > 0)
                        Console.Write(data2.Data.Bids.ToString());
                    Console.WriteLine($"BidsCount={data2.Data.Bids.Count()}");
                }
                WebCallResult<BinanceOrderBook> data3 =
                   await client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT");
                if (data3.ResponseStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.Write(data3.Data.ToString());
                }
                var data4 = await client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT");
                var data5 = await client.SpotApi.ExchangeData.GetCurrentAvgPriceAsync("ETHUSDT");

                Console.ReadLine();
            }
        }

        /*private void SubscribeToSpotUserStream()
        {
            var socketClient = new BinanceSocketClient();
            // Subscribe to a user stream
            var restClient = new BinanceClient();
            var listenKeyResult = restClient.Spot.UserStream.StartUserStream();
            var listenKeyResult = socketClient.SpotApi.ExchangeData.get
            if (!listenKeyResult.Success)
                throw new Exception("Failed to start user stream: " + listenKeyResult.Error);

            var successAccount = socketClient.Spot.SubscribeToUserDataUpdates(
                listenKeyResult.Data,
                data =>
                {
                    // Handle account info data
                    // Deprecated, will be removed in the future
                },
                data =>
                {
                    // Handle order update info data
                },
                null, // Handler for OCO updates
                null // Handler for position updates
                ); // Handler for account balance updates (withdrawals/deposits)
            Console.ReadLine();
        }*/
    }
    
}
