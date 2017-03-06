// ReSharper disable RedundantQualifier

module App.Components {
  import GridOptions = App.Models.GridOptions;
  import GridColumn = App.Models.GridColumn;

  export const grid = (gridOptions: GridOptions) => new Grid(gridOptions);

  class Grid {
    constructor(private readonly gridOptions: GridOptions) {
    }

    private sortedColumnId: string;
    private sortDirection: boolean;

    view() {
      return m('div.grid', [

        m('table.pure-table.pure-table-bordered', [
          this.tableHead(),
          this.tableBody()
        ])

      ]);
    }

    private tableHead() {
      const thead = m('thead', [

        m('tr', this.gridOptions.columns.map(column => m('th.grid-column-title', [
          column.title,
          this.sortIndicator(column)
        ])))

      ]);
      return thead;
    }

    private tableBody() {
      const tbody = m('tbody', [

        this.gridOptions.data.map(row => m('tr', this.gridOptions.columns.map(
          cell => m('td', this.renderCell(row[cell.id], cell.renderer)
        ))))

      ]);
      return tbody;
    }

    private renderCell(value: any, renderer: (v:any) => string): string {
      const cellContents = renderer ? renderer(value) : value;
      return cellContents;
    }

    private sortIndicator(column: GridColumn) {
      if (!column.allowSort) return m('');
      const isSorted = column.id === this.sortedColumnId;
      const sortSymbol = isSorted ? (this.sortDirection ? '▲' : '▼') : '□';
      const cssClass = `grid-column-sort-indicator${isSorted ? '' : '.grid-column-sort-indicator-hidden'}`;
      const vnode = m(`span.${cssClass}`, sortSymbol);
      return vnode;
    }

    private sortColumns(rows: {}[], column: string): any[] {
      return rows.sort((l, r) => l[column] - r[column]);
    }
  }
}