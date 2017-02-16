describe('welcomeView', () => {
  it('should contain view tag', () => {
    const vnode = App.Views.welcomeView.view();
    expect(vnode.tag).toBe('view');
  });
});
