module App.Components {
  // language=CSS
  const css = `.header{clear:both;text-align:center;}`;

  const navbarOptions = {
    items: [
      { name: 'Home', link: 'home' },
      { name: 'Item1', link: 'item1' },
      { name: 'Item2', link: 'item2' },
      { name: 'Item3', link: 'item3' }
    ]
  }

  function view() {
    return m(
      '.header', [
        m('h1', 'My Application Title'),
        m(Components.navBar, { options: navbarOptions }),
        m('hr')
      ]);
  }

  export const pageHeader = {
    view: view,
    css: css
  }
}