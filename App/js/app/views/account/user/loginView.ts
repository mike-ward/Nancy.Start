// ReSharper disable RedundantQualifier

module App.Views.Account.User {
  class LoginView {
    constructor(
      private readonly authenticationUrl: string,
      private readonly returnUrl: string,
      private readonly errorMessage: string) {
    }

    view() {
      return m('view', [
        m(App.Components.pageHeader),

        m('div.login-form', [
          m('h2', 'Login'),
          m('h2.error-text', this.errorMessage),

          m(`form.pure-form.pure-form-stacked[action="${this.authenticationUrl}?returnUrl=${this.returnUrl}"][method="POST"])`, [

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
    css = `.login-form {
      margin-left: 5rem;
    }`;
  }

  export const loginView = (authenticationUrl: string, returnUrl: string, msg: string) => new LoginView(authenticationUrl, returnUrl, msg);
}