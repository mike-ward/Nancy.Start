// ReSharper disable RedundantQualifier

module App.Components {
  import GridOptions = App.Models.GridOptions;
  import GridColumn = App.Models.GridColumn;

  class Grid {
    view(vnode) {
      const gridOptions = vnode.attrs.gridOptions as GridOptions;
      return gridOptions
        ? m('div.grid', [
          m('table.pure-table.pure-table-bordered', [
            this.tableHead(gridOptions, vnode.state),
            this.tableBody(gridOptions, vnode.state)
          ])
        ])
        : null;
    }

    private tableHead(gridOptions: GridOptions, state: any) {
      const thead = m('thead', [
        m('tr', gridOptions.columns.map(column =>
          m('th.grid-column-title',
            { onclick: () => this.sortColumn(column, state) }, [
              column.title,
              this.sortIndicator(column, state)
            ])
        ))
      ]);
      return thead;
    }

    private tableBody(gridOptions: GridOptions, state: any) {
      const data = this.sortByColumn(gridOptions, state);
      const tbody = m('tbody', [
        data.map(row => m('tr',
          gridOptions.columns.map(
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

    private sortIndicator(column: GridColumn, state: any) {
      if (!column.allowSort) return '';
      const isSorted = column.id === state.sortedColumnId;
      const sortSymbol = isSorted && !state.sortDirection ? '▼' : '▲';
      const cssClass = `grid-column-sort-indicator${isSorted ? '' : '.grid-column-sort-indicator-hidden'}`;
      const vn = m(`span.${cssClass}`, sortSymbol);
      return vn;
    }

    private sortByColumn(gridOptions: GridOptions, state: any) {
      const data = gridOptions.data.slice();
      if (!state.sortedColumnId) return data;
      const columnId = state.sortedColumnId;

      const sorter = state.sortDirection
        ? (l, r) => l[columnId] - r[columnId]
        : (l, r) => r[columnId] - l[columnId];

      data.sort(sorter);
      return data;
    }

    private sortColumn(column: GridColumn, state: any) {
      state.sortDirection = state.sortedColumnId === column.id
        ? !state.sortDirection
        : true;

      state.sortedColumnId = column.id;
    }

    // language=CSS
    css = `.grid th, .grid td {
      white-space: nowrap;
    }

    .grid-column-title:hover {
      cursor: pointer;
    }

    .grid-column-sort-indicator {
      margin-left: 1em;  
    }

    .grid-column-sort-indicator-hidden {
      visibility: collapse;
    }

    .grid-column-title:hover .grid-column-sort-indicator-hidden {
      color: gray !important;
      visibility: visible;
    }`;
  }

  export const grid = new Grid();
}