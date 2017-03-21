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
      .current-user-login{text-align:right;}

      /* override vex button styles to look like pure-css buttons */
      .vex.vex-theme-plain .vex-dialog-button.vex-dialog-button-primary{
        display: inline-block;
        zoom: 1;
        line-height: normal;
        white-space: nowrap;
        vertical-align: middle;
        text-align: center;
        cursor: pointer;
        -webkit-user-drag: none;
        -webkit-user-select: none;
           -moz-user-select: none;
            -ms-user-select: none;
                user-select: none;
        box-sizing: border-box;
        font-family: inherit;
        padding: 0.5em 1em;
        color: #444; 
        color: rgba(0, 0, 0, 0.80); 
        border: 1px solid #999; 
        border: none rgba(0, 0, 0, 0); 
        background-color: #E6E6E6;
        text-decoration: none;
        border-radius: 2px;
      }
      .vex.vex-theme-plain .vex-dialog-button:focus {
        animation: none;
        -webkit-animation: none;
        -moz-animation: none;
        -ms-animation: none;
        -o-animation: none;
        -webkit-backface-visibility: hidden;
        outline: none;
        filter: alpha(opacity=90);
        background-image: -webkit-linear-gradient(transparent, rgba(0,0,0, 0.05) 40%, rgba(0,0,0, 0.10));
        background-image: linear-gradient(transparent, rgba(0,0,0, 0.05) 40%, rgba(0,0,0, 0.10));
      }

      /* fixes vex closing flashing in IE */
      .vex.vex-closing .vex-content {
        opacity: 0; 
      }
      .vex.vex-closing .vex-overlay {
        opacity: 0;
      }`
  }

  export const styles = new Styles();
}