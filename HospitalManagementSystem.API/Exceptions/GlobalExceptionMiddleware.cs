using System.Net;
using System.Text.Json;

namespace HospitalManagementSystem.API.Exceptions

{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = exception.Message
            };

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
