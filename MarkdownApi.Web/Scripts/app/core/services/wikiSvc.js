(function () {
    var wikiSvc = function ($http, $q, toastrSvc, utilitySvc) {
        var
            getWiki = function (id) {
                var deferred = $q.defer();

                $http({
                    url: '/api/wikis/getWiki/' + id,
                    method: 'POST'
                }).success(function (data) {
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Retrieving Wiki");
                    deferred.reject(err);
                })

                return deferred.promise;
            },
            addWiki = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/wikis/addWiki',
                    method: 'POST',
                    data: model
                }).success(function (data) {
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Adding " + model.name);
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            updateWiki = function (model) {
                var deferred = $q.defer;

                $http({
                    url: '/api/wikis/updateWiki',
                    method: 'POST',
                    data: model
                }).success(function () {
                    toastrSvc.alertSuccess(model.name + " successfully updated", "Update Wiki");
                    deferred.resolve();
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Updating " + model.name);
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            deleteWiki = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/wikis/deleteWiki',
                    method: 'POST',
                    data: model
                }).success(function () {
                    toastrSvc.alertSuccess(model.name + " successfully deleted", "Delete Wiki");
                    deferred.resolve();
                }).error(function () {
                    utilitySvc.toastErrorMessage(err, "Error Deleting " + model.name);
                    deferred.reject(err);
                });

                return deferred.promise;
            };

        return {
            getWiki: getWiki,
            addWiki: addWiki,
            updateWiki: updateWiki,
            deleteWiki: deleteWiki
        };
    };

    wikiSvc.$inject = ['$http', '$q', 'toastrSvc', 'utilitySvc'];
    wikiApp.factory('wikiSvc', wikiSvc);
}());