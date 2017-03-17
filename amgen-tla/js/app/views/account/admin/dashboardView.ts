// ReSharper disable RedundantQualifier

module App.Views.Account.Admin {
  class DashboardView {
    private errorMessage: string;
    private gridOptions: App.Models.GridOptions;

    oncreate() {
      this.getAllUsers();
    }

    view() {
      return m('view', [
        m(App.Components.pageHeader),
        m('div.admin-dashboard', [
          m('h2', 'Administrator Dashboard'),
          m('h2.error-text', this.errorMessage)
        ]),
        m('div', { style: { 'margin-bottom': '1em' } }, [
          m('a.pure-button', { href: 'account/admin/add' }, 'Add User')
        ]),
        m(App.Components.grid, { 'gridOptions': this.gridOptions, style: {'font-size': 'smaller'} }),
        m(App.Components.pageFooter)
      ]);
    }

    private getAllUsers() {
      m.request({ url: 'account/admin/allUsers', data: { r: Date.now() } })
        .then(users => this.createGridOptions(users))
        .catch(error => this.errorMessage = error.message);
    }

    private createGridOptions(allUsers: any[]) {
      this.gridOptions = new App.Models.GridOptions();
      if (allUsers.length <= 0) return;

      const hideColumns = ['id', 'password'];
      const dateColumns = ['createdOn', 'lastModified', 'lastPasswordChange', 'lastLogin'];
      const keys = Object.keys(allUsers[0]);

      this.gridOptions.columns = keys
        .map(key => ({
          id: key,
          title: App.Services.Renderers.camelIdentifierToTitle(key),
          allowSort: true,
          hide: hideColumns.some(hc => hc === key),
          renderer: dateColumns.some(dc => dc === key)
            ? App.Services.Renderers.dateToISO
            : null
        }));

      this.gridOptions.data = allUsers.map(u => {
        u.key = u.id; // mithril can render faster with keys
        return u;
      });
    }
  }

  export const dashboardView = new DashboardView();
}