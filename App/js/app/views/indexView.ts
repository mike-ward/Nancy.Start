// ReSharper disable RedundantQualifier

module App.Views {
  function view() {
    return m('view', [
      m(App.Components.pageHeader),
      m('h2', 'Index View'),
      m(App.Components.pageFooter)
    ]);
  }

  export const indexView = { view: view };
}
