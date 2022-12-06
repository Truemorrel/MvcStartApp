using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcStartApp.Models.DB;
using MvcStartApp.Models.LogRepo;
using System.Threading.Tasks;

namespace MvcStartApp.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogRepository _logRepo;
        private readonly ILogger<LogsController> _logger;

        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            var logs = await _logRepo.GetLogs();
            return View(logs);
        }

        public LogsController(ILogger<LogsController> logger, ILogRepository repo)
        {
            _logger = logger;
            _logRepo = repo;
        }
    }
}
