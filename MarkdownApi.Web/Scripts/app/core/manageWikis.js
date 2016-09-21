(function () {
    var manageWikis = function (categorySvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/manage-wikis.html',
            link: function (scope) {
                scope.model = categorySvc.model;
                categorySvc.getCategories();
            }
        };
    };

    manageWikis.$inject = ['categorySvc'];
    wikiApp.directive('manageWikis', manageWikis);
}());