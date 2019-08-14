using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using TNDStudios.Patterns.CQRS.Service.Searches;

namespace TNDStudios.Patterns.CQRS.Service.API
{
    /// <summary>
    /// Implementation of the search broker where we cannot use the service bus
    /// and are relying on local emulated services
    /// </summary>
    public class LocalSearchBroker : ISearchBroker
    {
        // Local store for the incoming connection string via the constructor
        private String connectionString = String.Empty;

        // Local objects used to write data created during construction phase
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;
        CloudBlobClient blobClient;
        CloudBlobContainer blobContainer;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="connectionString">The connection string injected by the setup DI</param>
        public LocalSearchBroker(String connectionString)
        {
            // Store the connection string for use later if needed
            this.connectionString = connectionString;

            try
            {

                // Create the storage account linkage   
                if (CloudStorageAccount.TryParse(this.connectionString, out storageAccount))
                {
                    // Create a client to connect to the table
                    tableClient = storageAccount.CreateCloudTableClient();

                    // Make sure that the table actually exists
                    table = tableClient.GetTableReference("Searches");
                    if (!table.ExistsAsync().Result)
                    {
                        if (!table.CreateIfNotExistsAsync().Result)
                            throw new Exception("Could not create search table to store search tokens on initialisation");
                    }

                    // Create a client to write blobs (in place of a service bus later)
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    blobContainer = cloudBlobClient.GetContainerReference("searches");
                    if (!blobContainer.ExistsAsync().Result)
                    {
                        blobContainer.CreateAsync().GetAwaiter().GetResult();
                    }
                }
                else
                {
                    throw new Exception("Could not connect to the cloud storage account on initialisation");
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not initialise the search broker - '{ex.Message}'");
            }
        }

        /// <summary>
        /// Initialise the search and return a token to reference the search once completed
        /// </summary>
        /// <param name="request">The search request parameters</param>
        /// <returns>A token to reference the ongoing search</returns>
        public String StartSearch(SearchRequest request)
        {
            // Generate a new token to return to the caller so they can use it to find out the state of the searches
            String token = Guid.NewGuid().ToString();

            // Assign the token to the request so that when it is written to the service bus / trigger / blob
            // it has all the information the search element needss
            request.Token = token;

            // Add an entry for each search type for the partition of the token
            foreach (SearchType searchType in (SearchType[])Enum.GetValues(typeof(SearchType)))
            {
                // Write the trigger to kick off the search (Would be a service bus item but no local emulator for that)
#warning This should be a factory class, implement this later
                String blobFilename = String.Empty;
                switch (searchType)
                {
                    case SearchType.EU:
                        blobFilename = Constants.EUTriggerPath;
                        break;

                    case SearchType.International:
                        blobFilename = Constants.InternationalTriggerPath;
                        break;

                    case SearchType.UK:
                        blobFilename = Constants.UKTriggerPath;
                        break;
                }
                blobFilename = blobFilename.Replace("searches/", String.Empty).Replace("{name}", token);

                // Do the actual write
                CloudBlockBlob cloudBlockBlob = blobContainer.GetBlockBlobReference(blobFilename);
                cloudBlockBlob.UploadTextAsync(JsonConvert.SerializeObject(request));

                // Construct the table entry for this search and insert it in to the storage account so it 
                // can be retrieved by the token later
                SearchEntry searchEntry = new SearchEntry(token, searchType, SearchState.Initialising);
                TableOperation insertOp = TableOperation.Insert(searchEntry);
                TableResult result = table.ExecuteAsync(insertOp).Result;

                // Success but no content? If not then it failed and it should be reported back to the caller
                if (result.HttpStatusCode != (int)HttpStatusCode.NoContent)
                    throw new Exception($"Failed to intialise search for '{searchType.ToString()}' - Status Code {result.HttpStatusCode.ToString()}");                    
            }

            return token; // Send the token back
        }

        /// <summary>
        /// Returns a list of individual searhes that are ongoing for the overall search
        /// based on the token given
        /// </summary>
        /// <param name="token">The token given when the search was initialised</param>
        /// <returns>A list of the search elements and their state</returns>
        public List<SearchEntry> SearchStateList(String token)
        {
            // Prepare the results that we will be returning
            List<SearchEntry> results = new List<SearchEntry>();

            // Create the query to get just the results for this token
            TableQuery<SearchEntry> query = new TableQuery<SearchEntry>().Where
                (TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, token));
            
            // Get the results and build the response
            TableQuerySegment<SearchEntry> segment = null;
            while (segment == null || segment.ContinuationToken != null)
            {
                // Get the next segment of results to return (chunks of 100 so we limit the impact on the server)
                segment = table.ExecuteQuerySegmentedAsync(query.Take(100), segment?.ContinuationToken).Result;
                foreach (var entity in segment.Results)
                {
                    results.Add(entity);
                }
            }

            return results;
        }

        /// <summary>
        /// Set the state of the individual search as part of the overall search being performed
        /// </summary>
        /// <param name="token">The token that was given as part of the setup</param>
        /// <param name="searchType">The type of search element that is wanting to be modified</param>
        /// <param name="state">The new state of the search element</param>
        /// <returns>If the setting of the state was successful</returns>
        public Boolean SetState(String token, SearchType searchType, SearchState state)
        {
            return true;
        }
    }
}
