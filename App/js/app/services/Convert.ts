// ReSharper disable once InconsistentNaming
module App.Services {
  export class Convert {
    static dateToISO(v: string): string {
      if (!v) return 'null';
      const date = new Date(v).toISOString();
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