(function () {
    var markdownSvc = function () {
        var
            makeHtml = function (text) {
                var converter = new showdown.Converter({ extensions: ['prettify'] });
                var html = converter.makeHtml(text);
                return html;
            };

        return {
            makeHtml: makeHtml
        };
    };

    wikiApp.factory('markdownSvc', markdownSvc);
}());