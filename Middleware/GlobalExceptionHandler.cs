using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PoliNote.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // 500 error code
            int statusCode = StatusCodes.Status500InternalServerError;
            string title = "Internal Server Error";
            string detail = "An unexpected error occurred on the server.";

            // 400 validation error
            if (exception is ValidationException validationException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                title = "Validation Error";
                detail = validationException.Message;
            }

            httpContext.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
