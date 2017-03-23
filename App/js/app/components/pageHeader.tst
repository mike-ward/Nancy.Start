describe('PageHeader', () => {
  it('should contain a div with a class of .header', () => {
    const vnode = App.Components.pageHeader.view();
    expect(vnode.tag).toBe('div');
    expect(vnode.attrs.className).toBe('header');
  });
}); 