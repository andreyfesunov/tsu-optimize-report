import {Observable, of, shareReplay} from "rxjs";

export class ObservableEntityCache<T> {
  constructor(private readonly getById: (id: string | null) => Observable<T>) {
  }

  private readonly map: {
    [key: string]: Observable<T>;
  } = {};

  public resolve(id: string): Observable<T> {
    const existing = this.map[id];
    if (existing) {
      return existing;
    }

    const res = this.getById(id).pipe(
      shareReplay({
        refCount: false,
        bufferSize: 1
      })
    );
    this.map[id] = res;
    return res;
  }

  public setValue(id: string, value: T) {
    this.set(id, of(value));
  }

  public set(id: string, value$: Observable<T>) {
    this.map[id] = value$;
  }
}
