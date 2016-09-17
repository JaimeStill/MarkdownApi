(function () {
    var utilitySvc = function (toastrSvc) {
        toastErrorMessage = function (err, message) {
            if (err.ExceptionMessage) {
                toastrSvc.alertError(err.ExceptionMessage, message);
            } else if (err.Message) {
                toastrSvc.alertError(err.Message, message);
            } else if (err) {
                toastrSvc.alertError(err, message);
            } else {
                toastrSvc.alertError("Unspecified Error Interacting with PowerShell API", message);
            }
        };

        return {
            toastErrorMessage: toastErrorMessage
        };
    };

    utilitySvc.$inject = ['toastrSvc'];
    markdownApp.factory('utilitySvc', utilitySvc);
}());