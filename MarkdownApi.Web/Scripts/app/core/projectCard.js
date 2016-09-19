(function () {
    var projectCard = function () {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/project-card.html',
            scope: {
                project: '='
            },
            link: function (scope) {
                scope.viewProject = function () {
                    window.location.href = '/Product/' + scope.project.id;
                }
            }
        };
    };

    markdownApp.directive('projectCard', projectCard);
}());