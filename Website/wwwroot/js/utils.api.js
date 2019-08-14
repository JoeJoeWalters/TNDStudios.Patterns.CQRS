﻿var tndStudios = tndStudios || {};
tndStudios.utils = tndStudios.utils || {};
tndStudios.utils.api =
    {     
        // Make an API call with the given parameters
        call: function (url, method, request, callback) {

            // Start the progress spinner in 1/4 of a second just so it
            // doesn't flash too much if the response is really quick
            tndStudios.utils.ui.progress(true);

            // Make the ajax call
            $.ajax({
                type: method,
                url: url,
                data: JSON.stringify(request),
                crossDomain: false,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data, status, jqXHR) {

                    // Stop the progress spinner
                    tndStudios.utils.ui.progress(false);

                    // Success?
                    callback(data.success, data);
                },
                error: function (jgXHR, status) {

                    // Stop the progress spinner
                    tndStudios.utils.ui.progress(false);

                    // Do the failure calls
                    callback(false, { data: null, messsages: ["Error Connecting To Api"]});
                }
            });
        }
    };
