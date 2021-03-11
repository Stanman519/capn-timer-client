using System;
using System.Net.Http;
using System.Threading.Tasks;
using RestEase;

namespace ScheduledCallerClient
{
    public interface ICapnAPI : IDisposable
    {
        [Get("transactions/2021")]
        Task<HttpResponseMessage> GetTransactions();
        
    }
    
    public interface IGMBotAPI : IDisposable
    {
        [Get("pendingTrades/2021")]
        Task<HttpResponseMessage> GetPendingTrades();

        [Get("tradeBait")]
        Task<HttpResponseMessage> GetTradeBait();
    }
}