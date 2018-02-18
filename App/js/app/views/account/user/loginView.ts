// ReSharper disable RedundantQualifier

module App.Views.Account.User {
  function loginViewFactory(authenticationUrl: string, returnUrl: string, errorMessage: string) {

    function view() {
      return m('view', [
        m(App.Components.pageHeader),

        m('div.login-form', [
          m('h2', 'Login'),
          m('h2.error-text', errorMessage),

          m(`form.pure-form.pure-form-stacked[action="${authenticationUrl}?returnUrl=${returnUrl}"][method="POST"])`, [

            m('div.pure-control-group', [
              m('label[for="name"]', 'Email'),
              m('input[autofocus="autofocus"][id="name"][name="user"][required=""][type="email"]')
            ]),

            m('div.pure-control-group', [
              m('label[for="password"]', 'Password'),
              m('input[id="password"][name="password"][required=""][type="password"]')
            ]),

            m('div.pure-control-group', [
              m('button.pure-button.pure-button-primary[id="submit"][type="submit"]', 'Log In')
            ])

          ])
        ]),

        m(App.Components.pageFooter)
      ]);
    }

    // language=CSS
    const css = `.login-form {
      margin-left: 5rem;
    }`;

    return { view: view, css: css }
  }

  export const loginView = loginViewFactory;
}