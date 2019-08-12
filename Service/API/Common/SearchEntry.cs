using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Patterns.CQRS.Service
{
    public class SearchEntry : TableEntity
    {
        public int State { get; set; } // Int because enum's are not supported by table storage and are actively ignored
        public int Type { get; set; } // Int because enum's are not supported by table storage and are actively ignored
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public SearchEntry(String token, SearchType searchType, SearchState state)
        {
            DateTime now = DateTime.Now;

            PartitionKey = token;
            RowKey = ((int)searchType).ToString();
            State = (int)state;
            Type = (int)searchType;
            Created = now;
            Updated = now;
        }

        public SearchEntry() { }
    }
}
