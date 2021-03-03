using System;
using System.Diagnostics;
using System.Net.Http;
using RestEase;

namespace ScheduledCallerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ICapnAPI capnApi =
                RestClient.For<ICapnAPI>("https://mfl-capn.herokuapp.com/Mfl/");
            IGMBotAPI gmApi =
                RestClient.For<IGMBotAPI>("https://capn-crunch-gm-bot.herokuapp.com/Bot/");
            var time = DateTime.Now;
            
            var gmBotWatch = new Stopwatch();
            var transactionWatch = new Stopwatch();
            
            gmBotWatch.Start();
            transactionWatch.Start();

            while (true)
            {
                if (gmBotWatch.Elapsed.Days > 0)
                {
                    // CheckForTrades(gmApi);
                    try
                    {
                        var res = gmApi.GetPendingTrades().Result;
                        Console.WriteLine("called for trades");
                        gmBotWatch.Restart();
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.StatusCode);
                    }
                }

                if (transactionWatch.Elapsed.Minutes > 19)
                {
                    try
                    {
                        var res = capnApi.GetTransactions().Result;
                        Console.WriteLine("called for transactions");
                        transactionWatch.Restart();
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.StatusCode);
                    }
                }
            }
            
        }
    }
}