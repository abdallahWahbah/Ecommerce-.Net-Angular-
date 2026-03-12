using System.Net;
using System.Text.Json;
using FluentValidation;

namespace ECommerce.API.Middleware
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
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(e => e.ErrorMessage);

                var response = new
                {
                    errors
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    error = "Internal Server Error"
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
