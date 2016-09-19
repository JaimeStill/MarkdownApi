(function () {
    var displayProjects = function (projectSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/display-projects.html',
            link: function (scope) {
                scope.model = projectSvc.model;
                projectSvc.getProjects();
            }
        };
    };

    displayProjects.$inject = ['projectSvc'];
    markdownApp.directive('displayProjects', displayProjects);
}());