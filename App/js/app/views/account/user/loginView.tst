describe('loginView', () => {
  it('should contain view tag', () => {
    const vnode = App.Views.Account.User.loginView().view();
    expect(vnode.tag).toBe('view');
  });
});
