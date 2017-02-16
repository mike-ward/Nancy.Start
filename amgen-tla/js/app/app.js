"use strict";
// // true if IE less than 9
if (!-[1,])
    alert('Internet Explorer 7 and 8 are not supported');
// Global declarations (use sparingly)
var mountView = function (view) { return m.mount(document.getElementById('content'), view); };
require('mithril.js');
