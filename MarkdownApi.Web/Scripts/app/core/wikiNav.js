(function () {
    var wikiNav = function (wikiSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/wiki-nav.html',
            scope: {},
            link: function (scope) {
                scope.isDashboard = false;
                scope.model = {
                    search: ''
                };

                if (window.location.pathname === '/' || window.location.pathname === '/Admin') {
                    scope.isDashboard = true;
                }

                scope.findWikis = function () {
                    if (scope.model.search === null || scope.model.search === undefined || scope.model.search === '') {
                        wikiSvc.getWikis();
                    } else {
                        wikiSvc.findWikis(scope.model.search);
                    }
                }
            }
        }
    };

    wikiNav.$inject = ['wikiSvc'];
    wikiApp.directive('wikiNav', wikiNav);
}());