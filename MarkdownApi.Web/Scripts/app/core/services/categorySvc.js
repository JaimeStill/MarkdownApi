(function () {
    var categorySvc = function ($http, $q, toastrSvc, utilitySvc) {
        var
            model = {
                categories: [],
                loading: true,
                emptyMessage: ''
            },
            getCategories = function () {
                var deferred = $q.defer();
                model.loading = true;

                $http({
                    url: '/api/categories/getCategories',
                    method: 'GET'
                }).success(function (data) {
                    if (data.length < 1) {
                        model.emptyMessage = 'No wikis have been created yet...'
                    }
                    model.categories = data;
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Retrieving Categories");
                    deferred.reject(err);
                }).finally(function () {
                    model.loading = false;
                });

                return deferred.promise;
            },
            renameCategory = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/categories/renameCategory',
                    method: 'POST',
                    data: model
                }).success(function (data) {
                    toastrSvc.alertSuccess(model.name + " successfully renamed", "Rename Category");
                    if (data) {
                        getCategories();
                    }
                    deferred.resolve();
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Renaming Category");
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            findCategories = function (categories) {
                var deferred = $q.defer();
                model.loading = true;

                $http({
                    url: '/api/filters/findCategories?categories=' + categories,
                    method: 'POST'
                }).success(function (data) {
                    if (data.length < 1) {
                        model.emptyMessage = 'No categories have a name that contains ' + categories;
                    }
                    model.categories = data;
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Finding Categories");
                    deferred.reject(err);
                }).finally(function () {
                    model.loading = false;
                });

                return deferred.promise;
            },
            findWikis = function (wikis) {
                var deferred = $q.defer();
                model.loading = true;

                $http({
                    url: '/api/filters/findWikis?wikis=' + wikis,
                    method: 'POST'
                }).success(function (data) {
                    if (data.length < 1) {
                        model.emptyMessage = 'No wikis have a name that contains ' + wikis;
                    }
                    model.categories = data;
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Finding Wikis");
                    deferred.reject(err);
                }).finally(function () {
                    model.loading = false;
                });

                return deferred.promise;
            },
            findDocuments = function (documents) {
                var deferred = $q.defer();
                model.loading = true;

                $http({
                    url: '/api/filters/findDocuments?documents=' + documents,
                    method: 'POST'
                }).success(function (data) {
                    if (data.length < 1) {
                        model.emptyMessage = 'No documents have a title that contains ' + documents;
                    }
                    model.categories = data;
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Finding Documents");
                    deferred.reject(err);
                }).finally(function () {
                    model.loading = false;
                });

                return deferred.promise;
            };

        return {
            model: model,
            getCategories: getCategories,
            renameCategory: renameCategory,
            findCategories: findCategories,
            findWikis: findWikis,
            findDocuments: findDocuments
        };
    };

    categorySvc.$inject = ['$http', '$q', 'toastrSvc', 'utilitySvc'];
    wikiApp.factory('categorySvc', categorySvc);
}());