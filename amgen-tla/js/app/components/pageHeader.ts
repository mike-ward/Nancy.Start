module App.Components {
  class PageHeader {
    view() {
      return m('.header', [
        m('h1', 'My Application Title'),
        m('hr')
      ]);
    }
  }

  export const pageHeader = new PageHeader();
}
