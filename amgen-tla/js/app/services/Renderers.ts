module App.Services {
  export class Renderers {
    static toDateTime(v: string): string {
      if (!v) return 'null';
      const date = new Date(v).toLocaleString();
      return date;
    }

    static camelIdentifierToTitle(v: string): string {
      const split = v.split(/(?=[A-Z])/);
      const join = split.join(' ');
      const title = join.charAt(0).toUpperCase() + join.slice(1);
      return title;
    }
  }
}