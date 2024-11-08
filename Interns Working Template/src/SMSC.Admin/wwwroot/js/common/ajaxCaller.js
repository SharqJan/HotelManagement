var ajaxHelpers = {

    ajaxCall: async function (httpMethod, url, data, type, completeCallback, header) {
        var ajaxFormSubmit = false;
        if (typeof formSubmitRequestForgery == 'undefined') {
            ajaxFormSubmit = false;
        } else {
            ajaxFormSubmit = formSubmitRequestForgery;
        }

        if (header === undefined) {
            return window.$.ajax({
                type: httpMethod.toUpperCase(),
                url: url,
                data: data,
                dataType: type,
                async: true,
                headers: { 'RequestVerificationToken': window.$('input[name="__RequestVerificationToken"]').val(), 'FormSubmitRequestForgery': ajaxFormSubmit }

            })
        }
        //used in case of autorefresh 
        else {
            return window.$.ajax({
                type: httpMethod.toUpperCase(),
                url: url,
                data: data,
                dataType: type,
                async: true,
                headers: { 'RequestVerificationToken': window.$('input[name="__RequestVerificationToken"]').val(), 'Header': header, 'FormSubmitRequestForgery': ajaxFormSubmit },
                complete: function (data, textStatus, request) {
                    completeCallback(header);
                }
            })
        }

    },

    fileUploadAjax: async function (httpMethod, url, data, processData, type, contentType) {

        var ajaxFormSubmit = false;
        if (typeof formSubmitRequestForgery == 'undefined') {
            ajaxFormSubmit = false;
        } else {
            ajaxFormSubmit = formSubmitRequestForgery;
        }

        return window.$.ajax({
            type: httpMethod.toUpperCase(),
            url: url,
            data: data,
            processData: processData,
            dataType: type,
            async: true,
            contentType: contentType,
            headers: { 'RequestVerificationToken': window.$('input[name="__RequestVerificationToken"]').val(), 'FormSubmitRequestForgery': ajaxFormSubmit }
        });
    },

    getQueryStringValue: function (parameterName) {
        const queryString = [];
        if (queryString.length === 0 && window.location.search.split("?").length > 1) {
            let params = window.location.search.split("?")[1].split("&");
            for (var i = 0; i < params.length; i++) {
                var key = params[i].split("=")[0];
                queryString[key] = decodeURIComponent(params[i].split("=")[1]);
            }
        } 
        if (queryString[parameterName] != null) { return queryString[parameterName]; } else { return ''; }
    },
  //  window.location.search.split('?')[1].split('&')[0].split('=')[1]  Umaid Testing
    parseAjaxErrorMessage: function (errorObject) {
        var errorMessage;
        switch (errorObject.status) {
            case 0:
                errorMessage = "Not Connected, Verify Network/Internet.";
                break;
            case 301:
                errorMessage = "The requested page has moved to a new url.";
                break;
            case 302:
                errorMessage = "The requested page has moved temporarily to a new url.";
                break;
            case 303:
                errorMessage = "The requested page can be found under a different url.";
                break;
            case 304:
                errorMessage = "This is the response code to an If-Modified-Since or If-None-Match header, where the URL has not been modified since the specified date.";
                break;
            case 305:
                errorMessage = "The requested URL must be accessed through the proxy mentioned in the Location header.";
                break;
            case 307:
                errorMessage = "The requested page has moved temporarily to a new url. ";
                break;
            case 400:
                errorMessage = "The server did not understand the request.";
                break;
            case 401:
                errorMessage = "The requested page needs a username and a password.";
                break;
            case 403:
                errorMessage = "Access is forbidden to the requested page/action.";
                break;
            case 404:
                errorMessage = "The server can not find the requested page.";
                break;
            case 405:
                errorMessage = "The method specified in the request is not allowed.";
                break;
            case 406:
                errorMessage = "The server can only generate a response that is not accepted by the client.";
                break;
            case 407:
                errorMessage = "You must authenticate with a proxy server before this request can be served.";
                break;
            case 408:
                errorMessage = "The request took longer than the server was prepared to wait.";
                break;
            case 409:
                errorMessage = "The request could not be completed because of a conflict.";
                break;
            case 410:
                errorMessage = "The requested page is no longer available.";
                break;
            case 411:
                errorMessage = "The Content - Length is not defined. The server will not accept the request without it.";
                break;
            case 412:
                errorMessage = "The pre condition given in the request evaluated to false by the server.";
                break;
            case 413:
                errorMessage = "The server will not accept the request, because the request entity is too large.";
                break;
            case 414:
                errorMessage = "The server will not accept the request, because the url is too long. Occurs when you convert a 'post' request to a 'get' request with a long query information.";
                break;
            case 415:
                errorMessage = "The server will not accept the request, because the media type is not supported.";
                break;
            case 416:
                errorMessage = "The requested byte range is not available and is out of bounds.";
                break;
            case 417:
                errorMessage = "The expectation given in an Expect request-header field could not be met by this server.";
                break;
            case 500:
                errorMessage = "The request was not completed. The server met an unexpected condition.";
                break;
            case 501:
                errorMessage = "The request was not completed. The server did not support the functionality required.";
                break;
            case 502:
                errorMessage = "The request was not completed. The server received an invalid response from the upstream server.";
                break;
            case 503:
                errorMessage = "The request was not completed. The server is temporarily overloading or down.";
                break;
            case 504:
                errorMessage = "The gateway has timed out.";
                break;
            case 505:
                errorMessage = "The server does not support the 'http protocol' version.";
                break;
            default:
                errorMessage = `Uncaught Error.\n ${errorObject.message}`;
        }
        window.Swal.fire({ icon: "error", title: "Error occurred during ajax request", text: errorMessage });
    }

}







