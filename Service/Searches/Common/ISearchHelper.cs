using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    public interface ISearchHelper
    {
        SearchRequest GetRequest(Stream blob);
    }
}
