using Domain.Exceptions;
using FluentValidation;
using Shared.Common;

namespace PetroNexus.Middleware
{
    public class GlobalErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleWare> _logger;

        public GlobalErrorHandlingMiddleWare(
            RequestDelegate next,
            ILogger<GlobalErrorHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                // Handle 404 endpoints
                if (context.Response.StatusCode == StatusCodes.Status404NotFound
                    && !context.Response.HasStarted)
                {
                    await HandleNotFoundEndpointAsync(context);
                }
            }
            catch (Exception ex)
            {
                // Log full error internally
                _logger.LogError(ex, "Unhandled exception occurred");

                await HandleErrorAsync(context, ex);
            }
        }

        private static async Task HandleErrorAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                NotFoundExceptions => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            context.Response.StatusCode = statusCode;

            var message = ex switch
            {
                NotFoundExceptions => "Entity not found.",
                BadRequestException => "Validation failed.",
                UnAuthorizedException => "Unauthorized access.",
                ValidationException => "Validation failed.",
                _ => "Internal server error."
            };

            object response;

            if (ex is ValidationException validationEx)
            {
                var errors = validationEx.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage }).ToList();
                response = new
                {
                    success = false,
                    message,
                    errors
                };
            }
            else
            {
                response = ApiResponse.FailureResult(message, ex.Message);
            }

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleNotFoundEndpointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            var response = ApiResponse.FailureResult("Endpoint not found.", "The requested endpoint does not exist.");

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
