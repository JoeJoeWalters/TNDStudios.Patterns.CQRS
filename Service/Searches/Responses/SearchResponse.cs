using Newtonsoft.Json;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// Standard response for when a search is being queried
    /// </summary>
    [JsonObject]
    public class SearchResponse
    {
        [JsonProperty(PropertyName = "request", DefaultValueHandling = DefaultValueHandling.Populate)]
        public SearchRequest Request { get; set; }
    }
}
