module App.Components {
  class Loading {
    view(vnode:any) {
      return m('span', vnode.attrs, [
        m('img.loading-img', { src: 'Content/images/loading-rectangle.gif' }),
        m.trust('&nbsp;Loading&hellip;')
      ]);
    }

    // language=css
    css = `.loading-img { height: 16px; width: 16px; vertical-align: middle}`;
  }

  export const loading = new Loading();
}