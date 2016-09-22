(function () {
    var dashboard = function (categorySvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/dashboard.html',
            link: function (scope) {
                scope.model = categorySvc.model;
                categorySvc.getCategories();
            }
        };
    };

    dashboard.$inject = ['categorySvc'];
    wikiApp.directive('dashboard', dashboard);
}());