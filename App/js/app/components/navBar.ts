module App.Components {
  function view(vnode: any) {
    const options = vnode.attrs.options;
    return options
      ? m('.nav-bar', options.items.map((item: any) => m('a', { href: item.link }, item.name)))
      : null;
  }

  // language=css
  const css = `
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

  export const navBar = {
    view: view,
    css: css
  }
}