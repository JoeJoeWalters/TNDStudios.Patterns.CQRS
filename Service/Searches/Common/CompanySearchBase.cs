using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// Common functionality for individual search elements (the Azure functions 
    /// that go off and do the searches to the external services)
    /// </summary>
    public class CompanySearchBase
    {
        /// <summary>
        /// Locations of the blobs that will trigger the services (this would be a service bus topic
        /// with subscribers in a non-local emulated implementation)
        /// </summary>
        internal const String EUTriggerPath = "searches/eu/outstanding/{name}";
        internal const String UKTriggerPath = "searches/uk/outstanding/{name}";
        internal const String InternationalTriggerPath = "searches/international/outstanding/{name}";

        public virtual void Run(Stream myBlob, string name, ILogger log)
        {
            SearchRequest request = new SearchRequest() { };

            if (Process(request))
                return;

            throw new InvalidDataException();
        }

        /// <summary>
        /// Override so that each Azure Function can implement what happens when a search comes in without
        /// needing to know all the infrastructure involved.
        /// </summary>
        /// <param name="request">The request that came in and was processed by the Run method</param>
        /// <returns>If the process was successful</returns>
        public virtual Boolean Process(SearchRequest request)
        {
            // Some mocked up stuff here to prove a point
            Thread.Sleep(10000);

            return true;
        }
    }
}
