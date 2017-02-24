describe('addView', () => {
  it('should contain view tag', () => {
    const vnode = App.Views.Account.addUserView().view();
    expect(vnode.tag).toBe('view');
  });
});
