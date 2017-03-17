// ReSharper disable RedundantQualifier
// ReSharper disable once SimilarExpressionsComparison

module App.Components {
  import GridOptions = App.Models.GridOptions;
  import GridColumn = App.Models.GridColumn;

  class Grid {
    view(vnode) {
      const styles = vnode.attrs.style || {};
      const gridOptions = vnode.attrs.gridOptions as GridOptions;
      return gridOptions
        ? m('div.grid', { style: styles }, [
          m('table.pure-table.pure-table-bordered', [
            this.tableHead(gridOptions, vnode.state),
            this.tableBody(gridOptions, vnode.state)
          ])
        ])
        : null;
    }

    private tableHead(gridOptions: GridOptions, state: any) {
      const thead = m('thead', [
        m('tr', gridOptions.columns.filter(c => !c.hide).map(column =>
          m('th.grid-column-title',
            { onclick: () => this.columnClickActions(column, state) }, [
              column.title,
              this.sortIndicator(column, state)
            ])
        ))
      ]);
      return thead;
    }

    private tableBody(gridOptions: GridOptions, state: any) {
      const data = this.sortByColumn(gridOptions, state);
      const columns = gridOptions.columns.filter(c => !c.hide);
      const tbody = m('tbody', [
        data.map(row => m('tr', columns.map(
            column => m('td', this.renderCell(this.columnValue(row, column), column.renderer))
          ))
        )
      ]);
      return tbody;
    }

    private columnValue(row, column: GridColumn) {
      const value = row[column.id];
      // isNaN(undefined) === true, unfortunately
      // NaN, and only NaN, will compare unequal to itself
      if (value || value === 0 || value !== value) return value;
      return column.contentIfNull || '';
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
      const column = gridOptions.columns[columnId];

      const comparer = column && column.comparer
        ? column.comparer
        : this.defaultComparer;

      data.sort((l, r) => {
        const result = comparer(l[columnId], r[columnId]);
        return state.sortDirection ? result : -result;
      });

      return data;
    }

    private defaultComparer(a, b) {
      if (a === b) return 0; // NaN, and only NaN, will compare unequal to itself

      if (typeof a === 'number') {
        const aa = isNaN(a);
        const bb = isNaN(b);
        if (aa && bb) return 0;
        if (aa && !bb) return 1;
        if (!aa && bb) return -1;
        return a - b;
      }

      if (a != null && b == null) return 1;
      if (a == null && b != null) return -1;
      return a.localeCompare(b);
    }

    private columnClickActions(column: GridColumn, state: any) {
      if (column.allowSort) this.columnSortAction(column, state);
    }

    private columnSortAction(column: GridColumn, state: any) {
      state.sortDirection = state.sortedColumnId === column.id
        ? !state.sortDirection
        : true;

      state.sortedColumnId = state.sortedColumnId === column.id && state.sortDirection
        ? null
        : column.id;
    }

    // language=CSS
    css = `
      .grid th, .grid td{white-space:nowrap;}
      .grid-column-title:hover{cursor:pointer;}
      .grid-column-sort-indicator{margin-left:1em;}
      .grid-column-sort-indicator-hidden{visibility:collapse;}
      .grid-column-title:hover .grid-column-sort-indicator-hidden{color:gray !important;visibility:visible;}`;
  }

  export const grid = new Grid();
}