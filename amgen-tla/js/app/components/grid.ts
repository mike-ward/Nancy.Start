// ReSharper disable RedundantQualifier

module App.Components {
  import GridOptions = App.Models.GridOptions;
  import GridColumn = App.Models.GridColumn;

  export const grid = (gridOptions: GridOptions) => new Grid(gridOptions);

  class Grid {
    constructor(private readonly gridOptions: GridOptions) {
    }
    view() {
      return this.gridOptions
        ? m('div.grid', [

          m('table.pure-table.pure-table-bordered', [
            this.tableHead(),
            this.tableBody()
          ])

        ])
        : null;
    }

    private tableHead() {
      const thead = m('thead', [

        m('tr', this.gridOptions.columns.map(column =>
          m('th.grid-column-title',
            { onclick: () => this.sortColumn(column) }, [
              column.title,
              this.sortIndicator(column)
            ])))

      ]);
      return thead;
    }

    private tableBody() {
      const data = this.sortByColumn();
      const tbody = m('tbody', [

        data.map(row => m('tr',
          this.gridOptions.columns.map(
            column => m('td', this.renderCell(row[column.id], column.renderer))
          )
        ))

      ]);
      return tbody;
    }

    private renderCell(value: any, renderer: (v: any) => string): string {
      const cellContents = renderer ? renderer(value) : value;
      return cellContents;
    }

    private sortIndicator(column: GridColumn) {
      if (!column.allowSort) return m('');
      const isSorted = column.id === this.gridOptions.sortedColumnId;
      const sortSymbol = isSorted ? (this.gridOptions.sortDirection ? '▲' : '▼') : '▲';
      const cssClass = `grid-column-sort-indicator${isSorted ? '' : '.grid-column-sort-indicator-hidden'}`;
      const vnode = m(`span.${cssClass}`, sortSymbol);
      return vnode;
    }

    private sortByColumn() {
      const data = this.gridOptions.data.slice();
      if (!this.gridOptions.sortedColumnId) return data;
      const columnId = this.gridOptions.sortedColumnId;
      data.sort((l, r) =>
        this.gridOptions.sortDirection
          ? l[columnId] - r[columnId]
          : r[columnId] - l[columnId]);

      return data;
    }

    private sortColumn(column: GridColumn) {
      this.gridOptions.sortDirection = this.gridOptions.sortedColumnId === column.id
        ? !this.gridOptions.sortDirection
        : true;

      this.gridOptions.sortedColumnId = column.id;
    }
  }
}