using System.Net.Http;
using System.Threading.Tasks;
using RestEase;

namespace ScheduledCallerClient
{
    public interface ICapnAPI
    {
        [Get("transactions/2021")]
        Task<HttpResponseMessage> GetTransactions();
        
    }
    
    public interface IGMBotAPI
    {
        [Get("pendingTrades/2021")]
        Task<HttpResponseMessage> GetPendingTrades();
        
    }
}