(function () {
    var manageDocument = function (wikiSvc, documentSvc, markdownSvc, toastrSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/manage-document.html',
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
                        id: scope.wikiId
                    }
                }

                if (scope.documentId > 0) {
                    documentSvc.getDocument(scope.documentId).then(function (data) {
                        scope.document = data;
                    });
                } else {
                    wikiSvc.getWiki(scope.wikiId).then(function (data) {
                        scope.document.wiki = data;
                    });
                }

                scope.saveDocument = function () {
                    if (validate(scope.document)) {
                        if (scope.document.id > 0) {
                            documentSvc.updateDocument(scope.document);
                        } else {
                            documentSvc.addDocument(scope.document).then(function (data) {
                                window.location.href = '/Admin/Wiki/' + scope.wikiId + '/Document/' + data.id;
                            });
                        }
                    }
                }

                scope.deleteDocument = function () {
                    documentSvc.deleteDocument(scope.document).then(function () {
                        window.location.href = '/Admin/Wiki/' + scope.wikiId;
                    });
                }

                function validate(document) {
                    if (document.title === null || document.title === undefined || document.title === '') {
                        toastrSvc.alertError("Title must have a value", "Invalid Document");
                        return false;
                    }

                    return true;
                }
            }
        }
    };

    manageDocument.$inject = ['wikiSvc', 'documentSvc', 'markdownSvc', 'toastrSvc'];
    wikiApp.directive('manageDocument', manageDocument);
}());