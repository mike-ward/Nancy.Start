// ReSharper disable RedundantQualifier

module App.Views.Account.Admin {
  export const dashboardView = () => new DashboardView();

  class DashboardView {
    private allUsers = [];
    private errorMessage: string;

    view() {
      return m('view', [
        m(App.Components.pageHeader),
        m('div.admin-dashboard', [
          m('h2', 'Administrator Dashboard'),
          m('h2.error-text', this.errorMessage)
        ]),
        m('div.admin-dashboard-allusers', this.allUsersGrid()),
        m(App.Components.pageFooter)
      ]);
    }

    oncreate() {
      this.getAllUsers();
    }

    private getAllUsers() {
      m
        .request({ url: 'account/admin/allUsers', withCredentials: true })
        .then(users => this.allUsers = users)
        .catch(error => this.errorMessage = error.message);
    }

    private allUsersGrid() {
      const gridOptions = new App.Models.GridOptions();
      if (this.allUsers.length > 0) {
        const keys = Object.keys(this.allUsers[0]);
        gridOptions.cells = keys
          .map(key => ({ id: key, title: key }))
          .filter(cell => cell.id !== 'Password');
        gridOptions.data = this.allUsers;
      }
      return m(App.Components.grid(gridOptions));
    }
  }
}