describe('Renderers', () => {
  it('dateToIso should return ISO formated date', () => {
    var iso = App.Services.Renderers.dateToISO('12/1/1970');
    expect(iso).toMatch(/1970-12-01T.+\.000Z/);
  });

  it('dateToIso should return "null" for null date', () => {
    var iso = App.Services.Renderers.dateToISO(null);
    expect(iso).toBe('null');
  });

  it('camelIdentifierToTitle to title case and add spaces to indentifier', () => {
    var title = App.Services.Renderers.camelIdentifierToTitle('nowIsTheTime');
    expect(title).toBe('Now Is The Time');
  });
}); 