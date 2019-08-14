using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
using TNDStudios.Patterns.CQRS.Service.API;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// Mocked up search to pretend to search EU items
    /// </summary>
    public class EUCompanySearch : CompanySearchBase
    {
        /// <summary>
        /// Constructor to inject the broker and other items via DI then channel to base class
        /// </summary>
        /// <param name="broker"></param>
        public EUCompanySearch(ISearchBroker broker) : base(broker, SearchType.EU) { }

        [FunctionName("EUCompanySearch")]
        public override void Run([BlobTrigger(Constants.EUTriggerPath, Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
            => base.Run(myBlob, name, log);

        /// <summary>
        /// Override so that each Azure Function can implement what happens when a search comes in without
        /// needing to know all the infrastructure involved.
        /// </summary>
        /// <param name="request">The request that came in and was processed by the Run method</param>
        /// <returns>If the process was successful</returns>
        public override SearchResponse Process(SearchRequest request)
        {
            return base.Process(request);
        }
    }
}
