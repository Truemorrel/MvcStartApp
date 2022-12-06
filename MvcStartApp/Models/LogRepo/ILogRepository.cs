using System.Threading.Tasks;

namespace MvcStartApp.Models.LogRepo
{
    public interface ILogRepository
    {
        Task SendLog(Request msg);
        Task<Request[]> GetLogs();
    }
}
