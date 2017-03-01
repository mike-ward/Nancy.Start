describe('addView', () => {
  it('should contain view tag', () => {
    const vnode = App.Views.Account.User.addView().view();
    expect(vnode.tag).toBe('view');
  });
});
