using LoggingService;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CompanyEmployees
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILoggerManager _logger;
        public GlobalExceptionHandler(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            _logger.LogError($"Something went wrong: {exception.Message}");
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "An error occurred",
                Status = httpContext.Response.StatusCode,
                Detail = "Internal Server Error.",
                Type = exception.GetType().Name
            }, cancellationToken);

            return true;
        }
    }
}
