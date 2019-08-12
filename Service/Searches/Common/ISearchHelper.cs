using System.IO;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// Interface to define a search helper for the Azure functions
    /// </summary>
    public interface ISearchHelper
    {
        /// <summary>
        /// Get the payload of a request from the incoming stream (could be a service bus message or a blob etc.)
        /// </summary>
        /// <param name="blob">The stream from the service that kicked off the process</param>
        /// <returns>The search request pulled from the stream</returns>
        SearchRequest GetRequest(Stream stream);
    }
}
