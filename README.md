Nancy.Start
===========

Admittedly, this is not a normal bootstrap project since it doesn't
follow the current industry norms. I value fewer dependencies and
shorter code paths which has led me to this arrangement. YMMV

-   [NancyFx](http://nancyfx.org) for the back end. ASP.NET MVC and it's
    various flavors are not my thing.
-   [Mithril](http://mithril.js.org) for the front end. Easier and
    faster than React (but very React like).
-   I don't like Gulp/Grunt and don't use them.
-   Karma/Jasmine is used for testing.
-   Typescript because it's better.
-   I like using Visual Studio to edit back and front end code. I also
    like the debugging environment.
-   For JavaScript I use my own bundler. I find namespaces in Typescript
    are all I need. You can't use `require(module)` in this setup. It
    also means there's no WebPack (or other bundler). Instead, I add a
    few lines of custom post build code to the project file that
    concatenates and minifies as needed.
-   App style sheets are embedded in JavaScript. Any object with a `css`
    member is found and inserted into the DOM.
-   There's no hot loading (admittedly a cool thing). I run Karma in
    watch mode. This rebuilds the JavaScript so I can simply refresh the
    browser. Since CSS is embedded in JavaScript, it also refreshes the
    styesheets. F5 to reload the page.
-   Basic account support with login/logout.
-   Active Directory support
-   There's a simple `grid` component that supports column sorting.
    Custom cell renders and click actions are supported.
-   Elmah for error logging.
-   Purecss.io for styles. It's super small and does what I need.
-   NPM for JavaScript package managment and test scripting.
-   Mithril has a router but I don't use it. I find loading pages from
    the server works better for my purposes.

Notes:

To login:

User: admin@admin.com  
Password: admin

### Get Started

- nuget restore
- cd App/js
- npm install
- Open in Visual Studio
- Press `F5`
