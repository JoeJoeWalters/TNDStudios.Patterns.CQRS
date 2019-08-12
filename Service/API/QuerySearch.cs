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
using System.Collections.Generic;

namespace Service
{
    public class QuerySearch
    {
        private ISearchBroker broker = null;

        public QuerySearch(ISearchBroker broker)
        {
            this.broker = broker;
        }

        [FunctionName("QuerySearch")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "search/{token?}")] HttpRequest req,
            String token,
            ILogger log)
        {
            log.LogInformation("Checking Status Of Search.");

            if ((token ?? String.Empty) == String.Empty)
                throw new Exception("No token specified");

            try
            {
                List<SearchEntry> data = broker.SearchStateList(token);
                return new OkObjectResult(data);
            }
            catch(Exception ex)
            {
                return new ObjectResult(ex.Message) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
        }
    }
}
