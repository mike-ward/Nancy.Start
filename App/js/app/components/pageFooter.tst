describe('PageFooter', () => {
  it('should contain a div with a class of .footer', () => {
    const vnode = App.Components.pageFooter.view();
    expect(vnode.tag).toBe('div');
    expect(vnode.attrs.className).toBe('footer');
  });
}); 