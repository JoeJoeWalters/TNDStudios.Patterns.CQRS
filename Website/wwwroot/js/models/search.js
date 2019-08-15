var tndStudios = tndStudios || {};
tndStudios.models = tndStudios.models || {};
tndStudios.models.search =
    {
        // Search Page Model
        page: function () {

            // The properties of the object            
            this.currentToken = ""; // The token we currently are working with
            this.currentSearch = new tndStudios.models.search.search(null); // The search object
        },

        // Package Model
        search: function (data) {

            // The properties of the object
            this.searchArray = [];

            // Copy the content of this search from search json package
            this.fromObject = function (fromObject) {

                // Clear the object first (just in case)
                this.clear();

                // Start copying the data from the other object
                this.searchArray = [];
            }

            // Clear this search object (i.e. make it ready for editing)
            this.clear = function () {

                // Clear the properties
                this.searchArray = [];
            }

            // Any data passed in?
            if (data) {
                this.fromObject(data); // Assign the data to this object
            }
        },

        // The the api call to kick off the search
        start: function (searchObject, callback) {
            tndStudios.utils.api.call(
                'http://localhost:7071/api/search',
                'POST',
                searchObject,
                callback);
        },

        // The api call to get the status of the search
        status: function (token, callback) {

            // The the api call to load the provider types
            tndStudios.utils.api.call(
                'http://localhost:7071/api/search/' + token,
                'GET',
                null,
                callback
            );

        }
    };