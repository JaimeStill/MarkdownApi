(function () {
    var addProject = function (projectSvc, toastrSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/manage-project.html',
            scope: {
                projectId: '='
            },
            link: function (scope) {
                if (scope.projectId > 0) {
                    projectSvc.getProject(scope.projectId).then(function (data) {
                        scope.project = data;
                    });
                }

                scope.saveProject = function () {
                    if (validate(scope.project)) {
                        if (scope.project.id > 0) {
                            projectSvc.updateProject(scope.project);
                        } else {
                            projectSvc.addProject(scope.project).then(function (data) {
                                scope.project = data;
                            });
                        }
                    }
                }

                scope.deleteProject = function () {
                    projectSvc.deleteProject(scope.project).then(function () {
                        window.location.href = "/";
                    });
                }

                function validate(project) {
                    if (project.name === null || project.name === undefined || project.name === '') {
                        toastrSvc.alertError("Name must have a value", "Invalid Project");
                        return false;
                    }

                    if (project.description === null || project.description === undefined || project.description === '') {
                        toastrSvc.alertError("Description must have a value", "Invalid Project");
                        return false;
                    }

                    return true;
                }
            }
        }
    };

    addProject.$inject = ['projectSvc', 'toastrSvc'];
    markdownApp.directive('addProject', addProject);
}());