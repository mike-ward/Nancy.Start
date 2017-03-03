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
      const body = m('tbody', [

        this.gridOptions.data.map(row => m('tr', this.gridOptions.cells.map(
          cell => m('td', this.renderCell(row[cell.id], cell.renderer)
        ))))

      ]);
      return body;
    }

    private renderCell(value: any, renderer: (v:any) => string): string {
      const td = renderer ? renderer(value) : value;
      return td;
    }
  }
}