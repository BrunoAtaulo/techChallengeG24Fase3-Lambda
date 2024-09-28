using AWS_lambda_Auth.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace AWS_lambda_Auth.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ClientException ex)
            {
                await HandleClientExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(
            HttpContext context, Exception exception)
        {
            var response = new { message = exception.Message };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static Task HandleClientExceptionAsync(
            HttpContext context, ClientException Clientexception)
        {
            var response = new
            {
                message = Clientexception.Message,
                erros = Clientexception.Errors
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
