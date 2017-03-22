module App.Components {
  class NavBar {
    view(vnode) {
      const options = vnode.attrs.options;
      return options
        ? m('.nav-bar', options.items.map(item => m('a', { href: item.link }, item.name)))
        : null;
    }

    // language=css
    css = `
      .nav-bar {
        margin: .25em auto;
      }
      .nav-bar a {
        margin: 0 1em;
        white-space: nowrap;
        text-decoration: none;
      }
      .nav-bar a:hover {
        border-bottom: solid 1px;
      }`;
  }

  export const navBar = new NavBar();
}