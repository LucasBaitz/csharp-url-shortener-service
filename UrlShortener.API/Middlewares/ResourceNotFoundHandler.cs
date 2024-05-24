using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain.Errors;

namespace UrlShortener.API.Middlewares
{
    internal sealed class ResourceNotFoundExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ResourceNotFoundExceptionHandler> _logger;

        public ResourceNotFoundExceptionHandler(ILogger<ResourceNotFoundExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ResourceNotFoundException notFoundException) return false;

            _logger.LogError($"Exception occurred: {exception.Message}");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Resource not found",
                Detail = exception.Message,
                Instance = httpContext.Request.Path,
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
