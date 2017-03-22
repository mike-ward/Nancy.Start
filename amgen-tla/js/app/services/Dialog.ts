module App.Services {
  export class Dialog {
    static alert(message: string): void {
      alert(message);
    }

    static confirm(message: string): boolean {
      return confirm(message);
    }

    static prompt(message: string, value?: string): string {
      return prompt(message, value);
    }
  }
}