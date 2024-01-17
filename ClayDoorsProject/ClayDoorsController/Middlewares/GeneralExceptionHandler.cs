using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace ClayDoorsController.Middlewares
{
    /// <summary>
    /// Handler responsible to catch all unhandled exceptions in the application and prevent to return all information.
    /// </summary>
    public class GeneralExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                Message = "An error occurred during the request.",
            };

            var json = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(json);

            return true;
        }
    }
}
