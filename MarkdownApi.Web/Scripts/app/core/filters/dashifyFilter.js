(function () {
    var dashify = function () {
        return function (value) {
            return (!value) ? '' : value.replace(/ /g, '-');
        }
    };

    wikiApp.filter('dashify', dashify);
}());