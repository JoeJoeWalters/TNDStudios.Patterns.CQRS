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
            app.page.currentToken = "";
            app.page.currentSearch.clear();
        },

        // kick off the search
        startSearch: function () {

            // Clear the search
            app.page.currentToken = "";
            app.page.currentSearch.clear();
            tndStudios.models.search.start({ token: null, fromPrice: 0.0, toPrice: 0.0 }, this.startCallback); // Start the search
        },

        // Callback to load the item
        startCallback: function (success, data) {

            if (success) {

            }
            else {

            }
        },
    }
});

app.load();