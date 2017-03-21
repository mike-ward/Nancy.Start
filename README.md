Nancy.Start
===========

Things you should know:

-   [NancyFx](http://nancyfx.org) for the back end. ASP.NET MVC and it's
    various flavors are not my thing.
-   [Mithril](http://mithril.js.org) for the front end. Easier and
    faster than React (but very React like).
-   I don't like Gulp/Grunt and don't use them.
-   Karma/Jasmine is used for testing.
-   Typescript because it's better.
-   I like using Visual Studio to edit back and front ends.
-   For JavaScript I don't use a bundler so you can use require. I find
    namespaces in Typescript are all I need. You probably don't agree. I
    don't care. I also don't need to deal with WebPack and it's
    variants. Instead, I add a few lines of custom post build code to
    the project file. This concatenates and minifies as needed.
-   There's no hot loading (admittedly a cool thing). I run Karma in
    watch mode. This rebuilds the JavaScript so I can simply refresh the
    browser. Works for me.
-   App style sheets are embedded in JavaScript. Any object with a `css`
    member is found and inserted into the DOM.
-   Thre's a simple grid component that supports column sorting. Custom
    cell renders and click actions are supported.
-   Dialogs use the vex package.
-   Basic account support with login/logout.
-   Active directory support
-   Elmah
-   Purecss.io for styles. It's super small and does what I need.

