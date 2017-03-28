describe('indexView', () => {
  it('should contain view tag', () => {
    const vnode = App.Views.indexView.view();
    expect(vnode.tag).toBe('view');
  });
});
