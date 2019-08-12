using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    public class UKCompanySearch : CompanySearchBase
    {
        [FunctionName("UKCompanySearch")]
        public override void Run([BlobTrigger(UKTriggerPath, Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
            => base.Run(myBlob, name, log);

        public override Boolean Process(SearchRequest request)
        {
            return base.Process(request);
        }
    }
}
