import {filter, OperatorFunction} from "rxjs";

export function exists<T>(): OperatorFunction<T | null | undefined, T> {
  return filter((src): src is T => src !== null && src !== undefined)
}

