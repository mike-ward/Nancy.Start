﻿module App.Components {
  export class GridOptions {
    columns: GridColumn[] = [];
    data: {}[] = [];
  }

  export class GridColumn {
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