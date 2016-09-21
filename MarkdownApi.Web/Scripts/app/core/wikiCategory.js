(function () {
    var wikiCategory = function (categorySvc, toastrSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/wiki-category.html',
            scope: {
                category: '=',
                wikiLink: '@',
                isManaging: '='
            },
            link: function (scope) {
                scope.isEditing = false;

                scope.editCategory = function () {
                    scope.isEditing = true;
                    scope.savedCategory = scope.category.name;
                }

                scope.cancelEdit = function () {
                    scope.category.name = scope.savedCategory;
                    scope.isEditing = false;
                }

                scope.renameCategory = function () {
                    if (validate(scope.category.name)) {
                        categorySvc.renameCategory(scope.category).then(function () {
                            scope.isEditing = false;
                        });
                    }
                }

                function validate(category) {
                    if (category === null || category === undefined || category === '') {
                        toastrSvc.alertWarning('Category name must have a value', 'Invalid Category');
                    }

                    return true;
                }
            }
        };
    };

    wikiCategory.$inject = ['categorySvc', 'toastrSvc'];
    wikiApp.directive('wikiCategory', wikiCategory);
}());