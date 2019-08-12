using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TNDStudios.Patterns.CQRS.Service;

namespace Service
{
    /// <summary>
    /// Endpoint for checking the state of the search
    /// </summary>
    public class QuerySearch
    {
        /// <summary>
        /// The broker implementation that was decided on at startup by DI
        /// </summary>
        private ISearchBroker broker = null;

        /// <summary>
        /// Default constructor with dependency injected broker
        /// </summary>
        /// <param name="broker">The implementation of the broker to use</param>
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
            // Always log that the function kicked off
            log.LogInformation("Checking Status Of Search.");

            // Make sure that a token has been provided otherwise return an error to the caller
            if ((token ?? String.Empty) == String.Empty)
            {
                String errorMessage = "No token has been provided";
                log.LogError(errorMessage);
                return new BadRequestObjectResult(errorMessage);
            }

            try
            {
                // Get the state of this search from the broker implementation
                List<SearchEntry> data = broker.SearchStateList(token);
                return new OkObjectResult(data); // Return the data to the caller
            }
            catch(Exception ex)
            {
                // The broker reported an error, return this to the caller
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
