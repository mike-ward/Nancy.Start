module App.Components {
  class PageHeader {
    view() {
      return m('.header', [
        m('h1', 'My Application Title'),
        m(Components.navBar, { options: this.navbarOptions }),
        m('hr')
      ]);
    }

    private navbarOptions = {
      items: [
        { name: 'Home', link: 'home' },
        { name: 'Item1', link: 'item1' },
        { name: 'Item2', link: 'item2' },
        { name: 'Item3', link: 'item3' }
      ]
    };

    // language=CSS
    css = `.header{clear:both;text-align:center;}`;
  }

  export const pageHeader = new PageHeader();
}