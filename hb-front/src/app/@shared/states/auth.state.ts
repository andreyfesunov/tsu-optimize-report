import {map, Observable, shareReplay} from "rxjs";
import {exists, fromLocalStorage, parseJwt} from "../index";

export abstract class AuthState<TEntity> {
  public readonly tokenRaw$: Observable<string | undefined> = fromLocalStorage(this._tokenKey).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );
  public readonly token$: Observable<string> = this.tokenRaw$.pipe(
    exists(),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );
  public readonly user$: Observable<TEntity> = this.token$.pipe(
    map((token) => parseJwt(token)),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  protected constructor(private readonly _tokenKey: string) {
  }

  public setToken(accessToken: string): void {
    localStorage.setItem(this._tokenKey, JSON.stringify(accessToken));
  }
}
