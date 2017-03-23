describe('homeView', () => {
  it('should contain view tag', () => {
    const vnode = App.Views.homeView.view();
    expect(vnode.tag).toBe('view');
  });
});
