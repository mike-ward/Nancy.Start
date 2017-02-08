module App.Views {
  export class WelcomeView {
    view() {
      return m('view', [
        m(new Components.PageHeader()),
        m('h2', 'Welcome View')
      ]);
    }
  }
}
