using Newtonsoft.Json;
using System;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// Request to start a search
    /// </summary>
    [JsonObject]
    public class SearchRequest
    {
        [JsonProperty(PropertyName = "token", DefaultValueHandling = DefaultValueHandling.Populate)]
        public String Token { get; set; } = String.Empty;

        [JsonProperty(PropertyName = "priceFrom", DefaultValueHandling = DefaultValueHandling.Populate)]
        public Decimal PriceFrom { get; set; } = (Decimal)0.0;

        [JsonProperty(PropertyName = "priceTo", DefaultValueHandling = DefaultValueHandling.Populate)]
        public Decimal PriceTo { get; set; } = (Decimal)0.0;
    }
}
