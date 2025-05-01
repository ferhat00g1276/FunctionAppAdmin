using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppAdmin
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Admin, "get", "post")] HttpRequest req)
        {
            var name = req.Query["name"];
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult($"ADMIN : Welcome {name} to Azure Functions!");
        }
        [Function("Function2")]
        public IActionResult Run2([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            var name = req.Query["name"];

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult($"Function : Welcome {name} to Azure Functions!");
        }
        [Function("Function3")]
        public IActionResult Run3([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            var name = req.Query["name"];

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult($"Anonymous : Welcome {name} to Azure Functions!");
        }
    }
}
