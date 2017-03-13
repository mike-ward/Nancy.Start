module App.Models {
  export class GridOptions {
    columns: GridColumn[] = [];
    data: {}[] = [];
    sortedColumnId: string;
    sortDirection: boolean;
  }

  export class GridColumn {
    id: string;
    title: string;
    renderer?: (v: any) => string;
    allowSort?: boolean;
  }
}