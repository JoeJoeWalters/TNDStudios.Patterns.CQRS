using System;
using System.Collections.Generic;
using TNDStudios.Patterns.CQRS.Service.Searches;

namespace TNDStudios.Patterns.CQRS.Service.API
{
    /// <summary>
    /// The state of the individual searches as part of the overall search
    /// </summary>
    public enum SearchState : Int16
    {
        Initialising = 0, // Search has been requested and starting
        Pending = 1, // Search has been initialised and fired off but no state is back from external service yet
        Failed = 2, // The search element failed for some reason
        Complete = 3 // The search completed successfully
    }

    /// <summary>
    /// The type of individual search that is running and will be monitored
    /// </summary>
    public enum SearchType : Int16
    {
        UK = 0,
        EU = 1,
        International = 2
    }

    /// <summary>
    /// Interface for the search broker that will be injected on startup
    /// </summary>
    public interface ISearchBroker
    {
        /// <summary>
        /// Initialise the search and return a token to reference the search once completed
        /// </summary>
        /// <param name="request">The search request parameters</param>
        /// <returns>A token to reference the ongoing search</returns>
        String StartSearch(SearchRequest request);

        /// <summary>
        /// Returns a list of individual searhes that are ongoing for the overall search
        /// based on the token given
        /// </summary>
        /// <param name="token">The token given when the search was initialised</param>
        /// <returns>A list of the search elements and their state</returns>
        List<SearchEntry> SearchStateList(String token);

        /// <summary>
        /// Set the state of the individual search as part of the overall search being performed
        /// </summary>
        /// <param name="token">The token that was given as part of the setup</param>
        /// <param name="searchType">The type of search element that is wanting to be modified</param>
        /// <param name="state">The new state of the search element</param>
        /// <returns>If the setting of the state was successful</returns>
        Boolean SetState(String token, SearchType searchType, SearchState state);
    }
}
