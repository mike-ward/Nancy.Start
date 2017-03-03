// ReSharper disable RedundantQualifier

module App.Components {
  export const grid = (gridOptions: App.Models.GridOptions) => new Grid(gridOptions);

  class Grid {
    constructor(private readonly gridOptions: App.Models.GridOptions) {
    }

    view() {
      return m('div.components-grid', [

        m('table.pure-table.pure-table-bordered', [
          this.emitHead(),
          this.emitBody()
        ])

      ]);
    }

    private emitHead() {
      const header = m('thead', [
        m('tr', this.gridOptions.cells.map(cell => m('th', cell.title)))
      ]);
      return header;
    }

    private emitBody() {
      const body = this.gridOptions.data.map(row =>
        m('tbody', [
          m('tr', this.gridOptions.cells.map(cell => m('td', row[cell.id])))
        ])
      );
      return body;
    }
  }
}