// ReSharper disable RedundantQualifier

module App.Views {
  class HomeView {
    view() {
      return m('view', [
        m(App.Components.pageHeader),
        m('h2', 'Home View'),
        m(App.Components.pageFooter)
      ]);
    }
  }

  export const homeView = new HomeView();
}
