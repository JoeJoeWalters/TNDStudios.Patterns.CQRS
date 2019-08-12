using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TNDStudios.Patterns.CQRS.Service.Searches;

namespace TNDStudios.Patterns.CQRS.Service
{
    public class LocalSearchBroker : ISearchBroker
    {
        private String connectionString = String.Empty;

        public LocalSearchBroker(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public String StartSearch(SearchRequest request)
        {
            // Generate a new token to return to the caller so they can use it to find out the state of the searches
            String token = Guid.NewGuid().ToString();
            
            // Create the storage account linkage
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient client = storageAccount.CreateCloudTableClient();

            // Make sure that the table actually exists
            CloudTable table = client.GetTableReference("Searches");
            Boolean createResult = table.CreateIfNotExistsAsync().Result;

            // Add an entry for each search type for the partition of the token
            foreach (SearchType searchType in (SearchType[])Enum.GetValues(typeof(SearchType)))
            {
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

        public List<SearchEntry> SearchStateList(String token)
        {
            // Prepare the results that we will be returning
            List<SearchEntry> results = new List<SearchEntry>();

            // Create the storage account linkage
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient client = storageAccount.CreateCloudTableClient();

            // Make sure that the table actually exists
            CloudTable table = client.GetTableReference("Searches");
            Boolean createResult = table.CreateIfNotExistsAsync().Result;

            // Create the query to get just the results for this token
            TableQuery<SearchEntry> query = new TableQuery<SearchEntry>().Where
                (TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, token));
            
            // Get the results and build the response
            TableQuerySegment<SearchEntry> segment = null;
            while (segment == null || segment.ContinuationToken != null)
            {
                // Get the next segment of results to return
                segment = table.ExecuteQuerySegmentedAsync(query.Take(100), segment?.ContinuationToken).Result;
                foreach (var entity in segment.Results)
                {
                    results.Add(entity);
                }
            }

            return results;
        }

        public Boolean SetState(String token, SearchType searchType, SearchState state)
        {
            return true;
        }
    }
}
