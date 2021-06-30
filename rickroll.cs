using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace Company.Function
{
    public static class rickroll
    {
        [FunctionName("rickroll")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string accountSid = Environment.GetEnvironmentVariable("ACCOUNTSID");
            string authToken = Environment.GetEnvironmentVariable("AUTHTOKEN");

            TwilioClient.Init(accountSid, authToken);

            var fromNumber = new PhoneNumber(Environment.GetEnvironmentVariable("FROMNUMBER"));
            var toNumber = new PhoneNumber(req.Query["toNumber"]);

            var call = CallResource.Create(
                to: toNumber,
                from: fromNumber,
                twiml: new Twiml("<Response><Play>https://demo.twilio.com/docs/classic.mp3</Play></Response>") 
            );

            string responseMessage = call.Sid;

            return new OkObjectResult(new {message = responseMessage});
        }
    }
}
