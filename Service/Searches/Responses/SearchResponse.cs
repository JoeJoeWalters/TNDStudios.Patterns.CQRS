using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// Standard response for when a search is being queried
    /// </summary>
    [JsonObject]
    public class SearchResponse
    {
        /// <summary>
        /// The origional request that was fulfilled (or not)
        /// </summary>
        [JsonProperty(PropertyName = "request", DefaultValueHandling = DefaultValueHandling.Populate)]
        public SearchRequest Request { get; set; } = new SearchRequest() { };

        /// <summary>
        /// Did the search succeed? Not if it got any results
        /// </summary>
        [JsonProperty(PropertyName = "success", DefaultValueHandling = DefaultValueHandling.Populate)]
        public Boolean Success { get; set; } = false;

        /// <summary>
        /// Any issues that arose should be logged here as part of the response from the external system
        /// </summary>
        [JsonProperty(PropertyName = "exceptions", DefaultValueHandling = DefaultValueHandling.Populate)]
        public List<String> Exceptions { get; set; } = new List<string>();
    }
}
