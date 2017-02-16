module App.Views {
  class WelcomeView {
    view() {
      return m('view', [
        m(App.Components.pageHeader),
        m('h2', 'Welcome View'),
        m(App.Components.pageFooter)
      ]);
    }
  }

  export const welcomeView = new WelcomeView();
}
