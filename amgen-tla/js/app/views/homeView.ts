﻿module App.Views {
  export class HomeView {
    view() {
      return m('view', [
        m(new Components.PageHeader()),
        m('h2', 'Home View')
      ]);
    }
  }
}
