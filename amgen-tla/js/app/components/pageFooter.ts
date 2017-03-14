module App.Components {
  class PageFooter {
    view() {
      return m('.footer', [
        m('hr'),
        m('.app-footer', 'footer stuff goes here')
      ]);
    }

    // language=CSS
    css = `.footer {
      margin-top: 5rem;
    }`;
  }

  export const pageFooter = new PageFooter();
}
