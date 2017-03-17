module App.Models {
  export class GridOptions {
    columns: GridColumn[] = [];
    data: {}[] = [];
  }

  export class GridColumn {
    id: string;
    title: string;
    hide: boolean;
    renderer?: (v: any) => string;
    allowSort?: boolean;
    comparer?: (a, b) => number;
  }
}