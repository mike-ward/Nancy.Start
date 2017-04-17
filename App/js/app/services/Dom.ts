module App.Services {
  export class Dom {
    static loadCss(obj: any) {
      const css = this.findAllCss(obj, []);
      Dom.addCssToHead(css.join('\n'));
    }

    static findAllCss(obj: any, css: string[]) {
      const keys = Object.keys(obj);

      keys.forEach(function (key) {
        const value = obj[key];
        if (value && typeof value === 'object') {
          if (value.css) css.push(value.css);
          Dom.findAllCss(value, css);
        }
      });

      return css;
    }

    static addCssToHead(css: string) {
      const style = document.createElement('style');
      style.type = 'text/css';
      style.innerHTML = css;
      document.getElementsByTagName('head')[0].appendChild(style);
    }
  }
}