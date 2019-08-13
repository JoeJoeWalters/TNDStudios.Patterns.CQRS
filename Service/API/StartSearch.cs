using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using TNDStudios.Patterns.CQRS.Service;
using TNDStudios.Patterns.CQRS.Service.Searches;

namespace Service
{
    /// <summary>
    /// Kick off the search and retrieve a token to use later
    /// </summary>
    public class StartSearch
    {
        /// <summary>
        /// The broker implementation that was decided on at startup by DI
        /// </summary>
        private ISearchBroker broker = null;

        /// <summary>
        /// Default constructor with dependency injected broker
        /// </summary>
        /// <param name="broker">The implementation of the broker to use</param>
        public StartSearch(ISearchBroker broker)
        {
            this.broker = broker;
        }

        [FunctionName("StartSearch")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "search")] HttpRequest req,
            ILogger log)
        {
            // Log that the search has started
            log.LogInformation("Starting Search");

            // Get the payload from the body of the request by deserialising it
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SearchRequest request = JsonConvert.DeserializeObject<SearchRequest>(requestBody);

            try
            {
                // Ask the broker to start the search with the given payload
                // and send the resulting token back to the caller
                return new OkObjectResult(
                    new TokenResponse()
                    {
                        Token = broker.StartSearch(request)
                    });
            }
            catch (Exception ex)
            {
                // The broker returned a failure state, return this to the caller
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
