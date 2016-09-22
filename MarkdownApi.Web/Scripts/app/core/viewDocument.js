(function () {
    var viewDocument = function (documentSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/view-document.html',
            scope: {
                wikiId: '=',
                documentId: '='
            },
            link: function (scope) {
                scope.document = {
                    name: '',
                    description: '',
                    markdown: '',
                    html: '',
                    wiki: {
                        id: scope.wikiId,
                        category: {
                            name: ''
                        },
                        sidebar: {
                            markdown: '',
                            html: ''
                        },
                        markdown: '',
                        html: ''
                    }
                }

                documentSvc.getDocument(scope.documentId).then(function (data) {
                    scope.document = data;
                });
            }
        };
    };

    viewDocument.$inject = ['documentSvc'];
    wikiApp.directive('viewDocument', viewDocument);
}());