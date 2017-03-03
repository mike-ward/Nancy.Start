describe('dashboardView', () => {
  it('should contain view tag', () => {
    const vnode = App.Views.Account.Admin.dashboardView().view();
    expect(vnode.tag).toBe('view');
  });
});
