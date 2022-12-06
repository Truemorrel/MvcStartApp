using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using MvcStartApp.Models.LogRepo;

namespace MvcStartApp.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogRepository _logRepo;

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, ILogRepository logRepo)
        {
            _next = next;
            _logRepo = logRepo;
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");

            var log = new Request() { Url = $"http://{context.Request.Host.Value + context.Request.Path}" };
            await _logRepo.SendLog(log);

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}
