(function () {
    var renderMarkdown = function (markdownSvc, $sce) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/render-markdown.html',
            compile: function (element, attributes) {
                return {
                    pre: function (scope) {
                        scope.render = markdownSvc.render;

                        scope.$watch(function () { return scope.render.markdown; },
                            function (newValue) {
                                scope.render.html = $sce.trustAsHtml(markdownSvc.makeHtml(newValue));
                            });
                    },
                    post: function (scope, element) {
                        var renderer = element.find('div')[0];

                        scope.$watch(function () { return scope.render.html; },
                            function () {
                                scope.$evalAsync(prettyPrint(null, renderer));
                            });
                    }
                }
            }
        };
    };

    renderMarkdown.$inject = ['markdownSvc', '$sce'];
    markdownApp.directive('renderMarkdown', renderMarkdown);
}());