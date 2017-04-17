// ReSharper disable RedundantQualifier

module App.Views.Account.Admin {
  import GridColumn = App.Components.GridColumn;

  class DashboardView {
    private loading: boolean;
    private errorMessage: string;
    private gridOptions: App.Components.GridOptions;

    oncreate() {
      this.loadGrid();
    }

    view() {
      return m('view', [
        m(App.Components.pageHeader),
        m('div.admin-dashboard', [
          m('h2', 'Administrator Dashboard'),
          m('h3.error-text', this.errorMessage),
          m('div.admin-dashboard-buttons', [
            m('a.pure-button', { href: 'account/admin/add' }, 'Add User'),
            m('a.pure-button', { href: 'account/admin/system-information' }, 'System Information'),
            m(App.Components.loading, { style: { visibility: this.loading ? 'visible' : 'hidden' } })
          ]),
          m(App.Components.grid, { gridOptions: this.gridOptions })
        ]),
        m(App.Components.pageFooter)
      ]);
    }

    // language=css
    css = `
      .admin-dashboard .grid { font-size: smaller; }
      .admin-dashboard-buttons { margin-bottom: 1em }
      .admin-dashboard-buttons a { margin-right: 1em; }
    `;

    private loadGrid() {
      this.showLoadingIndicator();
      this.getAllUsers()
        .then((users: any) => {
          this.hideLoadingIndicator();
          this.gridOptions = this.createGridOptions(users);
        })
        .catch((error: any) => {
          this.hideLoadingIndicator();
          this.errorMessage = error.message;
        });
    }

    private getAllUsers() {
       return m.request({ url: 'account/admin/allUsers', data: { r: Date.now() } });
    }

    private showLoadingIndicator() {
      this.loading = true;
      m.redraw();
    }

    private hideLoadingIndicator() {
      this.loading = false;
      m.redraw();
    }

    private createGridOptions(allUsers: any[]) {
      const gridOptions = new App.Components.GridOptions();
      if (allUsers.length <= 0) return gridOptions;

      const hideColumns = ['id', 'password'];
      const dateColumns = ['createdOn', 'lastModified', 'lastPasswordChange', 'lastLogin'];
      const keys = Object.keys(allUsers[0]);

      const editColumns: GridColumn[] = [
        {
          id: '✗',
          title: '✗',
          tooltip: 'Delete User',
          contentIfNull: '✗',
          cellClick: v => Services.Dialog.confirm(`Delete ${v}?`)
        },
        {
          id: '✎',
          title: '✎',
          tooltip: 'Edit User',
          contentIfNull: '✎',
          cellClick: v => Services.Dialog.alert(v)
        }
      ];

      const dataColumns: GridColumn[] = keys
        .map(key => ({
          id: key,
          title: Services.Convert.camelIdentifierToTitle(key),
          allowSort: true,
          hide: hideColumns.some(hc => hc === key),
          renderer: dateColumns.some(dc => dc === key)
            ? Services.Convert.dateToISO
            : null
        }));

      gridOptions.columns = editColumns.concat(dataColumns);
      gridOptions.data = allUsers;
      return gridOptions;
    }
  }

  export const dashboardView = new DashboardView();
}