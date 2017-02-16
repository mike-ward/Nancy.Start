module App.Views {
  class WelcomeView {
    view() {
      return m('view', [
        m(Components.pageHeader),
        m('h2', 'Welcome View'),
        m(Components.pageFooter)
      ]);
    }
  }

  export const welcomeView = new WelcomeView();
}
