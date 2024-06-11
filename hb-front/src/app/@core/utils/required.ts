export function required<T>(v: T | null | undefined): T {
  if (v !== null && v !== undefined) return v;
  throw new Error('Required value is null or undefined');
}
