// ReSharper disable RedundantQualifier

module App.Views.Account.Admin {
  export const dashboardView = () => new DashboardView();

  class DashboardView {
    private errorMessage: string;
    private gridOptions: App.Models.GridOptions;

    oninit() {
      this.getAllUsers();
    }

    view() {
      return m('view', [
        m(App.Components.pageHeader),
        m('div.admin-dashboard', [
          m('h2', 'Administrator Dashboard'),
          m('h2.error-text', this.errorMessage)
        ]),
        m('div.admin-dashboard-allusers', [ m(App.Components.grid(this.gridOptions)) ]),
        m(App.Components.pageFooter)
      ]);
    }

    private getAllUsers() {
      m
        .request({ url: 'account/admin/allUsers', withCredentials: true })
        .then(users => this.initGridOptions(users))
        .catch(error => this.errorMessage = error.message);
    }

    private initGridOptions(allUsers: any[]) {
      this.gridOptions = new App.Models.GridOptions();
      const hideColumns = ['id', 'password'];
      const dateColumns = ['createdOn', 'lastModified', 'lastPasswordChange', 'lastLogin'];

      if (allUsers.length > 0) {
        const keys = Object.keys(allUsers[0]);
        this.gridOptions.columns = keys
          .filter(key => hideColumns.every(hc => hc !== key))
          .map(key => ({
            id: key,
            title: App.Services.Renderers.camelIdentifierToTitle(key),
            allowSort: true,
            renderer: dateColumns.some(dc => dc === key)
              ? App.Services.Renderers.toDateTime
              : null
          }));
        this.gridOptions.data = allUsers;
      }
    }
  }
}