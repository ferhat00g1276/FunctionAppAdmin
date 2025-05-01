using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FunctionAppAdmin
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        public class UserData
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }
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

        [Function("Function4")]
        public async Task<IActionResult> Run4(
     [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonSerializer.Deserialize<UserData>(body);

            if (data == null || string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.Email))
            {
                return new BadRequestObjectResult("Missing 'name' or 'email'");
            }

            var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("ferhat00g1276@gmail.com", "uqkz lmsk dfhc mef");

            client.EnableSsl = true;
            var msg = new MailMessage("ferhat00g1276@gmail.com", data.Email, "Hi!", "hi " + data.Name);
            client.Send(msg);

            return new OkResult();
        }


    }
}
