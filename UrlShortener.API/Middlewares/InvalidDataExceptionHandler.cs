using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Domain.Errors;

namespace UrlShortener.API.Middlewares
{
    internal sealed class InvalidDataExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<InvalidDataExceptionHandler> _logger;

        public InvalidDataExceptionHandler(ILogger<InvalidDataExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NotValidDataException notValidDataException) return false;

            _logger.LogError($"Exception occurred: {exception.Message}");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "The provided data is not valid.",
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
