using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TNDStudios.Patterns.CQRS.Service.Searches;
using System.Net;
using TNDStudios.Patterns.CQRS.Service;

namespace Service
{
    public class StartSearch
    {
        private ISearchBroker broker = null;

        public StartSearch(ISearchBroker broker)
        {
            this.broker = broker;
        }

        [FunctionName("StartSearch")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "search")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Starting Search");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SearchRequest request = JsonConvert.DeserializeObject<SearchRequest>(requestBody);

            try
            {
                String token = broker.StartSearch(request);
                return new OkObjectResult(token);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
        }
    }
}
