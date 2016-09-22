(function () {
    var renderDocument = function () {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/render-document.html',
            scope: {
                document: '='
            }
        };
    };

    wikiApp.directive('renderDocument', renderDocument);
}());