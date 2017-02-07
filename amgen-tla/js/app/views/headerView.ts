module App {
  class HeaderView {
    view() {
      return m('h1', { 'class': 'app-title' }, 'Hello World');
    }
  }

  export const headerView = new HeaderView();
}
