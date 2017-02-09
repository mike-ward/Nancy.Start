﻿module App.Views.Account {
  class LoginView {
    constructor(
      private readonly returnUrl: string,
      private readonly errorMessage: string) {
    }

    view() {
      return m('view', [
        m(Components.pageHeader()),

        m('div.login-form', [
          m('h2', 'Login'),
          m('h2.error-text', this.errorMessage),

          m(`form.pure-form.pure-form-stacked[action="account/forms-authenticate?returnUrl=${this.returnUrl}"][method="POST"])`, [

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

        m(Components.pageFooter())
      ]);
    }
  }

  export const loginView = (url, msg) => new LoginView(url, msg);
}