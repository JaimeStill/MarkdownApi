(function () {
    var projectSvc = function ($http, $q, toastrSvc, utilitySvc) {
        var
            model = {
                projects: []
            },
            getProjects = function () {
                var deferred = $q.defer();

                $http({
                    url: '/api/projects/getProjects',
                    method: 'GET'
                }).success(function (data) {
                    model.projects = data;
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Retrieving Projects");
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            getProject = function (id) {
                var deferred = $q.defer();

                $http({
                    url: '/api/projects/getProject/' + id,
                    method: 'GET'
                }).success(function (data) {
                    deferred.resolve(data);
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Retrieving Project");
                    deferred.reject(err);
                })

                return deferred.promise;
            },
            addProject = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/projects/addProject',
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
            updateProject = function (model) {
                var deferred = $q.defer;

                $http({
                    url: '/api/projects/updateProject',
                    method: 'POST',
                    data: model
                }).success(function () {
                    toastrSvc.alertSuccess(model.name + " successfully updated", "Update Project");
                    deferred.resolve();
                }).error(function (err) {
                    utilitySvc.toastErrorMessage(err, "Error Updating " + model.name);
                    deferred.reject(err);
                });

                return deferred.promise;
            },
            deleteProject = function (model) {
                var deferred = $q.defer();

                $http({
                    url: '/api/projects/deleteProject',
                    method: 'POST',
                    data: model
                }).success(function () {
                    toastrSvc.alertSuccess(model.name + " successfully deleted", "Delete Project");
                    deferred.resolve();
                }).error(function () {
                    utilitySvc.toastErrorMessage(err, "Error Deleting " + model.name);
                    deferred.reject(err);
                });

                return deferred.promise;
            };

        return {
            model: model,
            getProjects: getProjects,
            getProject: getProject,
            addProject: addProject,
            updateProject: updateProject,
            deleteProject: deleteProject
        };
    };

    projectSvc.$inject = ['$http', '$q', 'toastrSvc', 'utilitySvc'];
    markdownApp.factory('projectSvc', projectSvc);
}());