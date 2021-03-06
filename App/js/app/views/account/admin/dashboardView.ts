﻿// ReSharper disable RedundantQualifier

module App.Views.Account.Admin {
  import GridColumn = Components.GridColumn;

  let loading: boolean;
  let errorMessage: string;
  let gridOptions: App.Components.GridOptions;

  // language=css
  const css = `
      .admin-dashboard .grid { font-size: smaller; }
      .admin-dashboard-buttons { margin-bottom: 1em }
      .admin-dashboard-buttons a { margin-right: 1em; }
    `;

  function getAllUsers() {
    return m.request({ url: 'account/admin/allUsers', data: { r: Date.now() } });
  }

  function showLoadingIndicator() {
    loading = true;
    m.redraw();
  }

  function hideLoadingIndicator() {
    loading = false;
    m.redraw();
  }

  function createGridOptions(allUsers: any[]) {
    const gridOptions = {} as Components.GridOptions;
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
          ? Services.Convert.dateToLocaleString
          : null
      }));

    gridOptions.columns = editColumns.concat(dataColumns);
    gridOptions.data = allUsers;
    return gridOptions;
  }

  function loadGrid() {
    showLoadingIndicator();
    getAllUsers()
      .then((users: any) => {
        hideLoadingIndicator();
        gridOptions = createGridOptions(users);
      })
      .catch((error: any) => {
        hideLoadingIndicator();
        errorMessage = error.message;
      });
  }

  function oncreate() {
    loadGrid();
  }

  function view() {
    return m('view', [
      m(App.Components.pageHeader),
      m('div.admin-dashboard', [
        m('h2', 'Administrator Dashboard'),
        m('h3.error-text', errorMessage),
        m('div.admin-dashboard-buttons', [
          m('a.pure-button', { href: 'account/admin/addUser' }, 'Add User'),
          m('a.pure-button', { href: 'account/admin/system-information' }, 'System Information'),
          m(App.Components.loading, { style: { visibility: loading ? 'visible' : 'hidden' } })
        ]),
        m(App.Components.grid, { gridOptions: gridOptions })
      ]),
      m(App.Components.pageFooter)
    ]);
  }

  export const dashboardView = {
    css: css,
    oncreate: oncreate,
    view: view
  }
}