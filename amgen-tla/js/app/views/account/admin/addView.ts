// ReSharper disable RedundantQualifier

module App.Views.Account.Admin {
  class AddView {
    constructor(
      private readonly addUserUrl: string,
      private readonly errorMessage: string) {
    }

    view() {
      return m('view', [
        m(App.Components.pageHeader),

        m('div.add-user-form', [
          m('h2', 'New User'),
          m('h2.error-text', this.errorMessage),

          m(`form.pure-form.pure-form-stacked[action="${this.addUserUrl}"][method="POST"])`, [
            m('div.pure-control-group', [
              m('label[for="name"]', 'Email'),
              m('input[autofocus="autofocus"][id="name"][name="userName"][required=""][type="email"]')
            ]),

            m('div.pure-control-group', [
              m('label[for="password"]', 'Password'),
              m('input[id="password"][name="password"][required=""][type="password"]')
            ]),

            m('div.pure-control-group', [
              m('label[for="first"]', 'First Name'),
              m('input[id="first"][name="firstName"][required=""]')
            ]),

            m('div.pure-control-group', [
              m('label[for="last"]', 'Last Name'),
              m('input[id="last"][name="lastName"][required=""]')
            ]),

            m('div.pure-control-group', [
              m('button.pure-button.pure-button-primary[id="submit"][type="submit"]', 'Log In')
            ])

          ])
        ]),

        m(App.Components.pageFooter)
      ]);
    }
  }

  export const addView = (addUserUrl, msg) => new AddView(addUserUrl, msg);
}