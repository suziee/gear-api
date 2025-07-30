using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gear.Middleware
{
    //https://andrewlock.net/creating-a-custom-error-handler-middleware-function/
    public static class CustomErrorMiddleware
    {
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }

        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
        {
            return WriteResponse(httpContext, true);
        }

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
        {
            // might want to use false later
            return WriteResponse(httpContext, true);
        }

        private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            var feature = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = feature?.Error;

            if (ex == null)
            {
                return;
            }

            //no need, because kestrel already logs for you...
            //_logger.Error(ex, string.Empty);

            var problemDetails = new ProblemDetails
            {
                Status = 500,
                Title = ex.Message,
                Detail = ex.InnerException?.Message // ex.ToString() includes long stack trace that's not line separated, so kind of messy
            };

            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            // problemDetails.Extensions.Add(Constants.CorrelationIdKey, httpContext.Items[Constants.CorrelationIdKey]);

            var stream = httpContext.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problemDetails);
        }
    }
}
