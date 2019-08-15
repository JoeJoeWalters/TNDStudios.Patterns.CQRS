using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading;
using TNDStudios.Patterns.CQRS.Service.API;

namespace TNDStudios.Patterns.CQRS.Service.Searches
{
    /// <summary>
    /// Common functionality for individual search elements (the Azure functions 
    /// that go off and do the searches to the external services), probably best
    /// to use Durable Functions here to avoid costs due to waiting for the external APIs
    /// </summary>
    public class CompanySearchBase
    {
        /// <summary>
        /// The ibject search broker to use as passed in by the parent search implementation
        /// </summary>
        internal ISearchBroker broker = null;
        internal SearchType searchType = SearchType.Unknown;

        // Faked constants for pretending the system is running an external task
        private const Int32 MaxProcessingTime = 10000;
        private const Int32 MinProcessingTime = 5000;
        private const Double ChanceOfFailure = 0.05; // Double because Random is double

        /// <summary>
        /// Base constructor which the searches will channel their dependency injected items to
        /// </summary>
        /// <param name="broker">The prefered search broker to use</param>
        public CompanySearchBase(ISearchBroker broker, SearchType searchType)
        {
            this.broker = broker;
            this.searchType = searchType;
        }

        public virtual void Run(Stream stream, string name, ILogger log)
        {
            // Create a nulled request to start to check for failure
            SearchRequest request = null;
            
            // Decode the data stream to get the request
            try
            {
                // Deserialise the stream (no matter where it is from)
                using (StreamReader reader = new StreamReader(stream))
                using (JsonTextReader jsonReader = new JsonTextReader(reader))
                {
                    JsonSerializer jsonSer = new JsonSerializer();
                    request = jsonSer.Deserialize<SearchRequest>(jsonReader);
                }

                // Check that the request deserialises ok and there was a token to process
                if (request != null && (request.Token ?? String.Empty) != String.Empty)
                {
                    // Set the state to initialising
                    broker.SetState(request.Token, searchType, SearchState.Pending);

                    // Send the request to the search processor (which is part of the common base class too or an override)
                    SearchResponse response = Process(request);
                    if (response.Success)
                    {
                        broker.SetState(request.Token, searchType, SearchState.Complete);
                        return;
                    }
                }
                else
                    log.LogError("Request was empty or request contained no token");

                // If we get to here then all other options have been exhausted and we will throw and error
                // and as it is untrapped it will tell the system to try again, no need to log the error here as it will automatically log anyway
                broker.SetState(request.Token, searchType, SearchState.Failed);
                throw new Exception("Could not process request, Forcing system to try again.");
            }
            catch(Exception ex)
            {
                broker.SetState(request.Token, searchType, SearchState.Failed);
#warning Taken out for testing, In reality it would be handled by Azure retry mechanism
                //throw new Exception($"Could not process request - '{ex.Message}'");
            }
        }

        /// <summary>
        /// Override so that each Azure Function can implement what happens when a search comes in without
        /// needing to know all the infrastructure involved.
        /// </summary>
        /// <param name="request">The request that came in and was processed by the Run method</param>
        /// <returns>If the process was successful</returns>
        public virtual SearchResponse Process(SearchRequest request)
        {
            Int32 randomProcessingTime = MinProcessingTime + (Int32)((new Random()).NextDouble() * (MaxProcessingTime - MinProcessingTime));

            // Some mocked up stuff here to prove a point
            Thread.Sleep(randomProcessingTime);

            // Return a mocked random success or fail value to the caller
            return new SearchResponse()
            {
                Request = request,
                Success = !((new Random()).NextDouble() < ChanceOfFailure)
            };
        }
    }
}
