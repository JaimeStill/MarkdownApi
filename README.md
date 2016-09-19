# In Progress

# Markdown API

For more detailed information on Markdown, see [Markdown Syntax](https://daringfireball.net/projects/markdown/syntax)

![markdowneditor](https://cloud.githubusercontent.com/assets/14102723/18610595/6a886ac2-7cee-11e6-8213-6cacea8844f6.png)

## Dependencies

### External Dependencies
* [showdownjs](https://github.com/showdownjs/showdown)
* [showdownjs-prettify-extension](https://github.com/showdownjs/prettify-extension)
* [google-code-prettify](https://github.com/google/code-prettify)

### NuGet Packages

#### MarkdownAPI.Web
<pre>
AngularJS.Core (1.5.8)
</pre>

## How It Works

### Markdown Service

``` javascript
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
```

Provides a <code>render</code> object that coordinates the translation of Markdown to HTML, as well as a <code>makeHtml()</code> function that receives markdown text, initializes a showdown <code>Converter</code>, and returns the converted HTML.

### Render Markdown Directive

#### renderMarkdown.js
``` javascript
(function () {
    var renderMarkdown = function (markdownSvc, $sce) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/core/render-markdown.html',
            compile: function (element, attributes) {
                return {
                    pre: function (scope) {
                        scope.render = markdownSvc.render;

                        scope.$watch(function () { return scope.render.markdown; },
                            function (newValue) {
                                scope.render.html = $sce.trustAsHtml(markdownSvc.makeHtml(newValue));
                            });
                    },
                    post: function (scope, element) {
                        var renderer = element.find('div')[0];

                        scope.$watch(function () { return scope.render.html; },
                            function () {
                                scope.$evalAsync(prettyPrint(null, renderer));
                            });
                    }
                }
            }
        };
    };

    renderMarkdown.$inject = ['markdownSvc', '$sce'];
    markdownApp.directive('renderMarkdown', renderMarkdown);
}());
```

#### render-markdown.html
``` html
<div ng-bind-html="render.html"></div>
```

This directive receives a <code>markdownSvc</code> object, as well as the AngularJS [<code>$sce (Strict Contextual Escaping)</code>](https://docs.angularjs.org/api/ng/service/$sce) service, which is used in conjunction with [<code>ngBindHtml</code>](https://docs.angularjs.org/api/ng/directive/ngBindHtml) to bind HTML content to an element in Angular.

The [<code>pre-linking function</code>](https://docs.angularjs.org/api/ng/service/$compile#pre-linking-function) binds the <code>markdownSvc.render</code> object to the directive's scope. A [<code>$watch</code>](https://docs.angularjs.org/api/ng/type/$rootScope.Scope#$watch) is registered on the <code>markdown</code> property of the <code>render</code> object for changes. When changes occur, then updated value is assigned to the <code>html</code> property. This is accomplished by converting the <code>newValue</code> markdown to HTML using the <code>markdownSvc.makeHtml()</code> function inside of the <code>$sce.trustAsHtml()</code> function. 

**If this HTML were not explicitly trusted as HTML, the content would not be assigned and an error would be thrown to the browser console.**

The [<code>post-linking function</code>](https://docs.angularjs.org/api/ng/service/$compile#post-linking-function) initializes a variable for the <code>&lt;div&gt;</code> that defines the directive template. A [<code>$watch</code>](https://docs.angularjs.org/api/ng/type/$rootScope.Scope#$watch) is registered for the <code>markdownSvc.render.html</code> property. When the HTML value changes, the google-code-prettify.js <code>prettyPrint()</code> function is called. <code>null</code> is used to specify that no callback should be executed when the function completes, and the <code>renderer</code> element specifies the directive <code>&lt;div&gt;</code> as the root element to execute upon. Otherwise, <code>prettyPrint()</code> resolves to the <code>&lt;body&gt;</code> element of the document. This is all executed inside of an [<code>$evalAsync</code>](https://docs.angularjs.org/api/ng/type/$rootScope.Scope#$evalAsync) to ensure that the view appropriately reflects the changes.

### Markdown Editor Directive

#### markdownEditor.js
``` javascript
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
```

#### markdown-editor.html
``` html
<div>
    <h2 class="text-center">Markdown Editor</h2>
    <hr />
    <div class="row">
        <div class="col-sm-4">
            <textarea ng-model="render.markdown" class="form-control md-editor" spellcheck="false" rows="40"></textarea>
        </div>
        <div class="col-sm-8">
            <render-markdown></render-markdown>
        </div>
    </div>
</div>
```

This directive receives the <code>markdownSvc</code> service and binds the <code>render</code> object to its scope. The <code>render.markdown</code> property uses two-way binding on the <code>&lt;textarea&gt;</code> defined by the directive template. As this value is modified, the <code>&lt;render-markdown&gt;</code> directive renders an HTML preview of the markdown provided.
