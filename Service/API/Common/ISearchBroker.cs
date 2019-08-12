using System;
using System.Collections.Generic;
using System.Text;
using TNDStudios.Patterns.CQRS.Service.Searches;

namespace TNDStudios.Patterns.CQRS.Service
{
    public enum SearchState : Int16
    {
        Initialising = 0,
        Pending = 1,
        Failed = 2,
        Complete = 3
    }

    public enum SearchType : Int16
    {
        UK = 0,
        EU = 1,
        International = 2
    }

    public interface ISearchBroker
    {
        String StartSearch(SearchRequest request);
        List<SearchEntry> SearchStateList(String token);
        Boolean SetState(String token, SearchType searchType, SearchState state);
    }
}
