using System.IO;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// Search helper for the Azure search functions
    /// </summary>
    public class SearchHelper : ISearchHelper
    {
        /// <summary>
        /// Get the payload of a request from the incoming stream (could be a service bus message or a blob etc.)
        /// </summary>
        /// <param name="blob">The stream from the service that kicked off the process</param>
        /// <returns>The search request pulled from the stream</returns>
        public SearchRequest GetRequest(Stream stream)
        {
            return new SearchRequest() { };
        }
    }
}
