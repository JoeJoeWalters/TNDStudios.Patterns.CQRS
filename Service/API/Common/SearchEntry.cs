using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace TNDStudios.Patterns.CQRS.Service.API
{
    /// <summary>
    /// Implementation of the token / record entry that records each element of a search and it's state
    /// </summary>
    public class SearchEntry : TableEntity
    {
        public int State { get; set; } // Int because enum's are not supported by table storage and are actively ignored
        public int Type { get; set; } // Int because enum's are not supported by table storage and are actively ignored
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        /// <summary>
        /// Set up a new search entry for the table
        /// </summary>
        /// <param name="token">The token given when the search was initialised</param>
        /// <param name="searchType">The search element that is being created (essentially the external API and it's Azure functin being called)</param>
        /// <param name="state">The state of the search element</param>
        public SearchEntry(String token, SearchType searchType, SearchState state)
        {
            DateTime now = DateTime.Now;

            PartitionKey = token; // Partition by the token so we can easily see groups of search elements
            RowKey = ((int)searchType).ToString(); // Row key can be the search type as it is partitioned
            State = (int)state;
            Type = (int)searchType;
            Created = now; // Default to right now (as it is being created)
            Updated = now; // Default to right now (as it is being created)
        }

        /// <summary>
        /// Empty constructor needed for instantiation by the service
        /// </summary>
        public SearchEntry() { }
    }
}
