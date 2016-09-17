(function () {
    var markdownEditor = function (markdownSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/markdown-editor.html',
            link: function (scope) {
                scope.render = markdownSvc.render;
            }
        }
    };

    markdownEditor.$inject = ['markdownSvc'];
    markdownApp.directive('markdownEditor', markdownEditor);
}());