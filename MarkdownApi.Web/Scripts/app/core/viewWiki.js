(function () {
    var viewWiki = function (wikiSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/view-wiki.html',
            scope: {
                wikiId: '='
            },
            compile: function () {
                return {
                    pre: function (scope) {
                        scope.wiki = {
                            name: '',
                            description: '',
                            markdown: '',
                            html: '',
                            category: {
                                name: ''
                            },
                            sidebar: {
                                markdown: '',
                                html: ''
                            }
                        }

                        wikiSvc.getWiki(scope.wikiId).then(function (data) {
                            scope.wiki = data;
                        });
                    }
                }
            }
        }
    };

    viewWiki.$inject = ['wikiSvc', 'markdownSvc', 'toastrSvc'];
    wikiApp.directive('viewWiki', viewWiki);
}());