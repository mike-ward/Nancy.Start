module App.Components {
  export type GridOptions = {
    columns: GridColumn[];
    data: {}[];
  }

  export type GridColumn = {
    id: string;
    title: string;
    tooltip?: string;
    hide?: boolean;
    contentIfNull?: string;
    renderer?: (v: any) => string;
    allowSort?: boolean;
    comparer?: (a:any, b:any) => number;
    cellClick?: (value: any) => void;
    cellTooltip?: (value: any) => void;
  }
}