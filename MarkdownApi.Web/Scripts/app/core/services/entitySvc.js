(function () {
    var entitySvc = function ($http, $q, toastrSvc, utilitySvc) {
        var
            tagModel = {
                tags: []
            },
            documentModel = {
                documents: []
            },
            currentDocument = {
                document: {}
            },
            getTags = function () {
                var deferred = $q.defer();

                $http({
                    url: '/api/tags/getTags',
                    method: 'GET'
                }).success(function (data) {
                    tagModel.tags = data;
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error retrieving Tags");
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            addTag = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/',
                    method: 'POST',
                    data: model
                }).success(function () {
                    toastrSvc.alertSuccess(model.name + " successfully added");
                    deferred.resolve();
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error adding Tag");
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            updateTag = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/tags/updateTag',
                    method: 'POST',
                    data: model
                }).success(function () {
                    toastrSvc.alertSuccess(model.name + " successfully updated");
                    deferred.resolve();
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error updating Tag");
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            getDocuments = function () {
                var deferred = $q.defer();

                $http({
                    url: '/api/documents/getDocuments',
                    method: 'GET'
                }).success(function (data) {
                    documentModel.documents = data;
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error retrieving documents");
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
                    getDocuments();
                    getTags();
                    currentDocument.document = data;
                    toastrSvc.alertSuccess(model.name + " successfully added");
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error adding Document");
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
                }).success(function (data) {
                    getDocuments();
                    getTags();
                    currentDocument.document = data;
                    toastrSvc.alertSuccess(model.name + " successfully updated");
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error ");
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
                    getDocuments();
                    getTags();
                    currentDocument.document = {};
                    toastrSvc.alertSuccess(model.name + " successfully deleted");
                    deferred.resolve();
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error deleting Document");
                    deferred.reject(err);
                });

                return deferred.promise;
            };

        return {
            tagModel: tagModel,
            documentModel: documentModel,
            currentDocument: currentDocument,
            getTags: getTags,
            addTag: addTag,
            updateTag: updateTag,
            getDocuments: getDocuments,
            addDocument: addDocument,
            updateDocument: updateDocument,
            deleteDocument: deleteDocument
        };
    };

    entitySvc.$inject = ['$http', '$q', 'toastrSvc', 'utilitySvc'];
    markdownApp.factory('entitySvc', entitySvc);
}());