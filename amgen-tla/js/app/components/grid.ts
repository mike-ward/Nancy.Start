module App.Components {
  class Grid {
    view(vnode) {
      const styles = vnode.attrs.style || {};
      const gridOptions = vnode.attrs.gridOptions as GridOptions;

      return gridOptions
        ? m('div.grid', { style: styles }, [
          m('table.pure-table.pure-table-bordered', [
            this.head(gridOptions, vnode.state),
            this.body(gridOptions, vnode.state)
          ])
        ])
        : null;
    }

    private head(gridOptions: GridOptions, state: any) {
      const thead = m('thead', [
        m('tr', gridOptions.columns
          .filter(c => !c.hide)
          .map(column => this.columnHead(column, state))
        )
      ]);
      return thead;
    }

    private body(gridOptions: GridOptions, state: any) {
      const data = this.sortByColumn(gridOptions, state);
      const columns = gridOptions.columns.filter(c => !c.hide);
      const tbody = m('tbody', [
        data.map(row => m('tr',
          columns.map(column => this.renderCell(row, column))))
      ]);
      return tbody;
    }

    private columnHead(column: GridColumn, state: any) {
      return m('th.grid-column-title',
        {
          title: column.tooltip || '',
          onclick: () => this.titleClickActions(column, state)
        }, [
          column.title,
          this.sortIndicator(column, state)
        ]
      );
    }

    private renderCell(row: {}, column: GridColumn) {
      const value = this.columnValue(row, column);
      return m('td',
        {
          'class': column.cellClick ? 'grid-click-action' : '',
          title: column.cellTooltip ? column.cellTooltip(value) : '',
          onclick: () => column.cellClick ? column.cellClick(this.columnValue(row, column)) : ''
        },
        column.renderer ? column.renderer(value) : value);
    }

    private columnValue(row, column: GridColumn) {
      const value = row[column.id];
      return value === null || value === undefined ? column.contentIfNull : value;
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
        : Services.Compare.compareAny;

      data.sort((l, r) => {
        const result = comparer(l[columnId], r[columnId]);
        return state.sortDirection ? result : -result;
      });

      return data;
    }

    private titleClickActions(column: GridColumn, state: any) {
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
      .grid-click-action{cursor:pointer;}
      .grid-click-action:hover{text-decoration: underline;}
      .grid-column-title:hover{cursor:pointer;}
      .grid-column-sort-indicator{margin-left:1em;}
      .grid-column-sort-indicator-hidden{visibility:collapse;}
      .grid-column-title:hover .grid-column-sort-indicator-hidden{color:gray !important;visibility:visible;}`;
  }

  export const grid = new Grid();
}