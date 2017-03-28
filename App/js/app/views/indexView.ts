// ReSharper disable RedundantQualifier

module App.Views {
  class IndexView {
    view() {
      return m('view', [
        m(App.Components.pageHeader),
        m('h2', 'Index View'),
        m(App.Components.pageFooter)
      ]);
    }
  }

  export const indexView = new IndexView();
}
