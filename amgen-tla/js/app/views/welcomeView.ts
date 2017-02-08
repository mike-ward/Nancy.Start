module App.Views {
  class WelcomeView {
    view() {
      return m('view', [
        m(new Components.PageHeader()),
        m('h2', 'Welcome View'),
        m(new Components.PageFooter())
      ]);
    }
  }

  export const welcomeView = () => new WelcomeView();
}
