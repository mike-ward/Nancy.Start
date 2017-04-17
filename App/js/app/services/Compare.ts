module App.Services {
  export class Compare {
    static compareAny(a: any, b: any): number {
      if (a === b) return 0;

      // NaN, and only NaN, will compare unequal to itself,
      // and is more reliable than isNaN(). Adding insult
      // to injury, NaN itself is not a number.
      // ReSharper disable once SimilarExpressionsComparison
      const aIsNaN = a !== a;
      // ReSharper disable once SimilarExpressionsComparison
      const bIsNaN = b !== b;

      if (typeof a === 'number' || typeof b === 'number' || aIsNaN || bIsNaN) {
        if (aIsNaN && bIsNaN) return 0;
        if (aIsNaN && !bIsNaN) return -1;
        if (!aIsNaN && bIsNaN) return +1;
        if (a === undefined && b !== undefined) return -1;
        if (a !== undefined && b === undefined) return +1;
        return a - b;
      }

      if (a !== null && b === null) return +1;
      if (a === null && b !== null) return -1;
      return a.localeCompare(b);
    }
  }
}