module App {
  class HomeView {
    view() {
      return m('view', [
        m(headerView),
        m('p', 'I\'m some content for the home view')
      ]);
    }
  }

  export const homeView = new HomeView();
}
