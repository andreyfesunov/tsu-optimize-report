import {Injectable} from "@angular/core";
import {map, Observable, shareReplay} from "rxjs";
import {exists, fromLocalStorage, isTokenValid, LocalStorage, parseJwt} from "@core/utils";
import {ITokenModel} from "@core/models";

@Injectable({providedIn: "root"})
export class AuthState {
  private readonly _tokenKey: string = "JWT_TOKEN";

  public readonly tokenRaw$: Observable<string | undefined> = fromLocalStorage(this._tokenKey).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: false
    })
  );

  public readonly token$: Observable<string> = this.tokenRaw$.pipe(
    exists(),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  public readonly user$: Observable<ITokenModel> = this.token$.pipe(
    map((token) => parseJwt(token)),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  public readonly valid$: Observable<boolean> = this.tokenRaw$.pipe(
    map((token) => isTokenValid(token))
  );

  public setToken(accessToken: string): void {
    LocalStorage.setItem(this._tokenKey, accessToken);
  }

  public removeToken(): void {
    LocalStorage.setItem(this._tokenKey);
  }
}
