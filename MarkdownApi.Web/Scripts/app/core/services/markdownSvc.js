(function () {
    var markdownSvc = function () {
        var
            render = {
                markdown: '',
                html: ''
            },
            makeHtml = function (text) {
                var converter = new showdown.Converter({ extensions: ['prettify'] });
                var html = converter.makeHtml(text);
                return html;
            };

        return {
            render: render,
            makeHtml: makeHtml
        };
    };

    markdownApp.factory('markdownSvc', markdownSvc);
}());