var app = new Vue({
    el: '#contentcontainer',
    data: {
        page: new tndStudios.models.search.page()
    },
    computed: {

    },
    watch: {

    },
    methods: {

        load: function () {
            app.page.currentToken = "No Token";
            app.page.currentSearch.clear();
        },

        // kick off the search
        startSearch: function () {

            // Clear the search
            app.page.currentToken = "No Token";
            app.page.currentSearch.clear();
            tndStudios.models.search.start({ token: null, fromPrice: 0.0, toPrice: 0.0 }, this.startSearchCallback); // Start the search
        },

        // Callback to load the item
        startSearchCallback: function (success, data) {

            if (success) {
                if (data) {
                    app.page.currentToken = data.token;

                    // Immediatly query the search so we can display the results
                    this.querySearch();
                }
                else
                    alert("No token was retrieved");
            }
            else {
                alert("No token was retrieved");
            }
        },

        querySearch: function () {
            tndStudios.models.search.status(this.page.currentToken, this.querySearchCallback); // Start the search
        },

        querySearchCallback: function (success, data) {

            if (success) {
                if (data) {
                    var doRefresh = false;
                    $.each(data,
                        function (index, item)
                        {
                            if (item.state == 0 || item.state == 1)
                                doRefresh = true;
                        });

                    // Still some items not completed?
                    if (doRefresh) {

                        // Tell the system to check again in a second (SignalR probably is better but for now .. )
                        setTimeout(this.querySearch, 1000);
                    }
                    else
                        alert("Search Finished");
                }
            }
        }
    }
});

app.load();