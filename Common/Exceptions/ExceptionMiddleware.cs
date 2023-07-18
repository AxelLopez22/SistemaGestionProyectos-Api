using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Api_ProjectManagement.Common.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = GetStatusCode(exception);
            context.Response.StatusCode = response.StatusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                statusCode = context.Response.StatusCode,
                mensaje = response.Mensaje
            }));
        }

        private (int StatusCode, string Mensaje) GetStatusCode(Exception ex)
        {
            int StatusCode = 500;
            // string Mensaje = "Internal Server Error.";
            string Mensaje = $"Internal Server Error. {ex.Message}";

            if (ex is HttpStatusException httpStatusEx)
            {
                StatusCode = (int)httpStatusEx.StatusCode;
                Mensaje = httpStatusEx.Message;
            }
            else if (ex.InnerException is HttpStatusException httpStatusEx2)
            {
                StatusCode = (int)httpStatusEx2.StatusCode;
                Mensaje = httpStatusEx2.Message;
            }
            else if (ex is NotImplementedException)
            {
                StatusCode = StatusCodes.Status501NotImplemented;
                Mensaje = "Funcionalidad no implementada";
            }
            else if (ex is DbUpdateException)
            {
                StatusCode = StatusCodes.Status400BadRequest;
                Mensaje = $"Ocurrió un error al intentar guardar en la DB. {ex.Message}";
            }
            return (StatusCode, Mensaje);
        }
    }
}
