using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace func
{
    public class Echo
    {
        private readonly ILogger _logger;

        public Echo(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Echo>();
        }

        [Function("Echo")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            using var reader = new StreamReader(req.Body);
            string requestBody = await reader.ReadToEndAsync();
            await response.WriteStringAsync(requestBody);

            return response; 
        }
    }
}
