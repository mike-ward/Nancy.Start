module App.Components {

  function head(gridOptions: GridOptions, state: any) {
    const thead = m('thead', [
      m('tr', gridOptions.columns
        .filter(c => !c.hide)
        .map(column => columnHead(column, state))
      )
    ]);
    return thead;
  }

  function body(gridOptions: GridOptions, state: any) {
    const data = sortByColumn(gridOptions, state);
    const columns = gridOptions.columns.filter(c => !c.hide);
    const tbody = m('tbody', [
      data.map(row => m('tr',
        columns.map(column => renderCell(row, column))))
    ]);
    return tbody;
  }

  function columnHead(column: GridColumn, state: any) {
    return m('th.grid-column-title',
      {
        title: column.tooltip || '',
        onclick: () => titleClickActions(column, state)
      }, [
        column.title,
        sortIndicator(column, state)
      ]
    );
  }

  function renderCell(row: {}, column: GridColumn) {
    const value = columnValue(row, column);
    return m('td',
      {
        'class': column.cellClick ? 'grid-click-action' : undefined,
        title: column.cellTooltip ? column.cellTooltip(value) : undefined,
        onclick: () => column.cellClick ? column.cellClick(columnValue(row, column)) : undefined
      },
      column.renderer ? column.renderer(value) : value);
  }

  function columnValue(row: any, column: GridColumn) {
    const value = row[column.id];
    return value === null || value === undefined ? column.contentIfNull : value;
  }

  function sortIndicator(column: GridColumn, state: any) {
    if (!column.allowSort) return '';
    const isSorted = column.id === state.sortedColumnId;
    const sortSymbol = isSorted && !state.sortDirection ? '▼' : '▲';
    const cssClass = `grid-column-sort-indicator${isSorted ? '' : '.grid-column-sort-indicator-hidden'}`;
    const vn = m(`span.${cssClass}`, sortSymbol);
    return vn;
  }

  function sortByColumn(gridOptions: GridOptions, state: any) {
    const data = gridOptions.data.slice();
    if (!state.sortedColumnId) return data;
    const columnId = state.sortedColumnId;
    const column = gridOptions.columns[columnId];

    const comparer = column && column.comparer
      ? column.comparer
      : Services.Compare.compareAny;

    data.sort((l: any, r: any) => {
      const result = comparer(l[columnId], r[columnId]);
      return state.sortDirection ? result : -result;
    });

    return data;
  }

  function titleClickActions(column: GridColumn, state: any) {
    if (column.allowSort) columnSortAction(column, state);
  }

  function columnSortAction(column: GridColumn, state: any) {
    state.sortDirection = state.sortedColumnId === column.id
      ? !state.sortDirection
      : true;

    state.sortedColumnId = state.sortedColumnId === column.id && state.sortDirection
      ? null
      : column.id;
  }

  function view(vnode: any) {
    const gridOptions = vnode.attrs.gridOptions as GridOptions;

    return gridOptions
      ? m('div.grid', vnode.attrs, [
        m('table.pure-table.pure-table-bordered', [
          head(gridOptions, vnode.state),
          body(gridOptions, vnode.state)
        ])
      ])
      : null;
  }

  // language=CSS
  const css = `
      .grid th, .grid td{white-space:nowrap;}
      .grid-click-action{cursor:pointer;}
      .grid-click-action:hover{text-decoration: underline;}
      .grid-column-title:hover{cursor:pointer;}
      .grid-column-sort-indicator{margin-left:1em;}
      .grid-column-sort-indicator-hidden{visibility:collapse;}
      .grid-column-title:hover .grid-column-sort-indicator-hidden{color:gray !important;visibility:visible;}`;

  export const grid = {
    view: view,
    css: css
  }
}

