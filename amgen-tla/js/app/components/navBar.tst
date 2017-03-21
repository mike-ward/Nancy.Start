describe('NavBar', () => {
  it('should have div with class .navbar and anchors', () => {
    const navbarOptions = {
      items: [
        { name: 'Home', link: 'home' },
        { name: 'Item1', link: 'item1' },
        { name: 'Item2', link: 'item2' },
        { name: 'Item3', link: 'item3' }
      ]
    };
    const vnode = App.Components.navBar.view({ attrs: {options: navbarOptions} });
    expect(vnode.tag).toBe('div')
    expect(vnode.attrs.className, 'nav-bar');
    expect(vnode.children.length).toBe(4);
  })
})