using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExceptionHandler.Models;

namespace ExceptionHandler.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly object _lock = new object();


        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment _environment)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException || ex is UnAuthorizedException || ex is NotImplementedException || ex is SuccessWithErrorException || ex is BadRequestException)
                    throw;

                string id = Guid.NewGuid().ToString().Replace("-", "");
                await LogExceptionToFileAsync(ex, _environment, id, context.Request.Path);
                context.Response.Headers.Append("traceId", id);
                throw;
                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //context.Response.ContentType = "application/json";

                //var errorResponse = new { Message = $"An unexpected error occurred see the logs. traceId: {id}" };

                //await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));

            }
        }

        private async Task LogExceptionToFileAsync(Exception ex, IWebHostEnvironment _environment, string traceId, string path)
        {
            string logsDirectory = Path.Combine(_environment.WebRootPath, "ExceptionLogs");

            Directory.CreateDirectory(logsDirectory);

            var logFileName = $"Exception_{DateTime.Now:ddMMyyyy}.txt";
            var logFilePath = Path.Combine(logsDirectory, logFileName);

            var logMessage = @$"
Id: {traceId}
Endpoint: {path}
Exception: {ex.Message}
Inner Exception: {ex.InnerException?.Message}
DateTime: {DateTime.Now:G}";

            const int maxRetries = 3;
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    lock (_lock)
                    {
                        using (StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.UTF8))
                        {
                            writer.WriteLine(logMessage);
                        }
                    }
                    break;
                }
                catch (IOException)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
