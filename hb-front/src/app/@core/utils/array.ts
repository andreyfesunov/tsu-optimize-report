export class ArrayUtils {
  static sortByField<T, TField extends keyof T>(array: readonly T[], field: TField, ascending: boolean = true): T[] {
    const dir = ascending ? 1 : -1;
    return [...array].sort((a: T, b: T) => {
      if (a[field] < b[field]) {
        return -1 * dir;
      } else if (a[field] > b[field]) {
        return 1 * dir;
      } else {
        return 0;
      }
    });
  }
}
