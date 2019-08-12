using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    [JsonObject]
    public class SearchResponse
    {
        [JsonProperty(PropertyName = "request", DefaultValueHandling = DefaultValueHandling.Populate)]
        public SearchRequest Request { get; set; }
    }
}
