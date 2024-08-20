using Portfolio.Management.Domain.Dtos.Response;
using Portfolio.Management.Domain.Enums;
using System.Net;

namespace Portfolio.Management.API.Middleware
{
    public class ErrorHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(context);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            var response = new ErrorResponse(ErrorCode.GenericError, "Ocorreu um erro interno no servidor.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }
}
