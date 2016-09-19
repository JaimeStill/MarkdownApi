(function () {
    var markdownEditor = function (markdownSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/markdown-editor.html',
            link: function (scope) {
                scope.document = {
                    html: '',
                    markdown: '',
                    title: '',
                    id: 0
                };

                markdownSvc.render = scope.document;
            }
        }
    };

    markdownEditor.$inject = ['markdownSvc'];
    markdownApp.directive('markdownEditor', markdownEditor);
}());