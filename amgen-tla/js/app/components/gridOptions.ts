module App.Components {
  export class GridOptions {
    columns: GridColumn[] = [];
    data: {}[] = [];
  }

  export class GridColumn {
    id: string;
    title: string;
    hide?: boolean;
    contentIfNull?: string;
    renderer?: (v: any) => string;
    allowSort?: boolean;
    comparer?: (a, b) => number;
    cellClick?: (value: any) => void;
  }
}