using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using RestEase;

namespace ScheduledCallerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // ICapnAPI capnApi =
            //     RestClient.For<ICapnAPI>("https://mfl-capn.herokuapp.com/Mfl/");
            // IGMBotAPI gmApi =
            //     RestClient.For<IGMBotAPI>("https://capn-crunch-gm-bot.herokuapp.com/Bot/");
            var time = DateTime.Now;
            Console.WriteLine("Yo, WE OUT HERE!");
            
            var gmBotWatch = new Stopwatch();
            var transactionWatch = new Stopwatch();
            var tradeBaitWatch = new Stopwatch();

            gmBotWatch.Start();
            transactionWatch.Start();
            tradeBaitWatch.Start();

            while (true)
            {
                if (gmBotWatch.Elapsed.Days > 0)
                {
                    // CheckForTrades(gmApi);
                    try
                    {
                        using IGMBotAPI gmApi = RestClient.For<IGMBotAPI>("https://capn-crunch-gm-bot.herokuapp.com/Bot/");
                        var res = gmApi.GetPendingTrades().Result;
                        if(res.IsSuccessStatusCode) Console.WriteLine($"{DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("en-US"))} called for trades");
                        gmBotWatch.Restart();
                    }
                    catch (HttpRequestException e)
                    { Console.WriteLine(e.StatusCode); }
                }

                if (transactionWatch.Elapsed.Minutes > 19)
                {
                    try
                    {
                        using ICapnAPI capnApi = RestClient.For<ICapnAPI>("https://mfl-capn.herokuapp.com/Mfl/");
                        var res = capnApi.GetTransactions().Result;
                        if(res.IsSuccessStatusCode) Console.WriteLine($"{DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("en-US"))} called for transactions");
                        transactionWatch.Restart();
                    }
                    catch (HttpRequestException e)
                    { Console.WriteLine(e.StatusCode); }
                }

                if (tradeBaitWatch.Elapsed.Days > 0)
                {
                    try
                    {
                        using IGMBotAPI gmApi = RestClient.For<IGMBotAPI>("https://capn-crunch-gm-bot.herokuapp.com/Bot/");
                        var res = gmApi.GetTradeBait().Result;
                        if(res.IsSuccessStatusCode) Console.WriteLine($"{DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("en-US"))} called for trade bait");
                        tradeBaitWatch.Restart();
                    }
                    catch (HttpRequestException e)
                    { Console.WriteLine(e.StatusCode); }
                }
            }
            
        }
    }
}