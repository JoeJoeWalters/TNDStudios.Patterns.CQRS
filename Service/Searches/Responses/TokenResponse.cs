using Newtonsoft.Json;
using System;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    [JsonObject]
    public class TokenResponse
    {
        [JsonProperty(PropertyName = "token", DefaultValueHandling = DefaultValueHandling.Populate)]
        public String Token { get; set; } = String.Empty;
    }
}
