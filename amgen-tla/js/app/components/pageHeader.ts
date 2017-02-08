module App.Components {
  export class PageHeader {
    view() {
      return m('.header', [
        m('h1', 'My Application Title'),
        m('hr')
      ]);
    }
  }
}
