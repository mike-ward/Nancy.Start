module App.Components {
  class PageHeader {
    view() {
      return m('.header', [
        m('h1', 'My Application Title'),
        m('hr')
      ]);
    }

    // language=CSS
    css = `.header {
      text-align: center;
      clear: both;
    }`;
  }

  export const pageHeader = new PageHeader();
}
