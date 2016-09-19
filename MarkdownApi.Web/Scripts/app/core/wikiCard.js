(function () {
    var wikiCard = function () {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/wiki-card.html',
            scope: {
                wiki: '=',
                wikiLink: '@'
            }
        };
    };

    wikiApp.directive('wikiCard', wikiCard);
}());