(function () {
    var wikiNav = function (categorySvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/wiki-nav.html',
            scope: {},
            link: function (scope) {
                scope.isDashboard = false;
                scope.model = {
                    categorySearch: '',
                    wikiSearch: '',
                    documentSearch: ''
                };

                if (window.location.pathname === '/' || window.location.pathname === '/Admin') {
                    scope.isDashboard = true;
                }

                scope.findCategories = function () {
                    if (scope.model.categorySearch === null || scope.model.categorySearch === undefined || scope.model.categorySearch === '') {
                        categorySvc.getCategories();
                    } else {
                        categorySvc.findCategories(scope.model.categorySearch);
                    }
                }

                scope.findWikis = function () {
                    if (scope.model.wikiSearch === null || scope.model.wikiSearch === undefined || scope.model.wikiSearch === '') {
                        categorySvc.getCategories();
                    } else {
                        categorySvc.findWikis(scope.model.wikiSearch);
                    }
                }

                scope.findDocuments = function () {
                    if (scope.model.documentSearch === null || scope.model.documentSearch === undefined || scope.model.documentSearch === '') {
                        categorySvc.getCategories();
                    } else {
                        categorySvc.findDocuments(scope.model.documentSearch);
                    }
                }
            }
        }
    };

    wikiNav.$inject = ['categorySvc'];
    wikiApp.directive('wikiNav', wikiNav);
}());