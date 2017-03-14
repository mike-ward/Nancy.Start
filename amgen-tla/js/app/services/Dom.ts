module App.Services {
  export class Dom {
    static loadAllCss(obj) {
      const css = this.findAllCss(obj, []);
      this.addCss(css.join('\n'));
    }

    static findAllCss(obj, allCss) {
      const keys = Object.keys(obj);

      keys.forEach(function (key) {
        const value = obj[key];
        if (value && typeof value === 'object') {
          if (value.css) allCss.push(value.css);
          Dom.findAllCss(value, allCss);
        }
      });

      return allCss;
    }

    static addCss(css: string) {
      const style = document.createElement('style');
      style.type = 'text/css';
      style.innerHTML = css;
      document.getElementsByTagName('head')[0].appendChild(style);
    }
  }
}