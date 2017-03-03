module App.Models {
  export class GridOptions {
    cells: {
      id: string,
      title: string,
      renderer?: (v: any) => string
    }[] = [];
    data: {}[] = [];
  }
}