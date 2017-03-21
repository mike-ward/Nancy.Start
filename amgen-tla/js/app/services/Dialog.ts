// http://github.hubspot.com/vex/api/basic/

module App.Services {
  export interface IDialogButton {
    text: string;
    type: string;
    className: string;
    click: () => void;
  }

  export interface IDialogOptions {
    callback?: () => {};
    afterOpen?: () => {};
    message?: string;
    unsafeMessage?: string;
    label?: string;
    placeholder: string;
    input?: string;
    buttons?: IDialogButton[];
    showCloseButton?: boolean;
    onSubmit?: (e) => boolean;
    focusFirstInput?: boolean;
  }

  export interface IDialogInstance {
    close: () => boolean;
    id: string;
    rootEl: HTMLElement;
    overlayEl: HTMLElement;
    contentEl: HTMLElement;
    closeEl: HTMLElement;
    isOpen: boolean;
  }

  export class Dialog {
    // Examples:
    //   Dialog.alert('Thanks for checking out vex!')
    //   Dialog.alert({ unsafeMessage: '<b>Hello ' + _.escape(user.firstName) + '!</b>' })
    static alert(stringOrOptions: string | IDialogOptions): void {
      vex.dialog.alert(stringOrOptions);
    }

    // Example:
    //   vex.dialog.confirm({
    //     message: 'Are you absolutely sure you want to destroy the alien planet?',
    //     callback: function (value) {
    //     console.log(value)
    //   }
    //  })
    static confirm(options: IDialogOptions): boolean {
      return vex.dialog.confirm(options);
    }

    // Example:
    //   vex.dialog.prompt({
    //     message: 'What planet did the aliens come from?',
    //     placeholder: 'Planet name',
    //     callback: function (value) {
    //       console.log(value)
    //     }
    //   })
    static prompt(options: IDialogOptions): string {
      return vex.dialog.prompt(options);
    }

    // Example: 
    //   Putting this all together, let's create a dialog with the following customizations:
    //
    //   Display a date input and a color input,
    //   Add an extra button which resets the color input to the default value(#000)
    //   Change the text for the OK button
    //
    //   todayDateString = new Date().toJSON().slice(0, 10)
    //   vex.dialog.open({
    //     message: 'Select a date and color.',
    //     input: [
    //       '<style>',
    //         '.vex-custom-field-wrapper {',
    //           'margin: 1em 0;',
    //         '}',
    //         '.vex-custom-field-wrapper > label {',
    //           'display: inline-block;',
    //           'margin-bottom: .2em;',
    //         '}',
    //       '</style>',
    //       '<div class="vex-custom-field-wrapper">',
    //       '  <label for="date">Date</label>',
    //         '<div class="vex-custom-input-wrapper">',
    //           '<input name="date" type="date" value="' + todayDateString + '" />',
    //         '</div>',
    //       '</div>',
    //       '<div class="vex-custom-field-wrapper">',
    //         '<label for="color">Color</label>',
    //         '<div class="vex-custom-input-wrapper">',
    //           '<input name="color" type="color" value="#ff00cc" />',
    //         '</div>',
    //       '</div>'
    //     ].join(''),
    //     callback: function (data) {
    //       if (!data) {
    //         return console.log('Cancelled')
    //       }
    //       console.log('Date', data.date, 'Color', data.color)
    //       $('.demo-result-custom-vex-dialog').show().html([
    //         '<h4>Result</h4>',
    //         '<p>',
    //         'Date: <b>' + data.date + '</b><br/>',
    //         'Color: <input type="color" value="' + data.color + '" readonly />',
    //         '</p>'
    //       ].join(''))
    //     }
    //   })
    static dialogOpen(stringOrOptions: IDialogOptions): IDialogInstance {
      return vex.dialog.open(stringOrOptions);
    }

    static close(idOrInstance: string | IDialogInstance): boolean {
      return vex.close(idOrInstance);
    }

    static closeTop(): boolean {
      return vex.closeTop();
    }

    static closeAll(): boolean {
      return vex.closeAll();
    }

    static getById(id: string): IDialogInstance {
      return vex.getById(id);
    }

    static getAll() {
      return vex.getAll();
    }
  }

  export const dialogStyles = {
    // language=CSS
    css: `
      /* override vex button styles to look like pure.css style buttons */
      .vex.vex-theme-plain .vex-dialog-button.vex-dialog-button-primary{
        display: inline-block;
        zoom: 1;
        line-height: normal;
        white-space: nowrap;
        vertical-align: middle;
        text-align: center;
        cursor: pointer;
        -webkit-user-drag: none;
        -webkit-user-select: none;
           -moz-user-select: none;
            -ms-user-select: none;
                user-select: none;
        box-sizing: border-box;
        font-family: inherit;
        padding: 0.5em 1em;
        color: #444; 
        color: rgba(0, 0, 0, 0.80); 
        border: 1px solid #999; 
        border: none rgba(0, 0, 0, 0); 
        background-color: #E6E6E6;
        text-decoration: none;
        border-radius: 2px;
      }
      .vex.vex-theme-plain .vex-dialog-button:focus {
        animation: none;
        -webkit-animation: none;
        -moz-animation: none;
        -ms-animation: none;
        -o-animation: none;
        -webkit-backface-visibility: hidden;
        outline: none;
        filter: alpha(opacity=90);
        background-image: -webkit-linear-gradient(transparent, rgba(0,0,0, 0.05) 40%, rgba(0,0,0, 0.10));
        background-image: linear-gradient(transparent, rgba(0,0,0, 0.05) 40%, rgba(0,0,0, 0.10));
      }

      /* fixes vex closing flashing in IE */
      .vex.vex-closing .vex-content {
        opacity: 0; 
      }
      .vex.vex-closing .vex-overlay {
        opacity: 0;
      }`
  }
}