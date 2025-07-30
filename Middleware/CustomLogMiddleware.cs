using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Api.Gear.Middleware
{
    //https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/
    //https://josef.codes/asp-net-core-6-http-logging-log-requests-responses/
    //https://stackoverflow.com/questions/52946747/how-to-get-httprequest-body-in-net-core
    public class CustomLogMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public CustomLogMiddleware(RequestDelegate next, ILogger<CustomLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.ToLower().Contains("healthcheck"))
            {
                await _next(context);
                return;
            }

            _logger.LogInformation("Incoming request. Path: {@path}, Method: {@method}", context.Request.Path.Value, context.Request.Method);

            await Log(context.Request);

            await _next(context);

            Log(context.Response);
        }

        private async Task Log(HttpRequest request)
        {
            request.EnableBuffering();
            
            if (request.ContentLength == null ||
                !(request.ContentLength > 0) ||
                !request.Body.CanSeek)
            {
                return;
            }

            request.Body.Seek(0, SeekOrigin.Begin);

            string body;
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, (int)request.ContentLength, true))
            {
                body = await reader.ReadToEndAsync();
            }

            request.Body.Position = 0;

            // not necessary, but indentation from postman requests
            // makes the log file look more consistent (hopefully easier to read)
            var obj = JsonConvert.DeserializeObject(body);
            body = JsonConvert.SerializeObject(obj, Formatting.None);

            _logger.LogInformation("Request = {@body}", body);
        }

        private void Log(HttpResponse response)
        {
            // not logging response body because don't have a good way to filter GetAll responses

            // if (response.StatusCode == StatusCodes.Status200OK)
            // {
            //     return;
            // }

            _logger.LogInformation("Response status code = {@code}", response.StatusCode);
        }
    }
}
