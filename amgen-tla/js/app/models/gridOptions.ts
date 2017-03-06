module App.Models {
  export class GridOptions {
    columns: GridColumn[] = [];
    data: {}[] = [];
  }

  export class GridColumn {
    id: string;
    title: string;
    renderer?: (v: any) => string;
    allowSort?: boolean;
  }
}