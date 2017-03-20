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
      body{background-color:#fff;color:#333;line-height:1.6;margin:0;margin:1em auto;max-width:1200px;min-width:320px;}
      view{margin:1em auto;}
      .error-text{color:darkred;}
      .current-user-login{text-align:right;}`
  }

  export const styles = new Styles();
}