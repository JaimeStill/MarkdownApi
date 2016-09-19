(function () {
    var documentSvc = function ($http, $q, toastrSvc, utilitySvc) {
        var
            model = {
                documents: []
            },
            getDocuments = function (id) {
                var deferred = $q.defer();

                $http({
                    url: '/api/documents/getDocuments/' + id,
                    method: 'POST'
                }).success(function (data) {
                    model.documents = data;
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Retrieving Documents");
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            getDocument = function (id) {
                var deferred = $q.defer();

                $http({
                    url: '/api/documents/getDocument/' + id,
                    method: 'POST'
                }).success(function (data) {
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Retrieving Document");
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            addDocument = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/documents/addDocument',
                    method: 'POST',
                    data: model
                }).success(function (data) {
                    toastrSvc.alertSuccess(model.title + " successfully added", "Add Document");
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Adding Document");
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            updateDocument = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/documents/updateDocument',
                    method: 'POST',
                    data: model
                }).success(function () {
                    toastrSvc.alertSuccess(model.title + " successfully updated", "Update Document");
                    deferred.resolve();
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Updating Document");
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            deleteDocument = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/documents/deleteDocument',
                    method: 'POST',
                    data: model
                }).success(function () {
                    toastrSvc.alertSuccess(model.title + " successfully deleted", "Delete Document");
                    deferred.resolve();
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Deleting Document");
                    deferred.reject(err);
                });

                return deferred.promise;
            };

        return {
            model: model,
            getDocuments: getDocuments,
            getDocument: getDocument,
            addDocument: addDocument,
            updateDocument: updateDocument,
            deleteDocument: deleteDocument
        };
    };

    documentSvc.$inject = ['$http', '$q', 'toastrSvc', 'utilitySvc'];
    wikiApp.factory('documentSvc', documentSvc);
}());