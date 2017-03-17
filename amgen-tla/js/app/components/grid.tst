describe('Grid', () => {
  it('should contain a div with a class of .footer', () => {
    const vnode = App.Components.grid.view({ attrs: { gridOptions: { columns: [], data: [] } }, state: { sortedColumnId: null } });
    expect(vnode.tag).toBe('div');
    expect(vnode.attrs.className).toBe('grid');
  });
}); 