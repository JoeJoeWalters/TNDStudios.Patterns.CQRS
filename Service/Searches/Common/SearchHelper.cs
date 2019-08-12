using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    public class SearchHelper : ISearchHelper
    {
        public SearchRequest GetRequest(Stream blob)
        {
            return new SearchRequest() { };
        }
    }
}
