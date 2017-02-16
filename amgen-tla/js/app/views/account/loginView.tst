describe('loginView', () => {
  it('should contain view tag', () => {
    const vnode = App.Views.Account.loginView().view("url", "");
    expect(vnode.tag).toBe('view');
  });
});
