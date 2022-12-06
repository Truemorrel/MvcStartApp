using Microsoft.EntityFrameworkCore;
using MvcStartApp.Models.DB;
using System;
using System.Threading.Tasks;

namespace MvcStartApp.Models.LogRepo
{
    public class LogRepository : ILogRepository
    {
        private readonly LogContext _context;
        public async Task<Request[]> GetLogs()
        {
            return await _context.Requests.ToArrayAsync();
        }

        public async Task SendLog(Request msg)
        {
            msg.Id = Guid.NewGuid();
            msg.Date = DateTime.Now;
            var entry = _context.Entry(msg);
            if (entry.State == EntityState.Detached)
            {
                await _context.Requests.AddAsync(msg);
            }

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
        public LogRepository(LogContext context)
        {
            _context = context;
        }
    }
}
