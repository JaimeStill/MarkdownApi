(function () {
    var manageWikis = function (wikiSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/manage-wikis.html',
            link: function (scope) {
                scope.model = wikiSvc.model;
                wikiSvc.getWikis();
            }
        };
    };

    manageWikis.$inject = ['wikiSvc'];
    wikiApp.directive('manageWikis', manageWikis);
}());