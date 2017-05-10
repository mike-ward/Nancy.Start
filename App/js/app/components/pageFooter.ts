module App.Components {
  function view() {
    return m(
      '.footer', [
        m('hr'),
        m('.app-footer', 'footer stuff goes here')
      ]);
  }

  // language=CSS
  const css = `.footer{margin-top:5rem;}`;

  export const pageFooter = {
    view: view,
    css: css
  }
}