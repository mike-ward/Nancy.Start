describe('Compare', () => {
  it('compareAny test cases', () => {
    const compareAny = App.Services.Compare.compareAny;
    expect(compareAny('a', 'b')).toBe(-1);
    expect(compareAny('b', 'a')).toBe(+1);
    expect(compareAny('', '')).toBe(0);
    expect(compareAny('', ' ')).toBe(-1);
    expect(compareAny(' ', '')).toBe(+1);

    expect(compareAny(null, '')).toBe(-1);
    expect(compareAny(null, null)).toBe(0);
    expect(compareAny(0, null)).toBe(0);
    expect(compareAny(1, null)).toBe(+1);
    expect(compareAny(NaN, 1)).toBe(-1);
    expect(compareAny(1, NaN)).toBe(+1);

    expect(compareAny(1, 1)).toBe(0);
    expect(compareAny(1, 2)).toBe(-1);
    expect(compareAny(2, 1)).toBe(+1);

    expect(compareAny(undefined, undefined)).toBe(0);
    expect(compareAny(1, undefined)).toBe(+1);
    expect(compareAny(undefined, 1)).toBe(-1);
  });
}); 