using Newtonsoft.Json;
using System;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    [JsonObject]
    public class SearchRequest
    {
        [JsonProperty(PropertyName = "priceFrom", DefaultValueHandling = DefaultValueHandling.Populate)]
        public Decimal PriceFrom { get; set; }

        [JsonProperty(PropertyName = "priceTo", DefaultValueHandling = DefaultValueHandling.Populate)]
        public Decimal PriceTo { get; set; }
    }
}
