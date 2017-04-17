module App.Components {
  class Loading {
    view(vnode:any) {
      return m('span', vnode.attrs, [
        m('img',
          {
            src: 'Content/images/loading-rectangle.gif',
            style: { height: '16px', width: '16px', 'vertical-align': 'middle' }
          }),
        m.trust('&nbsp;Loading&hellip;')
      ]);
    }
  }

  export const loading = new Loading();
}