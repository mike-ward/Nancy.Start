module App.Components {
  class PageFooter {
    view() {
      return m('.footer', [
        m('hr'),
        m('.app-footer', 'footer stuff goes here')
      ]);
    }
  }

  export const pageFooter = () => new PageFooter();
}
