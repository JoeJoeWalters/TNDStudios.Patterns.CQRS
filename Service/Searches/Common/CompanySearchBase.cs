using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    public class CompanySearchBase
    {
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

        public virtual Boolean Process(SearchRequest request)
        {
            // Some mocked up stuff here to prove a point
            Thread.Sleep(10000);

            return true;
        }
    }
}
