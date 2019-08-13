using Newtonsoft.Json;
using System;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// A response for then a search has been started and a token is needed to be sent back to the caller
    /// </summary>
    [JsonObject]
    public class TokenResponse
    {
        [JsonProperty(PropertyName = "token", DefaultValueHandling = DefaultValueHandling.Populate)]
        public String Token { get; set; } = String.Empty;
    }
}
