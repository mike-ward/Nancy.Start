// // true if IE less than 9
if (!-[1,]) alert('Internet Explorer 7 and 8 are not supported');

// Mithril declarations
declare var m: any;
const mountView = view => m.mount(document.getElementById('content'), view);

// vex dialog package initialization
declare var vex: any;
vex.defaultOptions.className = 'vex-theme-plain';

module App {
  class Styles {
    // language=CSS
    css = `
    body
    {
      margin: 0;
      min-width: 320px;
      max-width: 1200px;
      line-height: 1.6;
      color: #333;
      background-color: #fff;
      margin: 1em auto;
    }

    .error-text
    {
      color: darkred;
    }

    view
    {
      margin: 1em auto;
    }

    .current-user-login
    {
      text-align: right;
    }`
  }

  export const styles = new Styles();
}
