(function () {
    var manageWiki = function (wikiSvc, toastrSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/manage-wiki.html',
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

                        if (scope.wikiId > 0) {
                            wikiSvc.getWiki(scope.wikiId).then(function (data) {
                                scope.wiki = data;
                            });
                        }

                        scope.saveWiki = function () {
                            if (validate(scope.wiki)) {
                                if (scope.wiki.id > 0) {
                                    wikiSvc.updateWiki(scope.wiki);
                                } else {
                                    wikiSvc.addWiki(scope.wiki).then(function (data) {
                                        window.location.href = "/Admin/Wiki/" + data.id;
                                    });
                                }
                            }
                        }

                        scope.deleteWiki = function () {
                            wikiSvc.deleteWiki(scope.wiki).then(function () {
                                window.location.href = "/Admin";
                            });
                        }

                        function validate(wiki) {
                            if (wiki.name === null || wiki.name === undefined || wiki.name === '') {
                                toastrSvc.alertError("Name must have a value", "Invalid Wiki");
                                return false;
                            }

                            if (wiki.description === null || wiki.description === undefined || wiki.description === '') {
                                toastrSvc.alertError("Description must have a value", "Invalid Wiki");
                                return false;
                            }

                            if (wiki.category.name === null || wiki.category.name === undefined || wiki.category.name === '') {
                                toastrSvc.alertError("Category must have a value", "Invalid Wiki");
                                return false;
                            }

                            return true;
                        }
                    }
                }
            }
        }
    };

    manageWiki.$inject = ['wikiSvc', 'toastrSvc'];
    wikiApp.directive('manageWiki', manageWiki);
}());