import {computed, signal} from "@angular/core";
import {Observable, of, switchMap} from "rxjs";

export class Spinner {
  private readonly _value$ = signal(0);
  private readonly _active$ = computed(() => this._value$() !== 0);
  private readonly _inactive$ = computed(() => this._value$() === 0);

  public get active$() {
    return this._active$();
  }

  public get inactive$() {
    return this._inactive$();
  }

  public enter() {
    this._value$.update((value) => value + 1);
  }

  public leave() {
    this._value$.update((value) => value - 1);
  }
}

export function switchMapSpinner<TIn, TOut>(project: (value: TIn) => Observable<TOut>, spinner: Spinner | undefined) {
  return switchMap<TIn, Observable<TOut>>((value) => {
    spinner?.enter();
    let active = true;

    return new Observable<TOut>((subscriber) => {
      const subscription = project(value).subscribe({
        next: (val) => {
          subscriber.next(val);
          if (active) {
            spinner?.leave();
            active = false;
          }
        },
        error: () => {
          if (active) {
            spinner?.leave();
            active = false;
          }
        }
      });

      return () => {
        subscription.unsubscribe();
        if (active) {
          spinner?.leave();
          active = false;
        }
      };
    });
  });
}

export function withSpinner<TOut>(source: Observable<TOut>, spinner: Spinner | undefined) {
  return of(0).pipe(switchMapSpinner<unknown, TOut>(() => source, spinner));
}
