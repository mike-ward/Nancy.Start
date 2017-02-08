// // true if IE less than 9
if (!-[1,]) alert('Internet Explorer 7 and 8 are not supported');

// Mithril declarations

declare var m: any;

// Global declarations (use sparingly)

const mountView = view => m.mount(document.getElementById('content'), view);
