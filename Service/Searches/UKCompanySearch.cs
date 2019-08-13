using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// Mocked up search to pretend to search UK items
    /// </summary>
    public class UKCompanySearch : CompanySearchBase
    {
        [FunctionName("UKCompanySearch")]
        public override void Run([BlobTrigger(UKTriggerPath, Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
            => base.Run(myBlob, name, log);

        /// <summary>
        /// Override so that each Azure Function can implement what happens when a search comes in without
        /// needing to know all the infrastructure involved.
        /// </summary>
        /// <param name="request">The request that came in and was processed by the Run method</param>
        /// <returns>If the process was successful</returns>
        public override Boolean Process(SearchRequest request)
        {
            return base.Process(request);
        }
    }
}
