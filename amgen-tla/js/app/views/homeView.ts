module App.Views {
  class HomeView {
    view() {
      return m('view', [
        m(Components.pageHeader),
        m('h2', 'Home View'),
        m(Components.pageFooter)
      ]);
    }
  }

  export const homeView = new HomeView();
}
