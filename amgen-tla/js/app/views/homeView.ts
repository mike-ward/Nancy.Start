module App.Views {
  class HomeView {
    view() {
      return m('view', [
        m(new Components.PageHeader()),
        m('h2', 'Home View'),
        m(new Components.PageFooter())
      ]);
    }
  }

  export const homeView = () => new HomeView();
}
