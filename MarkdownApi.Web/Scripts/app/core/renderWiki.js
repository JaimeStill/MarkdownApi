(function () {
    var renderWiki = function () {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/render-wiki.html',
            scope: {
                wiki: '='
            }
        };
    };

    wikiApp.directive('renderWiki', renderWiki);
}());