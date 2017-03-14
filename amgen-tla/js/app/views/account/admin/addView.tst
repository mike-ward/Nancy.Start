describe('addView', () => {
  it('should contain view tag', () => {
    const vnode = App.Views.Account.Admin.addView.view({attrs: {}});
    expect(vnode.tag).toBe('view');
  });
});
